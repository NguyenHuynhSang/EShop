import React, { useCallback } from "react";
import { ColDef, ColumnApi } from "ag-grid-community";
import { useSelector, shallowEqual } from "../../../store/store";
import { ColumnInfo } from "./product.duck";
import { useForceUpdate } from "../helpers/hookHelpers";
import { autoSizeColumns } from "../helpers/agGridHelpers";

export const colDefs: Record<string, ColDef> = {
  id: {
    headerName: "ID",
    type: ["id"],
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
    type: ["editable", "weight"],
  },
  category: {
    headerName: "Loại",
    type: ["selector"],
  },
  numberOfVersions: {
    headerName: "Số phiên bản",
    type: ["editable", "numericColumn"],
  },
  price: {
    headerName: "Giá",
    type: ["editable", "currency"],
  },
  originalPrice: {
    headerName: "Giá gốc",
    type: ["editable", "currency"],
  },
  discountPrice: {
    headerName: "Giá khuyến mãi",
    type: ["editable", "currency"],
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
    cellRenderer: "ActionRenderer",
    sortable: false,
  },
};

let COLUMN_DEFS: ColDef[] = [];

// return [columnDefs, columnInfos]
// columnDefs: is used to initialize column definitions on mount
// columnInfos: current column states saved in the store
export default function useColumnDefs(
  columnApi?: ColumnApi
): [ColDef[], ColumnInfo[]] {
  const columnInfos = useSelector(
    (state) => state.products.columnInfos,
    shallowEqual
  );
  const columnInfosGen = useSelector((state) => state.products.columnInfosGen);
  const forceUpdate = useForceUpdate();
  const getColumnDefs = useCallback(
    () => columnInfos.map((c) => ({ ...colDefs[c.field], ...c })),
    [columnInfos]
  );

  React.useEffect(() => {
    // columnDefs should not be changed once assigned for the first time or weird
    // behaviors start to happen when interacting with columns. For example when
    // moving or pinning column
    if (COLUMN_DEFS.length === 0) {
      COLUMN_DEFS = getColumnDefs();
    }
  }, [getColumnDefs]);

  React.useEffect(() => {
    COLUMN_DEFS = getColumnDefs();
    forceUpdate();
    setTimeout(() => {
      autoSizeColumns(columnApi);
    });
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [columnInfosGen]);

  return [COLUMN_DEFS, columnInfos];
}
