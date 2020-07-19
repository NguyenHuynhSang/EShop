import React from "react";
import Button from "@material-ui/core/Button";
import Dialog from "@material-ui/core/Dialog";
import DialogActions from "@material-ui/core/DialogActions";
import DialogContent from "@material-ui/core/DialogContent";
import DialogTitle from "@material-ui/core/DialogTitle";
import AddIcon from "@material-ui/icons/Add";
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
import ExportButton from "./ExportButton";
import { actions, ExportFormat } from "../base/table.duck";
import { makeStyles, theme } from "../../../styles";
import { useDialog } from "../helpers/hookHelpers";
import { useSelector, useDispatch } from "../../../store/store";
import { useExportDownload, useExportData } from "../helpers/agGridHelpers";

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
            <ExportButton iconSize={iconSize} />
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
