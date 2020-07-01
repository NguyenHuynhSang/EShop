import React from "react";
import {
  Portlet,
  PortletBody,
  PortletHeader,
  PortletHeaderToolbar,
} from "../../../partials/content/Portlet";
import { Button, SvgIcon } from "../../../widgets/Common";
import { ReactComponent as PlusIcon } from "../../../../assets/Add.svg";
import { ReactComponent as ProductIcon } from "../../../../assets/Product.svg";
import { ReactComponent as VisibleIcon } from "../../../../assets/Visible.svg";
import ProductTable from "./ProductTable";
import CurrencySelector from "./CurrencySelector";
import ColumnDisplayDialog from "./ColumnDisplayDialog";
import { useSelector, shallowEqual } from "../../../store/store";
import styled, { theme } from "../../../styles/styled";

const PortletIcon = styled(SvgIcon)({
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
  const columnInfos = useSelector(
    (state) => state.products.columnInfos,
    shallowEqual
  );
  const [displayColDialogVisible, setDisplayColDialogVisible] = React.useState(
    false
  );

  return (
    <Portlet id="productTableContainer">
      <PortletHeader
        title={
          <>
            <PortletIcon color={theme.color.blue} size={20}>
              <ProductIcon />
            </PortletIcon>
            <span>Quản lý sản phẩm</span>
          </>
        }
        toolbar={
          <PortletHeaderToolbar>
            <Button variant="primary">
              <SvgIcon color="white" size={20}>
                <PlusIcon />
              </SvgIcon>
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
            <SvgIcon color="white" size={20}>
              <VisibleIcon />
            </SvgIcon>
            &nbsp;&nbsp;Cột hiển thị&nbsp;
          </Button>
          <CurrencySelector />
        </Toolbar>
        <ProductTable columnInfos={columnInfos} />
      </PortletBody>
      <ColumnDisplayDialog
        open={displayColDialogVisible}
        handleClose={() => setDisplayColDialogVisible(false)}
        initialValue={columnInfos}
      />
    </Portlet>
  );
}
