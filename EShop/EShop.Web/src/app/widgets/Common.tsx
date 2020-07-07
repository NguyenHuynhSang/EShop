import React from "react";
import { Button as BsButton, ButtonProps } from "react-bootstrap";
import isString from "lodash/isString";
import ReactSelect, { StylesConfig, Props as SelectProps } from "react-select";
import styled, { theme, important } from "../styles/styled";

export const Button = styled<ButtonProps>(BsButton)({
  height: important("40px"),
});

// https://github.com/JedWatson/react-select/issues/1025#issuecomment-492552567
const borderColor = theme.color.grey2;
const focusColor = theme.color.blue;
const focusedColor = theme.color.focused;
const selectStyle: StylesConfig = {
  container: (provided, { selectProps }) => ({
    ...provided,
    display: "inline-block",
    width: isString(selectProps.width)
      ? selectProps.width
      : selectProps.width + "px",
  }),
  control: (provided, state) => {
    // console.log(state);
    return {
      ...provided,
      height: "40px",
      borderColor,
      ":hover": {
        borderColor: focusColor,
      },
    };
  },
  dropdownIndicator: (provided) => ({
    ...provided,
    color: borderColor,
    ":hover": {
      ...provided[":hover"],
      color: focusColor,
    },
    ":focus": {
      ...provided[":focus"],
      color: focusColor,
    },
    ":active": {
      ...provided[":active"],
      color: focusColor,
    },
  }),
  indicatorSeparator: (provided) => ({
    display: "none",
  }),
  menu: (provided) => ({
    ...provided,
    boxShadow: theme.shadow.normal,
  }),
  option: (provided, { isSelected, isFocused }) => {
    return {
      ...provided,
      transition: "background-color .15s ease",
      backgroundColor: isSelected
        ? theme.color.secondaryLight
        : isFocused
        ? focusedColor
        : "transparent",
      color: isSelected ? theme.color.secondary : "inherit",
      fontWeight: isSelected ? "bold" : "inherit",
    };
  },
};

type CustomSelectProps = {
  width?: string | number;
};

export function Select(props: SelectProps & CustomSelectProps) {
  return <ReactSelect styles={selectStyle} {...props} />;
}
