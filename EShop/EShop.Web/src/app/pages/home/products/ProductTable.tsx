import React, { useEffect, useRef } from "react";
import { IconButton } from "@material-ui/core";
import DeleteIconMaterial from "@material-ui/icons/Delete";
import EditIconMaterial from "@material-ui/icons/Edit";
import { AgGridReact } from "ag-grid-react";
import has from "lodash/has";
import {
  GridApi,
  ColumnApi,
  GridReadyEvent,
  ValueFormatterParams,
  ICellRendererParams,
  ColumnMovedEvent,
} from "ag-grid-community";
import classNames from "classnames";
import { Checkbox } from "@material-ui/core";
import { actions } from "./product.duck";
import { useSelector, useDispatch, shallowEqual } from "../../../store/store";
import { useEffectOnce } from "../helpers/hookHelpers";
import moveArrayItem from "../helpers/moveArrayItem";
import Product from "./product.model";
import ProductTableHeader from "./ProductTableHeader";
import ProductTableColumn from "./ProductTableColumn";
import { ColumnInfo } from "./product.duck.d";
import styled, { important, theme } from "../../../styles/styled";

// TODO: use intl https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Intl/NumberFormat
function formatNumber(number: number) {
  return Math.floor(number)
    .toString()
    .replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
const suffixCurrencyCode = {
  đ: true,
};
let SYMBOL = "";
const currencyFormatter = (params: ValueFormatterParams) => {
  const symbol = SYMBOL;
  if (symbol === undefined) return formatNumber(params.value);
  if (has(suffixCurrencyCode, symbol))
    return formatNumber(params.value) + symbol;
  return symbol + formatNumber(params.value);
};

function markAsDirty(params: ICellRendererParams) {
  params.colDef.cellClass = (p) =>
    p.rowIndex.toString() === params.node.id ? "ag-cell-dirty" : "";
  params.api.refreshCells({
    columns: [params.column.getId()],
    rowNodes: [params.node],
    force: true, // without this line, the cell style is not refreshed at the first time
  });
}

const CellCheckbox = styled(Checkbox)({
  padding: important(0),
});
function checkboxRenderer(params: ICellRendererParams) {
  return (
    <CellCheckbox
      checked={params.value}
      onChange={(e) => {
        // mark as dirty visually
        markAsDirty(params);
        params.setValue(e.target.checked);
      }}
    />
  );
}

const EditIcon = styled(EditIconMaterial)({
  color: theme.color.blue,
});
const DeleteIcon = styled(DeleteIconMaterial)({
  color: theme.color.danger,
});
function actionRenderer(params: ICellRendererParams) {
  return (
    <div>
      <IconButton>
        <EditIcon />
      </IconButton>
      <IconButton>
        <DeleteIcon />
      </IconButton>
    </div>
  );
}
type ProductTableProps = {
  className?: string;
  columnInfos: ColumnInfo[];
};

function autoSizeAllColumns(gridColumnApi?: ColumnApi) {
  const allColumnIds =
    gridColumnApi?.getAllColumns().map((c) => c.getId()) || [];
  gridColumnApi?.autoSizeColumns(allColumnIds, false);
  // when using custom header component, autosize does not work on the first try
  // especially when there too many columns to fit on one screen
  setTimeout(() => {
    gridColumnApi?.autoSizeColumns(allColumnIds, false);
  });
}
export default function ProductTable(props: ProductTableProps) {
  const { className, columnInfos, ...rest } = props;
  const products = useSelector<Product[]>(
    (state) =>
      state.products.products?.map((p) => {
        p.category = p.category.toString(); // TODO: fix category type
        return p;
      }),
    shallowEqual
  );
  const productCategories = useSelector(
    (state) => state.products.productCategories,
    shallowEqual
  );
  const symbol = useSelector((state) => state.products.currency?.symbol) ?? "";
  const dispatch = useDispatch();
  const gridApiRef = useRef<GridApi>();
  const gridColumnApiRef = useRef<ColumnApi>();

  useEffectOnce(() => {
    dispatch(actions.getCategoriesRequest());
    dispatch(actions.getAllRequest());
  });

  useEffect(() => {
    autoSizeAllColumns(gridColumnApiRef.current);
  }, [columnInfos]);

  useEffect(() => {
    // refresh to update valueFormatter to display latest currency format
    // valueFormatter is only registered once on mount so we have to use module-scope variable
    // which is referenced by valueFormatter.
    SYMBOL = symbol;
  }, [symbol]);

  const onFirstDataRendered = () => {
    autoSizeAllColumns(gridColumnApiRef.current);
  };
  const onGridReady = (params: GridReadyEvent) => {
    gridApiRef.current = params.api;
    gridColumnApiRef.current = params.columnApi;

    document
      .getElementById("productTableContainer")
      ?.addEventListener("resize", function() {
        setTimeout(() => autoSizeAllColumns(gridColumnApiRef.current));
      });
  };
  const onColumnMoved = (e: ColumnMovedEvent) => {
    if (e.columns !== null && e.toIndex !== undefined) {
      for (let column of e.columns.reverse()) {
        const fromIndex = columnInfos.findIndex(
          (c) => c.columnName === column.getColDef().field
        );
        dispatch(
          actions.setColumnDisplay(
            moveArrayItem(columnInfos, fromIndex, e.toIndex)
          )
        );
      }
    }
  };

  return (
    <div className={classNames("ag-theme-balham table-wrapper", className)}>
      <AgGridReact
        animateRows
        onColumnMoved={onColumnMoved}
        rowHeight={theme.tableRowHeight}
        headerHeight={45}
        // you can already toggle show/hide columns. dragging outside to hide column just makes it more confusing
        suppressDragLeaveHidesColumns
        columnTypes={{
          editable: {
            editable: true,
            onCellValueChanged: markAsDirty,
          },
          currency: {
            valueFormatter: currencyFormatter,
          },
          checkbox: {
            cellRenderer: "checkboxRenderer",
          },
          largeText: {
            cellEditor: "agLargeTextCellEditor",
            maxWidth: 250,
            sortable: false,
          },
        }}
        onFirstDataRendered={onFirstDataRendered}
        defaultColDef={{
          sortable: true,
          resizable: true,
        }}
        onGridReady={onGridReady}
        rowData={products}
        // getRowClass={this.getRowClass}
        frameworkComponents={{
          checkboxRenderer,
          actionRenderer,
          agColumnHeader: ProductTableHeader,
        }}
        {...rest}
      >
        {columnInfos.map((columnInfo) => {
          return ProductTableColumn({ columnInfo, productCategories });
        })}
      </AgGridReact>
    </div>
  );
}
