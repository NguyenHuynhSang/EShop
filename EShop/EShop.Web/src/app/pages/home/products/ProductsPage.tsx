import React from "react";
import {
  Portlet,
  PortletBody,
  PortletHeader,
  PortletHeaderToolbar,
} from "../../../partials/content/Portlet";
import {
  Button,
  PlusIcon,
  ProductIcon,
  VisibleIcon,
} from "../../../widgets/Common";
import ProductTable from "./ProductTable";
import CurrencySelector from "./CurrencySelector";
import ColumnDisplayDialog from "./ColumnDisplayDialog";
import ProductTablePagination from "./ProductTablePagination";
import styled, { theme } from "../../../styles/styled";

const StyledProductIcon = styled(ProductIcon)({
  paddingRight: "0.4rem",
  "& svg": {
    marginTop: "-0.2rem",
  },
});

const Toolbar = styled("div")({
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
});

export default function ProductsPage() {
  const [displayColDialogVisible, setDisplayColDialogVisible] = React.useState(
    false
  );

  return (
    <Portlet id="productTableContainer">
      <PortletHeader
        title={
          <>
            <StyledProductIcon color={theme.color.blue} size={20} />
            <span>Quản lý sản phẩm</span>
          </>
        }
        toolbar={
          <PortletHeaderToolbar>
            <Button variant="primary">
              <PlusIcon color="white" size={20} />
              &nbsp;&nbsp;Thêm sản phẩm
            </Button>
          </PortletHeaderToolbar>
        }
      />

      <PortletBody>
        <Toolbar>
          <Button
            variant="primary"
            onClick={() => setDisplayColDialogVisible(true)}
          >
            <VisibleIcon color="white" size={20} />
            &nbsp;&nbsp;Cột hiển thị&nbsp;
          </Button>
          <CurrencySelector />
          <ProductTablePagination />
        </Toolbar>
        <ProductTable />
      </PortletBody>
      <ColumnDisplayDialog
        open={displayColDialogVisible}
        handleClose={() => setDisplayColDialogVisible(false)}
      />
    </Portlet>
  );
}
