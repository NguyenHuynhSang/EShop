import React from "react";
import isString from "lodash/isString";
import ReactSelect, {
  StylesConfig,
  Props as SelectProps,
  components as SelectComps,
} from "react-select";
import styled, { theme } from "../styles/styled";
import { Styles } from "react-select/src/styles";

export function toSimpleOption(data: any) {
  return {
    label: data as string,
    value: data as string,
  };
}

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
        cursor: "pointer",
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
  groupHeading: (provided) => ({
    ...provided,
    color: theme.color.primary,
    marginBottom: "0.5em",
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
  // autosize select to its content width
  singleValue: (provided) => ({
    ...provided,
    maxWidth: "none",
    position: "static",
    transform: "none",
  }),
  placeholder: (provided) => ({
    ...provided,
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

export { SelectComps };
export type { SelectProps };

const baseStyle = selectStyle as Required<Styles>;
const agSelectStyle: StylesConfig = {
  ...baseStyle,
  container: (provided, state) => ({
    ...baseStyle.container(provided, state),
    width: "100%",
  }),
  control: (provided, state) => ({
    ...baseStyle.control(provided, state),
    border: "none",
    backgroundColor: "transparent",
    borderRadius: 0,
  }),
  menu: (provided, state) => ({
    ...baseStyle.menu(provided, state),
    marginTop: "2px",
    marginBottom: "2px",
    borderRadius: 0,
  }),
  dropdownIndicator: (provided, state) => ({
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
      menuPlacement="auto"
      {...props}
    />
  );
}
AgSelect.defaultProps = { ...Select.defaultProps };
