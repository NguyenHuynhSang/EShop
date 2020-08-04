const spacing = {
  0: '0',
  xl: '3rem',
  lg: '2rem',
  md: '1rem',
  sm: '.5rem',
  xs: '.25rem',
};
export type SpaceSize = keyof typeof spacing;

const theme = {
  color: {
    blue: '#007bff',
    indigo: '#6610f2',
    purple: '#6f42c1',
    pink: '#e83e8c',
    red: '#dc3545',
    orange: '#fd7e14',
    yellow: '#ffc107',
    green: '#28a745',
    teal: '#20c997',
    cyan: '#17a2b8',
    white: '#fff',
    grey: '#6c757d',
    grey2: '#e4e6ef',
    grayDark: '#343a40',
    primary: '#3699ff',
    primaryDark1: '#3089e5',
    primaryLight: '#E1F0FF',
    secondary: '#ffa800',
    secondaryLight: '#fff4de',
    success: '#1bc5bd',
    info: '#8950fc',
    warning: '#ffa800',
    danger: '#f64e60',
    light: '#f8f9fa',
    dark: '#343a40',
    background: '#f2f3f8',
    focused: '#f3f6f9',
  },
  font: {
    display: 'Poppins, Helvetica, sans-serif',
    number: "'Roboto Mono', monospace",
  },
  shadow: {
    light: '0 0 50px 0 rgba(82,63,105,.1)',
    normal: '0 0 50px 0 rgba(82,63,105,.15)',
  },
  spacing,
  space: (topBottom: SpaceSize, leftRight: SpaceSize) =>
    `${spacing[topBottom]} ${spacing[leftRight]}`,
  tableRowHeight: 40,
};

export default theme;
