import React, { useEffect, useRef } from "react";
import { IconButton } from "@material-ui/core";
import DeleteIconMaterial from "@material-ui/icons/Delete";
import EditIconMaterial from "@material-ui/icons/Edit";
import { AgGridReact, AgGridColumn } from "ag-grid-react";
import {
  GridApi,
  ColumnApi,
  GridReadyEvent,
  ValueFormatterParams,
  ICellRendererParams,
} from "ag-grid-community";
import styled from "styled-components";
import classNames from "classnames";
import { Checkbox } from "@material-ui/core";
import { actions } from "./product.duck";
import { useSelector, useDispatch, shallowEqual } from "../../../store/store";
import { useEffectOnce } from "../helpers/hookHelpers";
import toMap from "../helpers/toMap";
import theme from "../../../styles/theme";
import Product from "./product.model";
import { ColumnInfo } from "./product.duck.d";
import { BaseColDefParams } from "ag-grid-community/dist/lib/entities/colDef";

// TODO: currency locale
function formatNumber(number) {
  return Math.floor(number)
    .toString()
    .replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
function currencyFormatter(params: ValueFormatterParams) {
  return formatNumber(params.value) + "đ";
}

function markAsDirty(params: ICellRendererParams) {
  params.colDef.cellClass = (p) =>
    p.rowIndex.toString() === params.node.id ? "ag-cell-dirty" : "";
  params.api.refreshCells({
    columns: [params.column.getId()],
    rowNodes: [params.node],
    force: true, // without this line, the cell style is not refreshed at the first time
  });
}

const CellCheckbox = styled(Checkbox)`
  padding: 0 !important;
`;
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

const EditIcon = styled(EditIconMaterial)`
  color: ${theme.color.blue};
`;
const DeleteIcon = styled(DeleteIconMaterial)`
  color: ${theme.color.danger};
`;
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

function getColumn(
  columnName: string,
  productCategories: { [key: string]: string }
) {
  switch (columnName) {
    case "id":
      return (
        <AgGridColumn
          key={columnName}
          headerName="ID"
          lockPosition
          field={columnName}
          resizable={false}
          type="numericColumn"
        />
      );
    case "name":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Tên"
          field={columnName}
          type={["editable"]}
        />
      );
    case "description":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Mô tả"
          field={columnName}
          type={["editable", "largeText"]}
        />
      );
    case "content":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Nội dung"
          field={columnName}
          type={["editable", "largeText"]}
        />
      );
    case "weight":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Khối lượng"
          field={columnName}
          type={["editable", "numericColumn"]}
        />
      );
    case "category": {
      return (
        <AgGridColumn
          key={columnName}
          headerName="Loại"
          field={columnName}
          type={["editable"]}
          cellEditor="agSelectCellEditor"
          cellEditorParams={{
            values: Object.keys(productCategories),
          }}
          refData={productCategories}
        />
      );
    }
    case "numberOfVersions":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Số phiên bản"
          field={columnName}
          type={["editable", "numericColumn"]}
        />
      );
    case "price":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Giá"
          field={columnName}
          type={["editable", "numericColumn", "currency"]}
        />
      );
    case "originalPrice":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Giá gốc"
          field={columnName}
          type={["editable", "numericColumn", "currency"]}
        />
      );
    case "discountPrice":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Giá khuyến mãi"
          field={columnName}
          type={["editable", "numericColumn", "currency"]}
        />
      );
    case "quantity":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Số lượng"
          field={columnName}
          type={["editable", "numericColumn"]}
        />
      );
    case "display":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Hiển thị"
          field={columnName}
          type={["checkbox"]}
        />
      );
    case "deliver":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Giao hàng"
          field={columnName}
          type={["checkbox"]}
        />
      );
    case "applyPromotion":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Khuyến mãi"
          field={columnName}
          type={["checkbox"]}
        />
      );
    case "action":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Tùy chọn"
          field={columnName}
          cellRenderer="actionRenderer"
        />
      );
    default:
      console.log(columnName);
  }
}

type ProductTableProps = {
  className?: string;
  columnInfos: ColumnInfo[];
};

function autoSizeAllColumns(gridColumnApi?: ColumnApi) {
  const allColumnIds =
    gridColumnApi?.getAllColumns().map((c) => c.getId()) || [];
  gridColumnApi?.autoSizeColumns(allColumnIds, false);
}

export default function ProductTable(props: ProductTableProps) {
  const { className, columnInfos, ...rest } = props;
  const lastQuery = useSelector((state) => state.products.lastQuery);
  const products = useSelector<Product[]>(
    (state) =>
      state.products.cachedQueries[lastQuery]?.map((p) => {
        p.category = p.category.toString(); // TODO: fix category type
        return p;
      }),
    shallowEqual
  );
  let productCategories = useSelector(
    (state) => toMap(state.products.productCategories, "id", "name"),
    shallowEqual
  );
  const dispatch = useDispatch();
  const gridApiRef = useRef<GridApi>();
  const gridColumnApiRef = useRef<ColumnApi>();

  useEffectOnce(() => {
    dispatch(actions.getCategoriesRequest());
  });

  useEffect(() => {
    dispatch(actions.getAllRequest("/"));
    // console.log(products, lastQuery);
    // if (products) {
    //   setTimeout(() => {
    //     // dispatch(actions.getAllRequest("/"));
    //     const productToUpdate = products[3];
    //     productToUpdate.numberOfVersions = 12;
    //     gridApiRef.current?.applyTransaction({ update: [productToUpdate] });
    //   }, 5000);
    // }
  }, [dispatch, lastQuery]);

  useEffect(() => autoSizeAllColumns(gridColumnApiRef.current), [columnInfos]);

  const onFirstDataRendered = () =>
    autoSizeAllColumns(gridColumnApiRef.current);
  const onGridReady = (params: GridReadyEvent) => {
    gridApiRef.current = params.api;
    gridColumnApiRef.current = params.columnApi;

    window.addEventListener("resize", function() {
      setTimeout(() => autoSizeAllColumns(gridColumnApiRef.current));
    });
  };

  return (
    <div className={classNames("ag-theme-balham table-wrapper", className)}>
      <AgGridReact
        enableColResize
        rowHeight={theme.tableRowHeight}
        headerHeight={45}
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
          },
        }}
        onFirstDataRendered={onFirstDataRendered}
        defaultColDef={{
          sortable: true,
        }}
        onGridReady={onGridReady}
        rowData={products}
        // getRowClass={this.getRowClass}
        frameworkComponents={{
          checkboxRenderer,
          actionRenderer,
        }}
        {...rest}
      >
        {columnInfos
          .filter((c) => c.visible)
          .map((c) => getColumn(c.columnName, productCategories))}
      </AgGridReact>
    </div>
  );
}
