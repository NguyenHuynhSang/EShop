import React from "react";
import {
  Portlet,
  PortletBody,
  PortletHeader,
  PortletHeaderToolbar,
} from "../../../partials/content/Portlet";
import { Button } from "@material-ui/core";
import AddIcon from "@material-ui/icons/Add";
import GetAppIcon from "@material-ui/icons/GetApp";
import VisibilityIcon from "@material-ui/icons/Visibility";
import { ProductIcon } from "../../../widgets/Common";
import ProductTable from "./ProductTable";
import CurrencySelector from "./CurrencySelector";
import ColumnDisplayDialog from "./ColumnDisplayDialog";
import ProductTablePagination from "./ProductTablePagination";
import { makeStyles, theme } from "../../../styles";
import { useGridApi } from "../helpers/agGridHelpers";
import { useSelector } from "../../../store/store";

const iconSize = 18;

const useToolbarStyles = makeStyles({
  root: {
    "& > :not(:last-child)": {
      marginRight: theme.spacing.md,
    },
  },
});

function Toolbar() {
  const [api] = useGridApi();
  const styles = useToolbarStyles();
  const selectedRows = useSelector((state) => state.products.rowsSelected);
  const selectedRowsText = selectedRows > 0 ? "(" + selectedRows + ")" : "";

  return (
    <PortletHeaderToolbar className={styles.root}>
      <Button
        startIcon={<AddIcon style={{ fontSize: iconSize }} />}
        variant="outlined"
        color="primary"
        size="large"
      >
        Thêm sản phẩm
      </Button>
      <Button
        startIcon={<GetAppIcon style={{ fontSize: iconSize }} />}
        variant="contained"
        color="primary"
        size="large"
        onClick={() => {
          const visibleCols = api.column
            ?.getAllDisplayedColumns()
            .map((c) => c.getColId());

          api.grid?.exportDataAsCsv({
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
          });
        }}
      >
        Export {selectedRowsText}
      </Button>
    </PortletHeaderToolbar>
  );
}

const useStyles = makeStyles({
  productIcon: {
    paddingRight: "0.4rem",
    "& svg": {
      marginTop: "-0.2rem",
    },
  },
  toolbar: {
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
  const [displayColDialogVisible, setDisplayColDialogVisible] = React.useState(
    false
  );

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
        toolbar={<Toolbar />}
      />

      <PortletBody>
        <div className={styles.toolbar}>
          <Button
            startIcon={<VisibilityIcon style={{ fontSize: iconSize }} />}
            variant="outlined"
            color="primary"
            onClick={() => setDisplayColDialogVisible(true)}
          >
            Cột hiển thị
          </Button>
          <CurrencySelector />
          <ProductTablePagination />
        </div>
        <ProductTable name="product" />
      </PortletBody>
      <ColumnDisplayDialog
        open={displayColDialogVisible}
        handleClose={() => setDisplayColDialogVisible(false)}
      />
    </Portlet>
  );
}
