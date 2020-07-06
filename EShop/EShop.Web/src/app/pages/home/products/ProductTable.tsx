import React, { useEffect, useRef, useCallback } from "react";
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
  ColumnPinnedEvent,
  ColDef,
} from "ag-grid-community";
import classNames from "classnames";
import { Checkbox } from "@material-ui/core";
import { actions } from "./product.duck";
import { useSelector, useDispatch, shallowEqual } from "../../../store/store";
import { useOnMount } from "../helpers/hookHelpers";
import { autoSizeColumns, useGridApi } from "../helpers/agGridHelpers";
import Product from "./product.model";
import ProductTableHeader from "./ProductTableHeader";
import { Pinned } from "./product.duck.d";
import styled, { important, theme } from "../../../styles/styled";
import useColumnDefs from "./useColumnDefs";

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

const columnTypes: Record<string, ColDef> = {
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
};

type ProductTableProps = {
  className?: string;
};
export default function ProductTable(props: ProductTableProps) {
  const { className, ...rest } = props;
  const products = useSelector<Product[]>(
    (state) =>
      // TODO: fix category type
      state.products.products?.map((p) => ({
        ...p,
        category: p.category.toString(),
      })),
    shallowEqual
  );
  const onGridReadyCb = useCallback(() => {
    return (_: any, colApi?: ColumnApi) => {
      document
        .getElementById("productTableContainer")
        ?.addEventListener("resize", function() {
          setTimeout(() => autoSizeColumns(colApi));
        });
    };
  }, []);
  const [gridApiRef, columnApiRef, onGridReady] = useGridApi(onGridReadyCb);
  const onFirstDataRendered = () => autoSizeColumns(columnApiRef.current);
  const symbol = useSelector((state) => state.products.currency?.symbol) ?? "";
  const dispatch = useDispatch();

  useOnMount(() => {
    dispatch(actions.getCategoriesRequest());
    dispatch(actions.getAllRequest());
  });

  const [columnDefs, columnInfos] = useColumnDefs(columnApiRef.current);

  useEffect(() => {
    // refresh to update valueFormatter to display latest currency format
    // valueFormatter is only registered once on mount so we have to use module-scope variable
    // which is referenced by valueFormatter.
    SYMBOL = symbol;
  }, [symbol]);

  const onColumnMoved = (e: ColumnMovedEvent) => {
    if (e.columns !== null && e.toIndex !== undefined) {
      const { toIndex } = e;
      const order: Record<string, number> = {};
      const columnOrder = columnApiRef.current
        ?.getColumnState()
        // remove suffix _[digit]. field: id -> colId: id_1
        .map((c, i) => (order[c.colId.replace(/_[\d]+$/, "")] = i));
      if (columnOrder) dispatch(actions.setColumnOrder(order));

      for (let col of e.columns.reverse()) {
        const column = col.getColDef().field!;
        const fromIndex = columnInfos.findIndex((c) => c.field === column);
        console.log(column, fromIndex, toIndex);
      }
    }
  };
  const onColumnPinned = (e: ColumnPinnedEvent) => {
    if (e.columns !== null && e.pinned !== null) {
      const pinned = e.pinned as Pinned;
      for (let col of e.columns) {
        const column = col.getColDef().field!;
        dispatch(actions.setPinned({ column, pinned }));
      }
    }
  };

  return (
    <div className={classNames("ag-theme-balham table-wrapper", className)}>
      <AgGridReact
        // animateRows
        onColumnMoved={onColumnMoved}
        onColumnPinned={onColumnPinned}
        rowHeight={theme.tableRowHeight}
        headerHeight={45}
        // you can already toggle show/hide columns. dragging outside to hide
        // column just makes it more confusing
        suppressDragLeaveHidesColumns
        columnDefs={columnDefs}
        columnTypes={columnTypes}
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
      />
    </div>
  );
}
