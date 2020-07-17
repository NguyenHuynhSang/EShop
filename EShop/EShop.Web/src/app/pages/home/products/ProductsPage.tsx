import React, { useCallback } from "react";
import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Tooltip,
} from "@material-ui/core";
import AddIcon from "@material-ui/icons/Add";
import GetAppIcon from "@material-ui/icons/GetApp";
import VisibilityIcon from "@material-ui/icons/Visibility";
import { Light as SyntaxHighlighter } from "react-syntax-highlighter";
import {
  Portlet,
  PortletBody,
  PortletHeader,
  PortletHeaderToolbar,
} from "../../../partials/content/Portlet";
import { base16AteliersulphurpoolLight as highlightStyle } from "react-syntax-highlighter/dist/esm/styles/prism";
import { ProductIcon } from "../../../widgets/Common";
import ProductTable from "./ProductTable";
import CurrencySelector from "./CurrencySelector";
import ColumnDisplayDialog from "./ColumnDisplayDialog";
import ProductTablePagination from "./ProductTablePagination";
import { makeStyles, theme } from "../../../styles";
import { useGridApi, AgGridApi } from "../helpers/agGridHelpers";
import { useDialog } from "../helpers/hookHelpers";
import { CsvExportParams } from "ag-grid-community";
import { useSelector } from "../../../store/store";

const getExportParams = (api: AgGridApi): CsvExportParams => {
  const selectedRows = api.grid?.getSelectedNodes().length || 0;
  const visibleCols = api.column
    ?.getAllDisplayedColumns()
    .map((c) => c.getColId());

  return {
    onlySelected: selectedRows > 0,
    columnKeys: visibleCols?.filter((c) => c !== "action"),
    processCellCallback: (params) => {
      if (!params.node) return null;

      const field = params.column.getColDef().field!;
      if (field === "category") {
        return params.node.data[field].name;
      }

      return params.node.data[field];
    },
  };
};

const useCsvData = () => {
  const api = useGridApi();
  const params = getExportParams(api);
  const getData = useCallback(() => api.grid?.getDataAsCsv(params), [
    api.grid,
    params,
  ]);

  return getData;
};
const useCsvExport = () => {
  const api = useGridApi();
  const params = getExportParams(api);
  const exportData = useCallback(() => api.grid?.exportDataAsCsv(params), [
    api.grid,
    params,
  ]);

  return exportData;
};

type DialogProps = {
  open: boolean;
  handleClose: () => void;
};
function PreviewDialog({ open, handleClose }: DialogProps) {
  const csvData = useCsvData();
  const exportData = useCsvExport();
  const data = csvData();

  return (
    <Dialog open={open} onClose={handleClose}>
      <DialogTitle>Export Preview (Csv)</DialogTitle>
      <DialogContent>
        <SyntaxHighlighter language="javascript" style={highlightStyle}>
          {data}
        </SyntaxHighlighter>
      </DialogContent>
      <DialogActions>
        <Button
          onClick={() => {
            data && navigator.clipboard.writeText(data);
            handleClose();
          }}
          color="primary"
        >
          Copy
        </Button>
        <Button onClick={exportData} color="primary">
          Download
        </Button>
      </DialogActions>
    </Dialog>
  );
}

const exportBtnTooltip = "Right click to see the preview before downloading";
function ExportButton({ onClickPreview }) {
  const exportData = useCsvExport();
  const selectedRows = useSelector((state) => state.products.rowsSelected);
  const selectedRowsText = selectedRows > 0 ? "(" + selectedRows + ")" : "";

  return (
    <Tooltip title={exportBtnTooltip} enterDelay={250} enterNextDelay={2000}>
      <Button
        startIcon={<GetAppIcon style={{ fontSize: iconSize }} />}
        variant="contained"
        color="primary"
        size="large"
        onClick={exportData}
        onContextMenu={(e) => {
          e.preventDefault();
          onClickPreview();
        }}
      >
        Export {selectedRowsText}
      </Button>
    </Tooltip>
  );
}

const iconSize = 18;
const useStyles = makeStyles({
  productIcon: {
    paddingRight: "0.4rem",
    "& svg": {
      marginTop: "-0.2rem",
    },
  },
  toolbar: {
    "& > :not(:last-child)": {
      marginRight: theme.spacing.md,
    },
  },
  action: {
    marginBottom: theme.spacing.md,
    display: "flex",
    // fix pinned rows (:before has zIndex: 1) overlapping currency selector
    zIndex: 2,

    "& > :not(:last-child)": {
      marginRight: theme.spacing.md,
    },
    "& > .ag-pagination": {
      marginLeft: "auto",
    },
  },
});

export default function ProductsPage() {
  const styles = useStyles();
  const colDialog = useDialog();
  const previewDialog = useDialog();

  return (
    <Portlet id="productTableContainer">
      <PortletHeader
        title={
          <>
            <ProductIcon
              className={styles.productIcon}
              color={theme.color.blue}
              size={20}
            />
            <span>Quản lý sản phẩm</span>
          </>
        }
        toolbar={
          <PortletHeaderToolbar className={styles.toolbar}>
            <Button
              startIcon={<AddIcon style={{ fontSize: iconSize }} />}
              variant="outlined"
              color="primary"
              size="large"
            >
              Thêm sản phẩm
            </Button>
            <ExportButton onClickPreview={previewDialog.handleOpen} />
          </PortletHeaderToolbar>
        }
      />
      <PortletBody>
        <div className={styles.action}>
          <Button
            startIcon={<VisibilityIcon style={{ fontSize: iconSize }} />}
            variant="outlined"
            color="primary"
            onClick={colDialog.handleOpen}
          >
            Cột hiển thị
          </Button>
          <CurrencySelector />
          <ProductTablePagination />
        </div>
        <ProductTable name="product" />
      </PortletBody>
      <ColumnDisplayDialog
        open={colDialog.open}
        handleClose={colDialog.handleClose}
      />
      <PreviewDialog
        open={previewDialog.open}
        handleClose={previewDialog.handleClose}
      />
    </Portlet>
  );
}
