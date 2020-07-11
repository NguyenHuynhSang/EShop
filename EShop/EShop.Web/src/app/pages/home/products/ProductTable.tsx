import React, { useEffect } from "react";
import { IconButton } from "@material-ui/core";
import DeleteIconMaterial from "@material-ui/icons/Delete";
import EditIconMaterial from "@material-ui/icons/Edit";
import { AgGridReact } from "ag-grid-react";
import has from "lodash/has";
import {
  ICellRendererParams,
  ColumnPinnedEvent,
  ColDef,
  DragStoppedEvent,
  ValueFormatterParams,
} from "ag-grid-community";
import classNames from "classnames";
import { Checkbox } from "@material-ui/core";
import { actions, Pinned } from "./product.duck";
import { useSelector, useDispatch, shallowEqual } from "../../../store/store";
import { useOnMount } from "../helpers/hookHelpers";
import { useGridApi } from "../helpers/agGridHelpers";
import ProductTableHeader from "./ProductTableHeader";
import { AgSelect } from "../../../widgets/Common";
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

type ValueWithUnit = {
  prefixUnit: boolean;
  value: number;
  unit: string;
};

let SYMBOL = "";
const currencyFormatter = (params: ValueFormatterParams) => {
  const value = formatNumber(params.data[params.colDef.field!]);
  const unit = SYMBOL;
  return { value, unit, prefixUnit: !has(suffixCurrencyCode, unit) } as any;
};
let WEIGHT_UNIT = "kg";
const weightFormatter = (params: ValueFormatterParams) => {
  const value = params.data[params.colDef.field!];
  const unit = WEIGHT_UNIT;
  return { value, unit, prefixUnit: false } as any;
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
function actionRenderer() {
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
function SelectRenderer(params: ICellRendererParams) {
  const options = useSelector(
    (state) => state.products.categories,
    shallowEqual
  );
  const value = options.find((o) => o.value === parseInt(params.value.id, 10));
  return (
    <AgSelect
      options={options}
      placeholder="Category"
      defaultValue={value}
      isSearchable={false}
      onChange={(e: any) => {
        // TODO: immer doesn't like products.category being mutated
        // params.setValue({ id: e.value, name: e.label });
        markAsDirty(params);
      }}
    />
  );
}

function numberWithUnitRenderer(params: ICellRendererParams) {
  const val = params.valueFormatted as ValueWithUnit;
  const comp = [
    val.value,
    <span key="unit" className="unit">
      {val.unit}
    </span>,
  ];

  if (val.prefixUnit) comp.reverse();
  return <span>{comp}</span>;
}

const columnTypes: Record<string, ColDef> = {
  editable: {
    editable: true,
    onCellValueChanged: markAsDirty,
  },
  currency: {
    // type: 'numericColumn' not working here
    cellClass: "ag-right-aligned-cell",
    cellRenderer: "numberWithUnitRenderer",
    valueFormatter: currencyFormatter,
  },
  weight: {
    cellClass: "ag-right-aligned-cell",
    cellRenderer: "numberWithUnitRenderer",
    valueFormatter: weightFormatter,
  },
  checkbox: {
    cellRenderer: "checkboxRenderer",
  },
  selector: {
    // remove padding so select's width is the same as container width
    cellClass: "p0",
    cellRenderer: "SelectRenderer",
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
  const products = useSelector<any[]>(
    (state) => state.products.products,
    shallowEqual
  );
  const [api, onGridReady, autoSizeColumns] = useGridApi();
  const [columnDefs] = useColumnDefs(api.column);
  const onFirstDataRendered = () => autoSizeColumns();
  const symbol = useSelector((state) => state.products.currency?.symbol) ?? "";
  const dispatch = useDispatch();

  useOnMount(() => {
    dispatch(actions.getCategoriesRequest());
    dispatch(actions.getAllRequest());
  });

  useEffect(() => {
    // refresh to update valueFormatter to display latest currency format
    // valueFormatter is only registered once on mount so we have to use module-scope variable
    // which is referenced by valueFormatter.
    SYMBOL = symbol;
  }, [symbol]);

  const onColumnPinned = (e: ColumnPinnedEvent) => {
    if (e.columns !== null && e.pinned !== null) {
      const pinned = e.pinned as Pinned;
      for (let col of e.columns) {
        const column = col.getColDef().field!;
        dispatch(actions.setPinned({ column, pinned }));
      }
    }
  };
  // has better performance than onColumnMoved: https://stackoverflow.com/a/57287276/9449426
  const onDragStopped = (e: DragStoppedEvent) => {
    const columnOrder = e.columnApi
      ?.getColumnState()
      // remove suffix _[digit]. field: id -> colId: id_1
      .map((c) => c.colId.replace(/_[\d]+$/, ""));
    if (columnOrder) dispatch(actions.setColumnOrder(columnOrder));
  };

  return (
    <div className={classNames("ag-theme-balham table-wrapper", className)}>
      <AgGridReact
        // animateRows
        onDragStopped={onDragStopped}
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
          SelectRenderer,
          numberWithUnitRenderer,
          agColumnHeader: ProductTableHeader,
        }}
        {...rest}
      />
    </div>
  );
}
