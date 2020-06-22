import React from "react";
import styled from "styled-components";
import {
  Portlet,
  PortletBody,
  PortletHeader,
  PortletHeaderToolbar,
} from "../../../partials/content/Portlet";
import { PrimaryButton, SvgIcon } from "../../../widgets/Common";
import { ReactComponent as PlusIcon } from "../../../../assets/Add.svg";
import { ReactComponent as ProductIcon } from "../../../../assets/Product.svg";
import { ReactComponent as VisibleIcon } from "../../../../assets/Visible.svg";
import theme from "../../../styles/theme";
import ProductTable from "./ProductTable";
import ColumnDisplayDialog from "./ColumnDisplayDialog";
import { useSelector, shallowEqual } from "../../../store/store";

const PortletIcon = styled(SvgIcon)`
  padding-right: 0.4rem;

  svg {
    margin-top: -0.2rem;
  }
`;

const Toolbar = styled.div`
  margin-bottom: ${theme.spacing.md};
`;

export default function ProductsPage() {
  const columnInfos = useSelector((state) => state.products.columnInfos, shallowEqual);
  const [displayColDialogVisible, setDisplayColDialogVisible] = React.useState(
    false
  );

  return (
    <Portlet>
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
            <PrimaryButton>
              <SvgIcon color="white" size={20}>
                <PlusIcon />
              </SvgIcon>
              &nbsp;&nbsp;Thêm sản phẩm
            </PrimaryButton>
          </PortletHeaderToolbar>
        }
      />

      <PortletBody>
        <Toolbar>
          <PrimaryButton onClick={() => setDisplayColDialogVisible(true)}>
            <SvgIcon color="white" size={20}>
              <VisibleIcon />
            </SvgIcon>
            &nbsp;&nbsp;Cột hiển thị&nbsp;
          </PrimaryButton>
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
