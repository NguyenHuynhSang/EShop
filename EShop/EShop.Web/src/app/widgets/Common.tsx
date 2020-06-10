import React from "react";
import { Button } from "react-bootstrap";
import styled from "styled-components";

export const PrimaryButton = styled(PrimaryButtonInner)`
  display: inline-flex;
  height: 40px !important;
  padding-top: 0;
  padding-bottom: 0;
  margin-top: 0.25rem;
  margin-bottom: 0.25rem;
`;

type ButtonProps = {
  className?: string;
  children: React.ReactNode;
};

export default function PrimaryButtonInner(props: ButtonProps) {
  const { className, children } = props;
  return (
    <Button type="button" variant="primary" className={className}>
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
