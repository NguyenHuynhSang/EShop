import React from "react";
import {
  Portlet,
  PortletBody,
  PortletHeader,
  PortletHeaderToolbar,
} from "../../../partials/content/Portlet";
import { Button } from "../../../widgets/Common";
import { PlusIcon, ProductIcon, VisibleIcon } from "../../../widgets/SvgIcons";
import ProductTable from "./ProductTable";
import CurrencySelector from "./CurrencySelector";
import ColumnDisplayDialog from "./ColumnDisplayDialog";
import { useSelector, shallowEqual } from "../../../store/store";
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

  "& > :not(:last-child)": {
    marginRight: theme.spacing.md,
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
