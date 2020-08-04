import { ColDef } from 'ag-grid-community';
import Product, { ProductCategory } from './product.model';
import Currency from '../base/currency/currency.model';
import { OptionType } from '../../../widgets/Select';

export type ColumnSettings = {
  field: string; // field is required here instead of optional
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
  params: Params;
  products: ProductData[];
  productCategories: ProductCategory[];
  categories: OptionType[];
  columnSettings: ColumnSettings[];
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
