import React from "react";
import { ColDef } from "ag-grid-community";
import { useSelector, shallowEqual } from "../../../store/store";
import { ColumnInfo } from "./product.duck.d";
import { ProductCategory } from "./product.model";
import { useForceUpdate } from "../helpers/hookHelpers";
import toMap from "../helpers/toMap";

const getColDefs = (categories: ProductCategory[]): Record<string, ColDef> => {
  const categoryData = toMap(categories, "id", "name");

  return {
    id: {
      headerName: "ID",
      lockPosition: true,
      type: "numericColumn",
      resizable: false,
    },
    name: {
      headerName: "Tên",
      type: ["editable"],
    },
    description: {
      headerName: "Mô tả",
      type: ["editable", "largeText"],
    },
    content: {
      headerName: "Nội dung",
      type: ["editable", "largeText"],
    },
    weight: {
      headerName: "Khối lượng",
      type: ["editable", "numericColumn"],
    },
    category: {
      headerName: "Loại",
      type: ["editable"],
      cellEditor: "agSelectCellEditor",
      cellEditorParams: {
        values: Object.keys(categoryData),
      },
      refData: categoryData,
    },
    numberOfVersions: {
      headerName: "Số phiên bản",
      type: ["editable", "numericColumn"],
    },
    price: {
      headerName: "Giá",
      type: ["editable", "numericColumn", "currency"],
    },
    originalPrice: {
      headerName: "Giá gốc",
      type: ["editable", "numericColumn", "currency"],
    },
    discountPrice: {
      headerName: "Giá khuyến mãi",
      type: ["editable", "numericColumn", "currency"],
    },
    quantity: {
      headerName: "Số lượng",
      type: ["editable", "numericColumn"],
    },
    display: {
      headerName: "Hiển thị",
      type: ["checkbox"],
    },
    deliver: {
      headerName: "Giao hàng",
      type: ["checkbox"],
    },
    applyPromotion: {
      headerName: "Khuyến mãi",
      type: ["checkbox"],
    },
    action: {
      headerName: "Tùy chọn",
      cellRenderer: "actionRenderer",
      sortable: false,
    },
  };
};

let COLUMN_DEFS: ColDef[] = [];

// return [columnDefs, columnInfos]
// columnDefs: is used to initialize column definitions on mount
// columnInfos: current column states saved in the store
export default function useColumnDefs(
  onForceUpdateColDefs: Function
): [ColDef[], ColumnInfo[]] {
  const productCategories = useSelector(
    (state) => state.products.productCategories,
    shallowEqual
  );
  const columnInfos = useSelector(
    (state) => state.products.columnInfos,
    shallowEqual
  );
  const columnDisplayGen = useSelector(
    (state) => state.products.columnDisplayGen
  );
  const forceUpdate = useForceUpdate();

  React.useEffect(() => {
    // columnDefs should not be changed once assigned for the first time or weird
    // behaviors start to happen when interacting with columns. For example when
    // moving or pining
    if (COLUMN_DEFS.length === 0 && productCategories.length > 0) {
      const colDefs = getColDefs(productCategories);
      COLUMN_DEFS = columnInfos.map((c) => ({ ...colDefs[c.field], ...c }));
    }
  }, [columnInfos, productCategories]);

  React.useEffect(() => {
    const colDefs = getColDefs(productCategories);
    COLUMN_DEFS = columnInfos.map((c) => ({ ...colDefs[c.field], ...c }));
    forceUpdate();
    setTimeout(() => {
      onForceUpdateColDefs();
    });
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [columnDisplayGen]);

  return [COLUMN_DEFS, columnInfos];
}
