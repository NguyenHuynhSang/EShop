import React from "react";
import styled from "../styles/styled";
import { ReactComponent as PlusSvg } from "../../assets/Add.svg";
import { ReactComponent as ProductSvg } from "../../assets/Product.svg";
import { ReactComponent as VisibleSvg } from "../../assets/Visible.svg";

import { ReactComponent as ArrowLeftSvg } from "../../assets/Angle-left.svg";
import { ReactComponent as ArrowRightSvg } from "../../assets/Angle-right.svg";
import { ReactComponent as ArrowToLeftSvg } from "../../assets/Arrow-to-left.svg";
import { ReactComponent as ArrowToRightSvg } from "../../assets/Arrow-to-right.svg";

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

export function PlusIcon(props: SvgIconProps) {
  return (
    <SvgIcon {...props}>
      <PlusSvg />
    </SvgIcon>
  );
}
export function ProductIcon(props: SvgIconProps) {
  return (
    <SvgIcon {...props}>
      <ProductSvg />
    </SvgIcon>
  );
}
export function VisibleIcon(props: SvgIconProps) {
  return (
    <SvgIcon {...props}>
      <VisibleSvg />
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
export function ArrowLeftIcon(props: SvgIconProps) {
  return (
    <SvgIcon {...props}>
      <ArrowLeftSvg />
    </SvgIcon>
  );
}
export function ArrowRightIcon(props: SvgIconProps) {
  return (
    <SvgIcon {...props}>
      <ArrowRightSvg />
    </SvgIcon>
  );
}
export function ArrowToLeftIcon(props: SvgIconProps) {
  return (
    <SvgIcon {...props}>
      <ArrowToLeftSvg />
    </SvgIcon>
  );
}
export function ArrowToRightIcon(props: SvgIconProps) {
  return (
    <SvgIcon {...props}>
      <ArrowToRightSvg />
    </SvgIcon>
  );
}
