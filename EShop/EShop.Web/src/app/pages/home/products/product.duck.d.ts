import Product, { ProductCategory } from "./product.model";
import Currency from "../base/currency/currency.model";
import { ColDef } from "ag-grid-community";

export type ColumnInfo = {
  field: string;
  alwaysVisible?: boolean;
} & ColDef;

export interface ProductState {
  loading: boolean;
  columnInfosGen: number;
  params: Params;
  products: Product[];
  productCategories: ProductCategory[];
  categories: OptionTypeBase[];
  columnInfos: ColumnInfo[];
  currency?: Currency;
  weightUnit: WeightUnit;
  currencies: Currency[];
}

export type WeightUnit = "kg" | "lb";
export type Pinned = "left" | "right" | undefined;
export type ColumnPinPayload = {
  column: string;
  pinned: Pinned;
};

export enum SortMode {
  None = "none",
  Ascending = "asc",
  Descending = "desc",
}

export type Params = {
  sort?: SortMode;
  sortBy?: string;
  currency?: number;
};

export type ColumnVisiblePayload = { column: string; visible: boolean };
