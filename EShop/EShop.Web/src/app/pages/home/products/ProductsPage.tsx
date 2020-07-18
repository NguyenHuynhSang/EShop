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
import { actions, ExportFormat } from "../base/table.duck";
import { makeStyles, theme } from "../../../styles";
import {
  useGridApi,
  getExportParams,
  getDataAsJson,
  exportDataAsJson,
} from "../helpers/agGridHelpers";
import { useDialog } from "../helpers/hookHelpers";
import { useSelector, useDispatch } from "../../../store/store";

const useExportData = (format: ExportFormat) => {
  const api = useGridApi();
  const params = getExportParams(api);

  switch (format) {
    case ExportFormat.Csv:
      return () => api.grid?.getDataAsCsv(params);
    case ExportFormat.Excel:
      // TODO: write my own excel implementation to reduce extra dependency
      return () => api.grid?.getDataAsExcel(params);
    case ExportFormat.Json:
      return () => getDataAsJson(params, api);
  }
};
const useExportDownload = (format: ExportFormat) => {
  const api = useGridApi();
  const params = getExportParams(api);

  switch (format) {
    case ExportFormat.Csv:
      return () => api.grid?.exportDataAsCsv(params);
    case ExportFormat.Excel:
      return () => api.grid?.exportDataAsExcel(params);
    case ExportFormat.Json:
      return () => exportDataAsJson(params, api);
  }
};

function getLanguage(format: ExportFormat) {
  switch (format) {
    case ExportFormat.Csv:
      return "javascript";
    case ExportFormat.Excel:
      return "xml";
    case ExportFormat.Json:
      return "json";
  }
}
function ExportPreviewDialog() {
  const isOpen = useSelector((state) => state.table._global.exportDialogOpen);
  const format = useSelector((state) => state.table._global.exportFormat);
  const csvData = useExportData(format);
  const download = useExportDownload(format);
  const dispatch = useDispatch();
  const data = csvData();
  const close = () => dispatch(actions.setExportDialogClose());

  return (
    <Dialog open={isOpen} onClose={close}>
      <DialogTitle>Export Preview ({format})</DialogTitle>
      <DialogContent>
        <SyntaxHighlighter
          language={getLanguage(format)}
          style={highlightStyle}
        >
          {data}
        </SyntaxHighlighter>
      </DialogContent>
      <DialogActions>
        <Button
          onClick={() => {
            data && navigator.clipboard.writeText(data);
            close();
          }}
          color="primary"
        >
          Copy
        </Button>
        <Button
          onClick={() => {
            download();
            close();
          }}
          color="primary"
        >
          Download
        </Button>
      </DialogActions>
    </Dialog>
  );
}

const useExportOptionStyles = makeStyles((theme) => ({
  root: {
    display: "flex",
  },
  text: {
    width: "100%",
    padding: "8px 15px",
    transition: "color .25s ease",
    // left-align text
    display: "flex",
    justifyContent: "flex-start",
    "&:hover": {
      backgroundColor: "transparent",
      color: theme.palette.primary.main,
    },
  },
  previewButton: {
    padding: 0,
    marginLeft: "auto",
    width: "35px",
  },
}));
type ExportOptionProps = { children: React.ReactNode; format: ExportFormat };
function ExportOption({ children, format }: ExportOptionProps) {
  const download = useExportDownload(format);
  const dispatch = useDispatch();
  const styles = useExportOptionStyles();

  return (
    <div className={styles.root}>
      <Button
        className={styles.text}
        disableRipple
        disableFocusRipple
        onClick={download}
      >
        {children}
      </Button>
      <IconButton
        className={styles.previewButton}
        onClick={() => dispatch(actions.setExportDialogOpen(format))}
      >
        <VisibilityIcon htmlColor={theme.color.blue} />
      </IconButton>
    </div>
  );
}

const useExportButtonStyles = makeStyles({
  root: {
    "& [class$='-option']": {
      padding: 0,
      // remove selection color of the first option when opening
      backgroundColor: "transparent",
    },
  },
});
function ExportButton() {
  const styles = useExportButtonStyles();
  const selectedRows = useSelector((state) => state.products.rowsSelected);
  const selectedRowsText = selectedRows > 0 ? "(" + selectedRows + ")" : "";

  return (
    <SelectButton
      className={styles.root}
      startIcon={<GetAppIcon style={{ fontSize: iconSize }} />}
      variant="contained"
      color="primary"
      size="large"
      options={Object.keys(ExportFormat).map((f) => ({
        label: <ExportOption format={ExportFormat[f]}>{f}</ExportOption>,
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
            <ExportButton />
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
      <ExportPreviewDialog />
    </Portlet>
  );
}
