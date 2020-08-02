import createGlobalStyle from './createGlobalStyle';
import { theme } from './styled';

const agCustomTheme = 'balham';
const rootSelector = '.ag-theme-' + agCustomTheme + '.table-wrapper';

export default createGlobalStyle`
/* copy from https://github.com/ag-grid/ag-grid/blob/e9a157c75e44ac1a88edd6873be6d6852f5726f0/dist/styles/ag-theme-material.css#L10-L11
because I don't want to use the whole material theme for ag-grid */
@font-face {
    font-family: "agGridMaterial";
    src: url("data:application/font-woff;charset=utf-8;base64,d09GRgABAAAAABGsAAsAAAAAIJAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAABHU1VCAAABCAAAAlEAAAReYPJi3U9TLzIAAANcAAAAPgAAAFZWTFJvY21hcAAAA5wAAAHsAAAFgGqPB0pnbHlmAAAFiAAACFEAAA58peGl1GhlYWQAAA3cAAAALwAAADZ2zcSBaGhlYQAADgwAAAAWAAAAJAfRBDJobXR4AAAOJAAAABIAAAEkt5gAAGxvY2EAAA44AAAAbQAAAJSyFLU8bWF4cAAADqgAAAAfAAAAIAFcAEluYW1lAAAOyAAAATIAAAJebBQ2inBvc3QAAA/8AAABrgAAAom3/2OGeJx9k09yElEQxr9hCCYBk6gRFVFT8X/UOM4MTIBIQBApKwsXLly4iQstrZSrnMC1B7A8gKfwBJZLVx7AA1gewF83g5gswhQz73V//fXX/fopkLSoSF0VhqPd51ra3zt4r5qKmvzM//862H/7Zk/z0x2+on/nFYR/tKR1vdCBvgUKXgefC6WwEHbD3fBD+Cn8oRDUmjZBl/WAJ1bCO1KqhprKVFAJW8x6Sy3QIe+2W0+rqjn3DDTUWXLGxAydcUNPdU0r8PSdp0ctAzhT5+zxHvFc1/IxiEw34G+gZhursdbReUoLeozGHjo6RLfwruDbIXNETB9fH22ho4rex7Fe6h42q80iGzku5R/rke8j31XowhTVhr8Jv/FtknVqb5IpIeIu7A1QD/MebugK0U14erB1PHrLoysgExSmuszKKkuorEKH2nqmV0SWUTGJ6XkXJzXfQv3M3ne+TLcPWTtwjcDeQcPMaidyk1xWewK+yv6dPuqLvuq7fuqXfoOvkiXxCrpw7xzbmQX6dxQ9wNv+Z7VKT3AWR1GHEYvU3fTexPiGeQctWwWf9SZlHWFNHV+G0/AD1GScxhyTF3neBGuJ/Tb7GPRSPo0tnpPe5yEzYBNQo5Mxb2OazFtGhy8QbauJrnPerRY85/20rKd2k5Z9ZzNvp1zj1C/yjais4f15gvcSFmOaWeoelaHaOO7jz1A7y2aq6qhKfXZtssZ+CwZkWqOOiZJEV/0GjH2q1lHQyG/kqs64+k5+UquOiz0qoBNRfpNT75lNch9tNhWjv/VfaSwAAAB4nGNgZGphnMDAysDAVMW0h4GBoQdCMz5gMGRkAooysDIzYAUBaa4pDA4Muh8NmF8AuVFgEqgRRAAA0p8KWwAAeJy11AduVDEUheF/SkILvbcUSK+T3ntlGaFHoUShR6ySndwNEM4dHxYQJCx9c8d+nvdsPZ8BWoCaDEsdqnUqZPut0UpzvMbF5nidX+q3cZ6qvo9wwBEn0Tg91egI+xxyHJVm72+raHYbj+hlRr01GqywwDqT7DDOEtMss8gGs7q+xQTzTLHJHnOsss2ufl/V0+u0ck5PvaB1XNL9LnOFq1zjOje4yS1uc4e73OM+D3iopz3WnHY66KSLp5rXTY9W0Ec/AwwypB21ab2jjGmBrZytrZ1xfrbGysL65M74kna7uDE7o43OT23uza1u7/7Dzf5Da8uP2k/3npFvs1iTfWvIc1uRF7YgL21dXtmkvLYdeWPj8taW5MCm5dCW5Z0tynvbkA82Kx8tT9eRbcmxTcgnm5fPNiVfbFO+2p58szn5bqvyw7blxPQCo1JkVqJakLVWZLaiXmS+oqXQmSZMp5swnXPCdOIJywyGKQWE5dsLUzIIU0YIU1oIUx4IU4IIU5YIU6oIU74IU9IIU+YIU/oIUw4JUyIJUzYJU0oJU14JU3IJa+6rvSBrR0HWzoKsXQVZnxRKPWHN/XYXZO0pyNpbkLWvIGt/QdaBgqyDBVmHCrIOF/kfGiNFc/2jRWYpxgpyrFEw9gcVSJ+veJzFV21MW+cVvue9xTcmEOKAffECduyLfWkwrsy1udgGwlcCisrHZCdaG1LTdIG0sBjaVF2WoX1okyK1ayqt9Efzp9GkizSpTVv+pNOUVCxVmfrHUfNrYmzqn+GsQ1RCreTGVzvnxTbhY1Kn/Rjc933PfT/OPee8z/mwAAL+sTWxR9gvCOCxV4LdE/HoqoetmRVQZX4FG+byvXvQIHYncrnEbCYjCI/RGfED8QPBIlQKNcJhPKlFNFXSJCUiyRHNp9oVu2ZXFbsi6wrcMNKLi+k26lj/Yjo9z9/Zz7CfJyqxmDbSOlLIlssj9qA8+wShWtZtmk3WYT6WTa7GzQXYiGfNMvg2G4dBFJvvnRPnUIYjeNQrWSRLjeyQHS16q94aVv2qv1pSJVVXdVmXJdiYeTalR6N66tkvikQq9tZbsbk53otz25Y4kf9zcRV74f/zTZF/s1asRUqie6rWbAo1XbMrmdnZB5cuZV9+mT1vTrCB/K1cTijJuSquCtWCk27WewDsthoXaJ6WTojYwkGwgi6DLF5++LVLc+EjlvMxv2Y+GFoCY0hcxfdtiw+/BsfQp2AMb+cv781fRCDt4v6b/H0W2M05f3/6UZ4i52nz2Hx78UWeYvkOvpuT6Z18C7b7sfhDoQwRVSsIVpBkK4i6Gyx0dfYaR0trhK6N/dNMxZ4z89C5tPTp+/v3O6scoeMhR5UTFuFGHBfMP0HH0lJnZaXrsN/pDYW8Tl+di9hbS5jdvJ8qbpPDgltQ6K4iCjUJWwSbjE23a5GywrwN27xh5PhTGM2xwhu7buTHcUwahQlcwafoJ+waW0O9BFRJQi95M3b7dizH1uJ37sRzJdz8XLyDmjuFen5LFjt6lEfmRI0DjdqKNvUr1R6bh71vViihJGzMmhFSDz7DXgEzAp+Jt0Pe/EesPxFS8h/RGutXQvnzrL/gr4RNih+Iey6KrKtibTybLT7sr4+8bPdxPGNTbKoEYjd3chgkJ4dvzbIsOvwOPZG7rrLXY7lc7DZbiOdy8TulmPEUWxXa8SXIFK9FUlu1FtkF8gFAUldwQpEtitevKi6mtehEq0HAJdkiabTZIWvsTSnYp3ri8uBTEyMDJ16ok0+1DfmsvZLt6FDfPqsnvK9Xaj6uNhzp7x3A9ck6//fPWB/71eP1jUfC1h5m83UoTseg5nZf7O845xn0abWHjh4H1e3vVKpOfI+mx7pGq2sa6/xFXJLMhxAnjTySoKwoMQaSTtBRNr/oDUK4E1pcUHMARK+FoBr2wwaXre6FEwPvIFFfj8RI4GQgcPIH1AUao9GRaJT9g77Xf9HtLo7mZGEHdiDSnpFoESOTaN9CbPEhHikHAHovRm7DXGZr+WuYBSpyCbiRSJjLyVJ8Yd/we7HSzeh4N4AxiX3z8cd4MWYkmYQIATFHZwu+3cNm+Hco03jKKD5058fZ24U2k5hP0DYL3/uMWI5UuWAT7IheN51BMzQBCaejTXCo5h6FmUdBM2msHxzo8vPgMB/MI2FWpOlvhYICDFI3ND29gmM6fz+dnqbpXXpUE3pVD+rxFVcDXkkm2QJXA5XYmZ8AEyVGURXV2IQrDBJ0mSsbMxfMhVi2aN859gnmi1ohgF9AUeMQUTCuYVTjVBBkNIUPU2aLi5GKSHQCRrrfedsVw4iOtbWNRQ1Daff+HobMD+EVToNSWoQNJa4Mt6VeTLUNK3EpaWB/gmbGZlJtxbhajvaUuR0xLWHikCXM0hFMVGRAHbO2qOkRVZHsLHB+ePyNN8aHz0+XiPHR7u7RbvaTbZNEmFe6aalgl0lxkjSuxtudRdgcEicTwh4Y01Fd/o93yPdtNiwvkplEouDza8I9OqkjLw6hDMfGnrxsBbzO0r4ivy24io/kbI8Q/A9ZG8JEUiCAIABGCkCM1dDGvXK42dMceHLwTPRCk9nsbYBBJOBzbwNN7pXSzdSZwScDzQ1es7npgvllgxc+b7oQ5XNFnaYwTjvwflSUTwkCxjBlR5D2tDhqLF5/uBU8Ng1V1sTam5VdI12VN3lIHqDeXLY5nV6nEyL5W0l4JylO+oJBH0bv83zPdW+oVqnFB210fXa2iI0usROlqKJMourVwKM41S8qnHlVffVvEHC51ttPr59qv+yCVpyBv5v3Xa7L7afWT7evu7bqjf+dj7VUW27mUI/gE5qEJwRN0DE2edBX0BSdVA24qC44AIBug/bw+PinsOTUfLz8onlyKR0bRRm3Vl+vudnbfMz/yzAQK33wR/OLY8fUiYmb+P6l2nWsccK8gqRjZWXFMNq2Drg5A7Ab8GvzynvHuujI/BNqFxLgp8RsbKu5aoTHiyg7SCg7QtA6SCgTvdRzqLXQSgFgVy/N9Pb19c5cgoNF6uqPzj4d0fXI02f/UiTEVVow17dvfvjTbZs4UcAUyVKBNmwlaXjA2Qws6n8hWt9Lvb0vXaWu77tJydYK+7HLfWeBd8VWSUbPx5zyaM3Pa4RsDIt/VvCZSfwF4tisFZWSr5TpHHY2dJD8rcSma5gLU7FMJjaVpB800GAu8xKnoW4qnsnEp+C3pZrqE3aWuAMWVA2ZDBulXzzFtbMYxxmXjebZ3b3WEOyvZTJ0VtjFEwtunKf1LX2fEyuKtQ2ocPHd+CpsxCDxbiyLY7G+Yq5iXM1Qjc9cyRLvNXZtswak4qiQq9jrVAlu1YBzaCfKoy0UWyjNgN1hkWQsqBEMEtakVCjZt/JRJIyFEW6U8dGB3d1MOPrRjvCxtrFzo8l9+Vu/jA0Us1JP6LT7xdTAfDjfz+5i6lECx/d3HD7XHk21GUmY+kXt811RnpmU0Mmy08lncCf7gyD8G975gHwAAAB4nGNgZGBgAOLNefk58fw2Xxm4mV8ABaI4H+9rQNAMDMwvQOIMHAxMIB4AThwLfgB4nGNgZGBgfsHAgEQyMqACTwBFgwMNAAB4nGNgYGBgfjF0MT0AAODAKyYAAHicY2AAAikGD4Y4hhkMlxjeMMowujEWMC5ivMP4jkmByYopgCmJaQ8zA7MMswmzF3MD8wzmDcyfWFRYjFi8WCJYilgOsTKwqrB6sC5jfcPmxhbD1sY2hW0R2wa2fWwX2O6xfWC3Ix0CAAMJHZwAAAB4nGNgZGBg8GSwZeBkAAEmIOYCQgaG/2A+AwAWwQGmAHicfZA9asNAEIWf/BdiQwiEuFKxEEgTkH9Kk9qCgBsX7mVrJcustWK1NvgGOUhOkEOkzEFyijytt7ELz8LwvTdvplgAj/hBgKYCPLjeVAt3VGduk549d8gvnrsYYOK5R//dcx9v+PA8wBMOvBB07umE+PTcYv7Lc5v+t+cO+ddzF0P8ee4hDOC5j1UQeh7gNTBJHpsiXSRWmiJRS5kfVHJlXqqVNHWhSzGJxpeDWJbSkFOxPon6mE+tzURm9F7MdWmlUlpURu/kxkZba6vZaJR5P9roPRLkiGFQIMWCykI6lUBhSc75F4rK3Ezemq0c11QaJQT/P8L45kZMLp0++ym31jix1zhyb0rXIqPOmNHYk+buepNWfJpO5WY7Ohv6EbZuq8IMI77sKh8xxUv/NPJv3gAAeJxtkQdv2zAQhf3Fki3baeO6bbr3Hmqb7r1X+i9oipaJSKRAUR759WXtIECAHnDgew+Hd4OtjdY6+q3/xy4btImI6dAloUefAZsc4zhbDDnBiJOc4jTbnOEs5zjPBS5yictc4SrXuM4NbnKL29zhLve4zwMekvKIxzxhh6c84zkveMkrXvOGt7zjPR/4yCc+84WvfOM7P/jJL36zy5/WQOS5U7nw2pqOcM7O67aoZUcKI1WRrJ90J5ZT4fxQTpXcG9tFugIq2z4UtMmUV67URng1OpQbc1C5KW1hXVrpQFw3kKY0dV9a452QXmWRtNUyls7WdZSpWiZqUYngmfXUUqV1IeppO6DORBehTTzRrvZR7nQV5842VRQKfFSoie8U2oR+3cKKTJs8KcVCl3pfRaUyTRLmWzNjjRoY61NRFHausrgKBqpdaROHDAtXemb90AUTm44b761J7WSydVQwsdP51Ee1mKl+XQavNLNzcwD/jTNYw1VZssZhWh/OMPJOqaN3660kWymTNGa9B4Ich8KjsRjm1EgKpuwxZhE+P6OkoWLJhH1mrdZfDrCtIAAA") format("woff");
}

${rootSelector} {
    --ag-header-background-color: #ecf0f3;
    --ag-row-border-color: #ecf0f3;
    --ag-border-color: transparent;

    width: 100%;
    height: 100%;
}
${rootSelector} .ag-root {
    width: 100% !important;
    border: none !important;
    font-family: ${theme.font.display} !important;
}

${rootSelector} .ag-cell.ag-cell-focus {
    border-color: ${theme.color.primary} !important;
}

/* make wrapper.ag-grid-container has the same width as the column width to easily align content inside */
${rootSelector} .ag-header-cell .ag-react-container {
    width: 100%;
    height: 100%;
}

/* workaround: custom cell renderer will have duplicated content in 1 frame if
the cell is refreshed. To be able to hide the duplicated one, add a root wrapper
in your cell renderer (e.g. don't use React.Fragment as root component)
*/
${rootSelector} .ag-cell .ag-react-container > span:not([class]) ~ * {
    display: none;
}
${rootSelector} .ag-cell .ag-react-container {
    display: flex; /* vertically center custom components like checkbox */
    justify-content: center;
    width: 100%;
    /* images height should not overflow cell container */
    height: 100%;
    /* vertical center custom text renderer like ValueWithUnitRenderer */
    align-items: center;
}
${rootSelector} .ag-cell.ag-right-aligned-cell {
    justify-content: right;
    font-family: ${theme.font.number};
}
${rootSelector} .ag-right-aligned-cell, ${rootSelector} .ag-right-aligned-cell .ag-react-container {
    justify-content: right; /* text-align not working since we use flex here */
}

${rootSelector} .ag-cell {
    display: flex;
    /* vertically align content because custom height is larger than normal */
    align-items: center;
}

${rootSelector} .ag-root-wrapper-body {
    /* without this line, the table will be collapsed */
    height: 100% !important;
}

/* theming */

${rootSelector} .m0 { margin: 0 !important; }
${rootSelector} .p0 { padding: 0 !important; }
${rootSelector} .unit {
    color: #a4a4a4;
    font-family: ${theme.font.display};
}

${rootSelector} .ag-root-wrapper {
    border: solid 1px var(--ag-row-border-color) !important;
}

${rootSelector} .ag-root-wrapper .ag-header {
    border-bottom: 1px solid var(--ag-row-border-color);
}

${rootSelector} .ag-root-wrapper .ag-header-cell::after {
    /* remove divider between headers */
    background-color: transparent;
}
${rootSelector} .ag-root-wrapper.ag-keyboard-focus .ag-header-cell:focus::after {
    /* make focus border stretch 100% */
    width: 100%;
    height: 100%;
    top: 0;
    left: 0;
}

${rootSelector} .ag-root-wrapper .ag-cell.ag-cell-dirty {
    background-color: rgba(255, 193, 7, .5);
}

${rootSelector} .ag-cell-inline-editing {
    height: ${theme.tableRowHeight}px !important;
    border-radius: 0 !important;
}

/* ----- row stylings ----- */

${rootSelector} .ag-row {
    transition: background-color .3s !important;
}

/* action buttons only show its true color when hovering. We don't want to
distract user from more important things in the table */
${rootSelector} .ag-row:not(.ag-row-hover) .actions svg {
    color: ${theme.color.grey2};
    transition: color .2s;
}

${rootSelector} .ag-row.ag-row-selected {
    background-color: ${theme.color.primaryLight};
}

/* ----- pinned columns ----- */

/* pinned header */
${rootSelector} .ag-pinned-left-header,
${rootSelector} .ag-pinned-right-header {
    /* elevation effect to differentiate between pinned columns and normal ones */
    box-shadow: ${theme.shadow.light};
}
${rootSelector} .ag-pinned-left-header[ref='ePinnedLeftHeader'] {
    /* remove the white border (use box-shadow as soft border) */
    border-right: none;
}
${rootSelector} .ag-pinned-right-header[ref='ePinnedRightHeader'] {
    border-left: none;
}

/* pinned content */
${rootSelector} .ag-pinned-left-cols-container,
${rootSelector} .ag-pinned-right-cols-container {
    z-index: 1;
    box-shadow: ${theme.shadow.light};
}

/* ag input */
${rootSelector} .ag-right-aligned-cell .ag-text-field-input {
    text-align: right;
}

${rootSelector} .ag-center-cols-container {
    /* we don't want to see blank space if there is few columns, fill the rest of the table with the same background color */
    width: 1000% !important;
}

/* ----ag material checkbox---- */

/* cell wrapper contain selection id and its normal content */
${rootSelector} .ag-cell-wrapper {
    width: 100%;
}
${rootSelector} .ag-checkbox-input-wrapper {
    font-family: 'agGridMaterial';
    margin-left: 17px;
}
${rootSelector} .ag-checkbox-input-wrapper:focus-within {
    box-shadow: none;
}
${rootSelector} .ag-checkbox-input-wrapper.ag-checked::after {
    color: ${theme.color.secondary};
}

/* ----image cell renderer ---- */
${rootSelector} .ag-react-container img {
    object-fit: contain;
    max-height: ${theme.tableRowHeight}px;
    cursor: pointer;
    mix-blend-mode: multiply;
}
`;
