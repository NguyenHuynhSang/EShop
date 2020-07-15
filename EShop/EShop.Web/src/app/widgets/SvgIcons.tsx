import React from "react";
import styled from "../styles/styled";
import { ReactComponent as ProductSvg } from "../../assets/Product.svg";

import { ReactComponent as UsaSvg } from "../../assets/Flags/united-states.svg";
import { ReactComponent as VietnamSvg } from "../../assets/Flags/vietnam.svg";
import { ReactComponent as EuSvg } from "../../assets/Flags/european-union.svg";
import { ReactComponent as JapanSvg } from "../../assets/Flags/japan.svg";

type SvgIconWrapperProps = {
  color?: string;
  size?: number;
  children: React.ReactNode;
  className?: string;
};

const SvgIconWrapper = styled<SvgIconWrapperProps>("span")({
  "& svg": {
    width: (p) => p.size + "px",

    "& g": {
      "& [fill]": {
        fill: (p) => p.color,
      },
    },
  },
});

export function SvgIcon(props: SvgIconWrapperProps) {
  const { color, size, children, className } = props;
  return (
    <SvgIconWrapper color={color} size={size} className={className}>
      {children}
    </SvgIconWrapper>
  );
}

type SvgIconProps = Omit<SvgIconWrapperProps, "children">;

export function ProductIcon(props: SvgIconProps) {
  return (
    <SvgIcon {...props}>
      <ProductSvg />
    </SvgIcon>
  );
}
export function UsaIcon(props: SvgIconProps) {
  return (
    <SvgIcon {...props}>
      <UsaSvg />
    </SvgIcon>
  );
}
export function VietnamIcon(props: SvgIconProps) {
  return (
    <SvgIcon {...props}>
      <VietnamSvg />
    </SvgIcon>
  );
}
export function EuIcon(props: SvgIconProps) {
  return (
    <SvgIcon {...props}>
      <EuSvg />
    </SvgIcon>
  );
}
export function JapanIcon(props: SvgIconProps) {
  return (
    <SvgIcon {...props}>
      <JapanSvg />
    </SvgIcon>
  );
}
