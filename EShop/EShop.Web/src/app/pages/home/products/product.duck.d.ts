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
  params?: Params;
  products: Product[];
  productCategories: ProductCategory[];
  columnInfos: ColumnInfo[];
  currency?: Currency;
  currencies: Currency[];
}

export enum ProductAction {
  SetColumnDisplay = "[Set Column Display] Action",
  SetCurrentCurrency = "[Set Current Currency] Action",
  GetAllRequest = "[Get All Request] Action",
  GetAllSuccess = "[Get All Success] Product API",
  GetAllFailure = "[Get All Failure] Product API",
  GetRequest = "[Get Request] Action",
  GetCategoriesRequest = "[Get Categories Request] Action",
  GetCategoriesSuccess = "[Get Categories Success] Product API",
  GetCategoriesFailure = "[Get Categories Failure] Product API",
  GetCurrenciesRequest = "[Get Currencies Request] Action",
  GetCurrenciesSuccess = "[Get Currencies Success] Product API",
  GetCurrenciesFailure = "[Get Currencies Failure] Product API",
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

export interface GetAllRequestAction {
  type: typeof ProductAction.GetAllRequest;
  payload: {
    params?: Params;
  };
}

interface GetAllSuccessAction {
  type: typeof ProductAction.GetAllSuccess;
  payload: {
    results: Product[];
  };
}

interface GetCategoriesRequestAction {
  type: typeof ProductAction.GetCategoriesRequest;
}

interface GetCategoriesSuccessAction {
  type: typeof ProductAction.GetCategoriesSuccess;
  payload: {
    results: ProductCategory[];
  };
}

interface GetCurrenciesRequestAction {
  type: typeof ProductAction.GetCurrenciesRequest;
}

interface GetCurrenciesSuccessAction {
  type: typeof ProductAction.GetCurrenciesSuccess;
  payload: {
    results: Currency[];
  };
}

interface SetColumnDisplayAction {
  type: typeof ProductAction.SetColumnDisplay;
  payload: {
    columnInfos: ColumnInfo[];
  };
}

export interface SetCurrentCurrencyAction {
  type: typeof ProductAction.SetCurrentCurrency;
  payload: {
    currencyId: number;
  };
}

export type ProductActionType =
  | SetColumnDisplayAction
  | SetCurrentCurrencyAction
  | GetAllRequestAction
  | GetAllSuccessAction
  | GetCategoriesRequestAction
  | GetCategoriesSuccessAction
  | GetCurrenciesRequestAction
  | GetCurrenciesSuccessAction;
