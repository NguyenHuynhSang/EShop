import React from "react";
import { Button as BsButton, ButtonProps } from "react-bootstrap";
import isString from "lodash/isString";
import ReactSelect, { StylesConfig, Props as SelectProps } from "react-select";
import styled, { theme, important } from "../styles/styled";

export const Button = styled<ButtonProps>(BsButton)({
  height: important("40px"),
});

const customStyles: StylesConfig = {
  container: (provided, { selectProps }) => ({
    ...provided,
    display: "inline-block",
    width: isString(selectProps.width)
      ? selectProps.width
      : selectProps.width + "px",
  }),
  control: (provided) => ({
    ...provided,
    height: "40px",
    borderColor: theme.color.grey2,
    ":hover": {
      borderColor: theme.color.blue,
    },
  }),
  dropdownIndicator: (provided) => ({
    ...provided,
    color: theme.color.grey2,
    ":hover": {
      // TODO: use immer
      ...provided[":hover"],
      color: theme.color.blue,
    },
    ":focus": {
      ...provided[":focus"],
      color: theme.color.blue,
    },
    ":active": {
      ...provided[":active"],
      color: theme.color.blue,
    },
  }),
  indicatorSeparator: (provided) => ({
    display: "none",
  }),
};

type CustomSelectProps = {
  width?: string | number;
};

export function Select(props: SelectProps & CustomSelectProps) {
  return <ReactSelect styles={customStyles} {...props} />;
}
