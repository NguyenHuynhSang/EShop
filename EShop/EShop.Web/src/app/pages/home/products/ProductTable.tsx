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
import { Checkbox } from "@material-ui/core";
import { actions } from "./product.duck";
import { useSelector, useDispatch, shallowEqual } from "../../../store/store";
import { useEffectOnce } from "../helpers/hookHelpers";
import toMap from "../helpers/toMap";
import theme from "../../../styles/theme";
import Product from "./product.model";
import { ColumnInfo } from "./product.duck.d";

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
function displayRenderer(params: ICellRendererParams) {
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
          headerName="ID"
          field="id"
          resizable={false}
          type="numericColumn"
        />
      );
    case "name":
      return <AgGridColumn headerName="Tên" field="name" type={["editable"]} />;
    case "description":
      return (
        <AgGridColumn
          headerName="Mô tả"
          field="description"
          type={["editable"]}
        />
      );
    case "content":
      return (
        <AgGridColumn
          headerName="Nội dung"
          field="content"
          type={["editable"]}
        />
      );
    case "weight":
      return (
        <AgGridColumn
          headerName="Khối lượng"
          field="weight"
          type={["editable", "numericColumn"]}
        />
      );
    case "category": {
      return (
        <AgGridColumn
          headerName="Loại"
          field="category"
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
          headerName="Số phiên bản"
          field="numberOfVersions"
          type={["editable", "numericColumn"]}
        />
      );
    case "price":
      return (
        <AgGridColumn
          headerName="Giá"
          field="price"
          type={["editable", "numericColumn", "currency"]}
        />
      );
    case "originalPrice":
      return (
        <AgGridColumn
          headerName="Giá gốc"
          field="originalPrice"
          type={["editable", "numericColumn", "currency"]}
        />
      );
    case "discountPrice":
      return (
        <AgGridColumn
          headerName="Giá khuyến mãi"
          field="discountPrice"
          type={["editable", "numericColumn", "currency"]}
        />
      );
    case "quantity":
      return (
        <AgGridColumn
          headerName="Số lượng"
          field="quantity"
          type={["editable", "numericColumn"]}
        />
      );
    case "display":
      return (
        <AgGridColumn
          headerName="Hiển thị"
          field="display"
          type={["checkbox"]}
        />
      );
    case "deliver":
      return (
        <AgGridColumn
          headerName="Giao hàng"
          field="deliver"
          type={["checkbox"]}
        />
      );
    case "applyPromotion":
      return (
        <AgGridColumn
          headerName="Khuyến mãi"
          field="applyPromotion"
          type={["checkbox"]}
        />
      );
    case "action":
      return (
        <AgGridColumn
          headerName="Tùy chọn"
          field="action"
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

  useEffect(() => {
    gridApiRef.current?.sizeColumnsToFit();
  }, [columnInfos]);

  const onFirstDataRendered = () => gridApiRef.current?.sizeColumnsToFit();
  const onGridReady = (params: GridReadyEvent) => {
    gridApiRef.current = params.api;
    gridColumnApiRef.current = params.columnApi;

    window.addEventListener("resize", function() {
      setTimeout(function() {
        gridApiRef.current?.sizeColumnsToFit();
      });
    });
  };

  return (
    <div className={`ag-theme-balham table-wrapper ${className}`}>
      <AgGridReact
        enableColResize
        rowHeight={theme.tableRowHeight}
        headerHeight={45}
        columnTypes={{
          editable: {
            editable: true,
            onCellValueChanged: markAsDirty,
          },
          currency: {
            valueFormatter: currencyFormatter,
          },
          checkbox: {
            cellRenderer: "displayRenderer",
          },
        }}
        onFirstDataRendered={onFirstDataRendered}
        defaultColDef={{
          sortable: true,
        }}
        onGridReady={onGridReady}
        rowData={products}
        // getRowClass={this.getRowClass}
        domLayout="print"
        frameworkComponents={{
          displayRenderer,
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
