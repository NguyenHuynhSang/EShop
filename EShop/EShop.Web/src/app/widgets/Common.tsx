import React from "react";
import { Button as BsButton, ButtonProps } from "react-bootstrap";
import styled from "styled-components";

export const Button = styled(BsButton)<ButtonProps>`
  height: 40px !important;
`;

const SvgIconWrapper = styled.span<SvgIconProps>`
  svg {
    width: ${(p) => p.size}px;

    g {
      [fill] {
        fill: ${(p) => p.color};
      }
    }
  }
`;

type SvgIconProps = {
  color?: string;
  size?: number;
  children: React.ReactNode;
  className?: string;
};

export function SvgIcon(props: SvgIconProps) {
  const { color, size, children, className } = props;
  return (
    <SvgIconWrapper color={color} size={size} className={className}>
      {children}
    </SvgIconWrapper>
  );
}
