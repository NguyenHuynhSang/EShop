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
import theme from "../../../styles/theme";

const PortletIcon = styled(SvgIcon)`
  padding-right: 0.4rem;

  svg {
    margin-top: -0.2rem;
  }
`;

export default function ProductsPage() {
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

      <PortletBody>lol</PortletBody>
    </Portlet>
  );
}
