import React from 'react';
import { ColDef } from 'ag-grid-community';
import { useSelector, shallowEqual } from '../../../store/store';
import { ColumnSettings } from './product.duck';

export const colDefs: Record<string, ColDef> = {
  id: {
    headerName: 'ID',
    type: ['id'],
  },
  name: {
    headerName: 'Tên',
    type: ['editable'],
  },
  image: {
    headerName: 'Hình',
    type: ['image'],
  },
  description: {
    headerName: 'Mô tả',
    type: ['editable', 'largeText'],
  },
  content: {
    headerName: 'Nội dung',
    type: ['editable', 'largeText'],
  },
  weight: {
    headerName: 'Khối lượng',
    type: ['editable', 'weight'],
  },
  category: {
    headerName: 'Loại',
    type: ['selector'],
  },
  numberOfVersions: {
    headerName: 'Số phiên bản',
    type: ['editable', 'numericColumn'],
  },
  price: {
    headerName: 'Giá',
    type: ['editable', 'currency'],
  },
  originalPrice: {
    headerName: 'Giá gốc',
    type: ['editable', 'currency'],
  },
  discountPrice: {
    headerName: 'Giá khuyến mãi',
    type: ['editable', 'currency'],
  },
  quantity: {
    headerName: 'Số lượng',
    type: ['editable', 'numericColumn'],
  },
  display: {
    headerName: 'Hiển thị',
    type: ['checkbox'],
  },
  deliver: {
    headerName: 'Giao hàng',
    type: ['checkbox'],
  },
  applyPromotion: {
    headerName: 'Khuyến mãi',
    type: ['checkbox'],
  },
  action: {
    headerName: 'Tùy chọn',
    cellRenderer: 'ActionRenderer',
    sortable: false,
  },
};

export default function useColumnDefs(name: string) {
  // TODO: use name params as key in redux store
  const columnSettings = useSelector(
    state => state.products.columnSettings,
    shallowEqual
  );
  const colDefsRef = React.useRef<ColumnSettings[]>([]);

  React.useEffect(() => {
    colDefsRef.current = columnSettings.map(c => ({
      ...colDefs[c.field],
      ...c,
      colId: c.field,
    }));
  }, [columnSettings]);

  return colDefsRef.current;
}
