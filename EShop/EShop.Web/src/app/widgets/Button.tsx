import { Button as BsButton, ButtonProps } from "react-bootstrap";
import styled, { important } from "../styles/styled";

export const Button = styled<ButtonProps>(BsButton)({
  height: important("40px"),
  whiteSpace: "nowrap",
});
