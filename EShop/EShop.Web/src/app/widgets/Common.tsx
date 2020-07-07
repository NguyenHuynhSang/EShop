import React from "react";
import { Button as BsButton, ButtonProps } from "react-bootstrap";
import isString from "lodash/isString";
import ReactSelect, { StylesConfig, Props as SelectProps } from "react-select";
import styled, { theme, important } from "../styles/styled";
import { Styles } from "react-select/src/styles";

export const Button = styled<ButtonProps>(BsButton)({
  height: important("40px"),
  whiteSpace: "nowrap",
});

// https://github.com/JedWatson/react-select/issues/1025#issuecomment-492552567
const borderColor = theme.color.grey2;
const focusColor = theme.color.primary;
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
  control: (provided, { isSelected, isFocused }) => {
    // console.log(state);
    return {
      ...provided,
      height: "40px",
      boxShadow: "none",
      borderColor: isSelected || isFocused ? focusColor : borderColor,
      ":hover": {
        borderColor: focusColor,
      },
    };
  },
  dropdownIndicator: (provided, { isSelected, isFocused }) => ({
    ...provided,
    color: isSelected || isFocused ? focusColor : borderColor,
    ":hover": {
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

const StyledReactSelect = styled<Props>(ReactSelect)({
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

const baseStyle = selectStyle as Required<Styles>;
const agSelectStyle: StylesConfig = {
  ...baseStyle,
  container: (provided, state) => ({
    ...provided,
    ...baseStyle.container(provided, state),
    width: "100%",
  }),
  control: (provided, state) => ({
    ...provided,
    ...baseStyle.control(provided, state),
    border: "none",
    backgroundColor: "transparent",
    borderRadius: 0,
  }),
  menu: (provided, state) => ({
    ...provided,
    ...baseStyle.menu(provided, state),
    marginTop: '2px',
    marginBottom: '2px',
    borderRadius: 0,
  }),
  dropdownIndicator: (provided, state) => ({
    ...provided,
    ...baseStyle.dropdownIndicator(provided, state),
    color: focusColor,
  }),
};
export function AgSelect(props: Props) {
  return (
    <StyledReactSelect
      styles={agSelectStyle}
      // insert the menu outside of the table so it wont be hidden
      menuPortalTarget={document.body}
      // Minimum height of the menu before flipping
      minMenuHeight={220}
      // flip when there isn't enough space below the control.
      menuPlacement='auto'
      {...props}
    />
  );
}
AgSelect.defaultProps = { ...Select.defaultProps };
