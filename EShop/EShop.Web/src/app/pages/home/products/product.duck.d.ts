import Product, { ProductCategory } from "./product.model";
import Currency from "../base/currency/currency.model";

export type ColumnInfo = {
  columnName: string;
  visible: boolean;
  alwaysVisible?: boolean;
  pinned?: boolean;
};

export interface ProductState {
  loading: boolean;
  params: Params;
  products: Product[];
  productCategories: ProductCategory[];
  columnInfos: ColumnInfo[];
  currency?: Currency;
  currencies: Currency[];
}

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
