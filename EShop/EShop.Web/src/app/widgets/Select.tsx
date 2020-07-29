import React from 'react';
import isString from 'lodash/isString';
import ReactSelect, {
  StylesConfig,
  Props as SelectProps,
  components as SelectComps,
  ValueType,
  ActionMeta,
  OptionsType,
} from 'react-select';
import { Styles } from 'react-select/src/styles';
import Button, { ButtonProps } from '@material-ui/core/Button';
import { makeStyles, theme } from '../styles';

export * from 'react-select';
export { SelectComps };

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
    display: 'inline-block',
    ...(selectProps.width === 'auto'
      ? {}
      : isString(selectProps.width)
      ? { width: selectProps.width }
      : { width: selectProps.width + 'px' }),
  }),
  control: (provided, { isSelected, isFocused }) => {
    // console.log(state);
    return {
      ...provided,
      height: '40px',
      boxShadow: 'none',
      borderColor: isSelected || isFocused ? focusColor : borderColor,
      ':hover': {
        borderColor: focusColor,
        cursor: 'pointer',
      },
    };
  },
  dropdownIndicator: (provided, { isSelected, isFocused }) => ({
    ...provided,
    color: isSelected || isFocused ? focusColor : borderColor,
    ':hover': {
      color: focusColor,
    },
  }),
  groupHeading: provided => ({
    ...provided,
    color: theme.color.primary,
    marginBottom: '0.5em',
  }),
  // TODO: Add css for auto-width selector if using input
  // https://github.com/JedWatson/react-select/issues/3603#issuecomment-591511367
  indicatorSeparator: () => ({ display: 'none' }),
  menu: provided => ({
    ...provided,
    boxShadow: theme.shadow.normal,
  }),
  option: (provided, { isSelected, isFocused }) => {
    return {
      ...provided,
      transition: 'background-color .15s ease',
      backgroundColor: isSelected
        ? theme.color.secondaryLight
        : isFocused
        ? focusedColor
        : 'transparent',
      color: isSelected ? theme.color.secondary : 'inherit',
      fontWeight: isSelected ? 'bold' : 'inherit',
      ':hover': {
        cursor: 'pointer',
      },
    };
  },
  // autosize select to its content width
  singleValue: provided => ({
    ...provided,
    maxWidth: 'none',
    position: 'static',
    transform: 'none',
  }),
  placeholder: provided => ({
    ...provided,
    position: 'static',
    transform: 'none',
  }),
};

type CustomSelectProps = { width?: string | number };
type Props = SelectProps & CustomSelectProps;

const useStyles = makeStyles({
  root: {
    // prevent value container from being pushed up when there are no space left
    '& [class$="dummyInput"]': {
      position: 'absolute',
    },
  },
});
export default function Select(props: Props) {
  const { width = 'auto', ...rest } = props;
  const styles = useStyles();
  return (
    <ReactSelect
      styles={selectStyle}
      className={styles.root}
      width={width}
      {...rest}
    />
  );
}

const useSelectButtonStyles = makeStyles<Props>({
  childrenWrapper: {
    position: 'absolute',
    width: '100%',
    height: '100%',
    '& > [class$="-control"]': {
      opacity: 0,
    },
  },
});

export type OptionType = { label: any; value: string };
export type SelectButtonProps = ButtonProps & {
  onChange?: (
    value: ValueType<OptionType>,
    action: ActionMeta<OptionType>
  ) => void;
  options: OptionsType<OptionType>;
};
export const SelectButton = (props: SelectButtonProps) => {
  const { onChange, options, children: sbChildren, ...sbRest } = props;
  const styles = useStyles();
  const selectButtonStyles = useSelectButtonStyles();

  return (
    <Select
      styles={selectStyle}
      className={styles.root}
      isSearchable={false}
      // menuPortalTarget={document.body}
      onChange={onChange}
      options={options}
      width='auto'
      components={{
        SelectContainer: ({ children, ...rest }) => (
          <SelectComps.SelectContainer {...rest}>
            <Button {...sbRest}>
              {sbChildren}
              <div className={selectButtonStyles.childrenWrapper}>
                {children}
              </div>
            </Button>
          </SelectComps.SelectContainer>
        ),
        IndicatorsContainer: () => null,
        ClearIndicator: () => null,
        Placeholder: () => null,
      }}
    />
  );
};

const baseStyle = selectStyle as Required<Styles>;
const agSelectStyle: StylesConfig = {
  ...baseStyle,
  container: (provided, state) => ({
    ...baseStyle.container(provided, state),
    width: '100%',
  }),
  control: (provided, state) => ({
    ...baseStyle.control(provided, state),
    border: 'none',
    backgroundColor: 'transparent',
    borderRadius: 0,
  }),
  menu: (provided, state) => ({
    ...baseStyle.menu(provided, state),
    marginTop: '2px',
    marginBottom: '2px',
    borderRadius: 0,
  }),
  dropdownIndicator: (provided, state) => ({
    ...baseStyle.dropdownIndicator(provided, state),
    color: focusColor,
  }),
};

export function AgSelect(props: Props) {
  const { width = 'auto', ...rest } = props;
  const styles = useStyles();

  return (
    <ReactSelect
      className={styles.root}
      styles={agSelectStyle}
      // insert the menu outside of the table so it wont be hidden
      menuPortalTarget={document.body}
      // Minimum height of the menu before flipping
      minMenuHeight={220}
      // flip when there isn't enough space below the control.
      menuPlacement='auto'
      width={width}
      {...rest}
    />
  );
}
