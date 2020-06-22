import React from "react";
import { Button } from "react-bootstrap";
import styled from "styled-components";

export const PrimaryButton = styled(PrimaryButtonInner)`
  height: 40px !important;
`;

type ButtonProps = {
  className?: string;
  children: React.ReactNode;
  onClick?: ((event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => void) | undefined;
};

export default function PrimaryButtonInner(props: ButtonProps) {
  const { className, children, onClick } = props;
  return (
    <Button type="button" variant="primary" onClick={onClick} className={className}>
      {children}
    </Button>
  );
}

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
