import Product, { ProductCategory } from "./product.model";

export type ColumnInfo = {
  columnName: string;
  visible: boolean;
};

export interface ProductState {
  loading: boolean;
  cachedQueries: {
    [query: string]: Product[];
  };
  productCategories: ProductCategory[];
  lastQuery: string;
  columnInfos: ColumnInfo[];
}

export enum ProductAction {
  SetColumnDisplay = "[Set Column Display] Action",
  GetAllRequest = "[Get All Request] Action",
  GetAllSuccess = "[Get All Success] Product API",
  GetAllFailure = "[Get All Failure] Product API",
  GetRequest = "[Get Request] Action",
  GetCategoriesRequest = "[Get Categories Request] Action",
  GetCategoriesSuccess = "[Get Categories Success] Product API",
  GetCategoriesFailure = "[Get Categories Failure] Product API",
}

export type Params = { [key: string]: string | undefined };

export interface GetAllRequestAction {
  type: typeof ProductAction.GetAllRequest;
  payload: {
    params?: Params;
  };
}

interface GetAllSuccessAction {
  type: typeof ProductAction.GetAllSuccess;
  payload: {
    params?: Params;
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

interface SetColumnDisplayAction {
  type: typeof ProductAction.SetColumnDisplay;
  payload: {
    columnInfos: ColumnInfo[];
  };
}

export type ProductActionType =
  | SetColumnDisplayAction
  | GetAllRequestAction
  | GetAllSuccessAction
  | GetCategoriesRequestAction
  | GetCategoriesSuccessAction;
