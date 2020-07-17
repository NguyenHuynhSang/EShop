import React from "react";
import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  IconButton,
} from "@material-ui/core";
import AddIcon from "@material-ui/icons/Add";
import GetAppIcon from "@material-ui/icons/GetApp";
import VisibilityIcon from "@material-ui/icons/Visibility";
import "ag-grid-enterprise";
import { Light as SyntaxHighlighter } from "react-syntax-highlighter";
import {
  Portlet,
  PortletBody,
  PortletHeader,
  PortletHeaderToolbar,
} from "../../../partials/content/Portlet";
import { base16AteliersulphurpoolLight as highlightStyle } from "react-syntax-highlighter/dist/esm/styles/prism";
import { SelectButton, ProductIcon } from "../../../widgets/Common";
import ProductTable from "./ProductTable";
import CurrencySelector from "./CurrencySelector";
import ColumnDisplayDialog from "./ColumnDisplayDialog";
import ProductTablePagination from "./ProductTablePagination";
import { makeStyles, theme } from "../../../styles";
import {
  useGridApi,
  getExportParams,
  getDataAsJson,
  exportDataAsJson,
} from "../helpers/agGridHelpers";
import { useDialog } from "../helpers/hookHelpers";
import { useSelector } from "../../../store/store";

enum ExportFormat {
  Csv = "Csv",
  Json = "Json",
  Excel = "Excel",
}
let exportFormat = ExportFormat.Csv;

const useExportData = () => {
  const api = useGridApi();
  const params = getExportParams(api);

  switch (exportFormat) {
    case ExportFormat.Csv:
      return () => api.grid?.getDataAsCsv(params);
    case ExportFormat.Excel:
      // TODO: write my own excel implementation to reduce extra dependency
      return () => api.grid?.getDataAsExcel(params);
    case ExportFormat.Json:
      return () => getDataAsJson(params, api);
  }
};
const useExportDownload = () => {
  const api = useGridApi();
  const params = getExportParams(api);

  switch (exportFormat) {
    case ExportFormat.Csv:
      return () => api.grid?.exportDataAsCsv(params);
    case ExportFormat.Excel:
      return () => api.grid?.exportDataAsExcel(params);
    case ExportFormat.Json:
      return () => exportDataAsJson(params, api);
  }
};

type DialogProps = {
  open: boolean;
  handleClose: () => void;
};
function getLanguage() {
  switch (exportFormat) {
    case ExportFormat.Csv:
      return "javascript";
    case ExportFormat.Excel:
      return "xml";
    case ExportFormat.Json:
      return "json";
  }
}
function PreviewDialog({ open, handleClose }: DialogProps) {
  const csvData = useExportData();
  const download = useExportDownload();
  const data = csvData();

  return (
    <Dialog open={open} onClose={handleClose}>
      <DialogTitle>Export Preview ({exportFormat})</DialogTitle>
      <DialogContent>
        <SyntaxHighlighter language={getLanguage()} style={highlightStyle}>
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
        <Button onClick={download} color="primary">
          Download
        </Button>
      </DialogActions>
    </Dialog>
  );
}

const useExportOptionStyles = makeStyles({
  root: {
    display: "flex",
  },
  text: {
    flex: 1,
  },
  previewButton: {
    padding: 0,
    marginLeft: "auto",
  },
});
function ExportOption({ children, onClick, onClickPreview }) {
  const styles = useExportOptionStyles();
  return (
    <div className={styles.root}>
      <span className={styles.text} onClick={onClick}>
        {children}
      </span>
      <IconButton className={styles.previewButton} onClick={onClickPreview}>
        <VisibilityIcon htmlColor={theme.color.blue} />
      </IconButton>
    </div>
  );
}

function ExportButton({ onClickPreview }) {
  const download = useExportDownload();
  const selectedRows = useSelector((state) => state.products.rowsSelected);
  const selectedRowsText = selectedRows > 0 ? "(" + selectedRows + ")" : "";

  return (
    <SelectButton
      startIcon={<GetAppIcon style={{ fontSize: iconSize }} />}
      variant="contained"
      color="primary"
      size="large"
      options={Object.keys(ExportFormat).map((f) => ({
        label: (
          <ExportOption
            onClick={(e) => {
              exportFormat = ExportFormat[f];
              download();
            }}
            onClickPreview={(e) => {
              // TODO: close menu with custom onClick handler
              exportFormat = ExportFormat[f];
              onClickPreview();
            }}
          >
            {f}
          </ExportOption>
        ),
        value: f,
        // Disable react-select Option's onClick handler, because we already have one in ExportButton.
        // This will prevent the error: <button> cannot appear as a descendant of <button>
        isDisabled: true,
      }))}
    >
      Export {selectedRowsText}
    </SelectButton>
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
