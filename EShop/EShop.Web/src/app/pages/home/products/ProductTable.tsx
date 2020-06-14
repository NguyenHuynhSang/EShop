import React, { useEffect, useRef } from "react";
import { useDispatch, shallowEqual } from "react-redux";
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
import { useSelector } from "../../../store/store";
import { useEffectOnce } from "../helpers/hookHelpers";
import toMap from "../helpers/toMap";
import theme from "../../../styles/theme";
import Product from "./product.model";

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

export default function ProductTable(props) {
  const { className, ...rest } = props;
  const lastQuery = useSelector((state) => state.products.lastQuery);
  const products = useSelector<Product[]>(
    (state) =>
      state.products.cachedQueries[lastQuery]?.map((p) => {
        p.category = p.category.toString();
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
    <>
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
          <AgGridColumn
            headerName="ID"
            field="id"
            width={40}
            resizable={false}
            type="numericColumn"
          />
          <AgGridColumn
            headerName="Tên"
            field="name"
            width={350}
            type={["editable"]}
          />
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
          <AgGridColumn
            headerName="Số phiên bản"
            field="numberOfVersions"
            maxWidth={112}
            type={["editable", "numericColumn"]}
          />
          <AgGridColumn
            headerName="Giá"
            field="price"
            maxWidth={115}
            type={["editable", "numericColumn"]}
            valueFormatter={currencyFormatter}
          />
          <AgGridColumn
            headerName="Số lượng"
            field="quantity"
            maxWidth={115}
            type={["editable", "numericColumn"]}
          />
          <AgGridColumn
            headerName="Hiển thị"
            field="display"
            maxWidth={80}
            cellRenderer="displayRenderer"
          />
          <AgGridColumn
            headerName="Tùy chọn"
            field="action"
            cellRenderer="actionRenderer"
          />
        </AgGridReact>
      </div>
    </>
  );
}
