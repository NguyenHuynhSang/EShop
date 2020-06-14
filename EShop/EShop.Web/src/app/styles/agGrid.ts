import { createGlobalStyle } from "styled-components";
import theme from "./theme";

export default createGlobalStyle`
.ag-root {
    width: 100% !important;
    border: none !important;
    box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
}

.ag-cell-focus {
    border: none !important;
}

.ag-cell .ag-react-container {
    display: flex; /* vertically center custom components like checkbox */
    justify-content: center;
}

.ag-cell {
    display: flex;
    /* vertically align content because custom height is larger than normal */
    align-items: center;
}

/* theming */

.ag-theme-balham {
    font-family: var(--display-font) !important;
}

.ag-root-wrapper {
    --ag-row-border-color: #ecf0f3;
    --ag-border-color: var(--light);
    border: solid 1px var(--ag-row-border-color) !important;
}

.ag-root-wrapper .ag-header {
    border-bottom: 1px solid var(--ag-row-border-color);
}

.ag-root-wrapper .ag-header-cell::after {
    background-color: transparent;
}

.ag-root-wrapper .ag-cell.ag-cell-dirty {
    background-color: rgba(255, 193, 7, .5);
}

.ag-ltr .ag-right-aligned-cell {
    justify-content: right; /* text-align not working since we use flex here */
}

.ag-cell-inline-editing {
    height: ${theme.tableRowHeight}px !important;
    border-radius: 0 !important;
}

/* ag cell select */
.ag-picker-field-wrapper {
    border: solid 1px var(--ag-row-border-color);
    background-color: var(--ag-border-color) !important;
    border-radius: 0 !important;
}
.ag-popup-child:not(.ag-tooltip-custom) {
    box-shadow: 0 0 50px 0 rgba(82,63,105,.15) !important;
}
.ag-list-item {
    padding: .75rem 1.25rem !important;
    height: auto !important;
}

/* ag input */
.ag-right-aligned-cell .ag-text-field-input {
    text-align: right;
}
`;
