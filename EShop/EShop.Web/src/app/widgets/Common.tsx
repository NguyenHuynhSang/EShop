import React from "react";
import { Button as BsButton, ButtonProps } from "react-bootstrap";
import isString from "lodash/isString";
import ReactSelect, { StylesConfig, Props as SelectProps } from "react-select";
import styled, { theme, important } from "../styles/styled";

export const Button = styled<ButtonProps>(BsButton)({
  height: important("40px"),
  whiteSpace: "nowrap",
});

// https://github.com/JedWatson/react-select/issues/1025#issuecomment-492552567
const borderColor = theme.color.grey2;
const focusColor = theme.color.blue;
const focusedColor = theme.color.focused;
const selectStyle: StylesConfig = {
  container: (provided, { selectProps }) => ({
    ...provided,
    display: "inline-block",
    ...(selectProps.width === "auto"
      ? {}
      : isString(selectProps.width)
      ? { width: selectProps.width }
      : { width: selectProps.width + "px" }),
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
  // TODO: Add css for auto-width selector if using input
  // https://github.com/JedWatson/react-select/issues/3603#issuecomment-591511367
  indicatorSeparator: () => ({ display: "none" }),
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
  singleValue: (provided) => ({
    ...provided,
    maxWidth: "none",
    position: "static",
    transform: "none",
  }),
};

type CustomSelectProps = {
  width?: string | number;
};
type Props = SelectProps & CustomSelectProps;

export const StyledReactSelect = styled<ButtonProps>(ReactSelect)({
  // prevent value container from being pushed up when there are no space left
  '& [class$="dummyInput"]': {
    position: "absolute",
  },
});
export function Select(props: Props) {
  return <StyledReactSelect styles={selectStyle} {...props} />;
}
Select.defaultProps = {
  width: "auto",
} as Partial<Props>;
