import Product, { ProductCategory } from './product.model';
import Currency from '../base/currency/currency.model';
import { ColDef } from 'ag-grid-community';
import { OptionType } from '../../../widgets/Select';

export type ColumnInfo = {
  field: string;
  alwaysVisible?: boolean;
} & ColDef;

type Pagination = {
  startResult: number;
  endResult: number;
  totalResults: number;
  perPage: number;
  currentPage: number;
  totalPages: number;
};

export type ProductData = Product & {
  rowIndex: number;
};

export interface ProductState {
  loading: boolean;
  rowsSelected: number;
  columnInfosGen: number;
  params: Params;
  products: ProductData[];
  productCategories: ProductCategory[];
  categories: OptionType[];
  columnInfos: ColumnInfo[];
  currency?: Currency;
  weightUnit: WeightUnit;
  currencies: Currency[];
  pagination: Pagination;
}

export enum WeightUnit {
  Kg = 'kg',
  Lb = 'lb',
}
export type Pinned = 'left' | 'right' | undefined;
export type ColumnPinPayload = {
  column: string;
  pinned: Pinned;
};

export enum SortMode {
  None = 'none',
  Ascending = 'asc',
  Descending = 'desc',
}

export type Params = {
  sort?: SortMode;
  sortBy?: string;
  currency?: number;
  page?: number;
  perPage?: number;
};

export type ColumnVisiblePayload = { column: string; visible: boolean };
