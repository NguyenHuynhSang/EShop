import React from "react";
import { AgGridColumn } from "ag-grid-react";
import { ColumnInfo } from "./product.duck.d";
import { useSelector, shallowEqual } from "../../../store/store";
import toMap from "../helpers/toMap";
import { ProductCategory } from "./product.model";

function getColumn(
  columnInfo: ColumnInfo,
  productCategories: ProductCategory[]
) {
  const { columnName, visible } = columnInfo;
  switch (columnName) {
    case "id":
      return (
        <AgGridColumn
          key={columnName}
          headerName="ID"
          lockPosition
          field={columnName}
          pinned
          resizable={false}
          type="numericColumn"
          hide={!visible}
        />
      );
    case "name":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Tên"
          pinned
          field={columnName}
          type={["editable"]}
          hide={!visible}
        />
      );
    case "description":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Mô tả"
          field={columnName}
          type={["editable", "largeText"]}
          hide={!visible}
        />
      );
    case "content":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Nội dung"
          field={columnName}
          type={["editable", "largeText"]}
          hide={!visible}
        />
      );
    case "weight":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Khối lượng"
          field={columnName}
          type={["editable", "numericColumn"]}
          hide={!visible}
        />
      );
    case "category": {
      const categoryData = toMap(productCategories, "id", "name");
      return (
        <AgGridColumn
          key={columnName}
          headerName="Loại"
          field={columnName}
          type={["editable"]}
          cellEditor="agSelectCellEditor"
          cellEditorParams={{
            values: Object.keys(categoryData),
          }}
          refData={categoryData}
          hide={!visible}
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
          hide={!visible}
        />
      );
    case "price":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Giá"
          field={columnName}
          type={["editable", "numericColumn", "currency"]}
          hide={!visible}
        />
      );
    case "originalPrice":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Giá gốc"
          field={columnName}
          type={["editable", "numericColumn", "currency"]}
          hide={!visible}
        />
      );
    case "discountPrice":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Giá khuyến mãi"
          field={columnName}
          type={["editable", "numericColumn", "currency"]}
          hide={!visible}
        />
      );
    case "quantity":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Số lượng"
          field={columnName}
          type={["editable", "numericColumn"]}
          hide={!visible}
        />
      );
    case "display":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Hiển thị"
          field={columnName}
          type={["checkbox"]}
          hide={!visible}
        />
      );
    case "deliver":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Giao hàng"
          field={columnName}
          type={["checkbox"]}
          hide={!visible}
        />
      );
    case "applyPromotion":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Khuyến mãi"
          field={columnName}
          type={["checkbox"]}
          hide={!visible}
        />
      );
    case "action":
      return (
        <AgGridColumn
          key={columnName}
          headerName="Tùy chọn"
          field={columnName}
          cellRenderer="actionRenderer"
          hide={!visible}
        />
      );
    default:
      throw new Error("Column not exist:" + columnName);
  }
}

type ProductTableColumnProps = {
  columnInfo: ColumnInfo;
  productCategories: ProductCategory[]
};

// NOTE: call this as a function instead of Component. Otherwise, ag-grid
// will not recognize AgGridColumn element
// e.g. call ProductTableColumn(..) instead of <ProductTableColumn ... />
export default function ProductTableColumn(props: ProductTableColumnProps) {
  const { columnInfo, productCategories } = props;

  return getColumn(columnInfo, productCategories);
}
