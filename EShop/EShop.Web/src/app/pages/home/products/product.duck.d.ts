import Product, { ProductCategory } from "./product.model";

export interface ProductState {
  loading: boolean;
  cachedQueries: {
    [query: string]: Product[];
  };
  productCategories: ProductCategory[];
  lastQuery: string;
}

export enum ProductAction {
  GetAllRequest = "[Get All Request] Action",
  GetAllSuccess = "[Get All Success] Product API",
  GetAllFailure = "[Get All Failure] Product API",
  GetRequest = "[Get Request] Action",
  GetCategoriesRequest = "[Get Categories Request] Action",
  GetCategoriesSuccess = "[Get Categories Success] Product API",
  GetCategoriesFailure = "[Get Categories Failure] Product API",
}

export interface GetAllRequestAction {
  type: typeof ProductAction.GetAllRequest;
  payload: {
    params: string;
  };
}

interface GetAllSuccessAction {
  type: typeof ProductAction.GetAllSuccess;
  payload: {
    params: string;
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

export type ProductActionType =
  | GetAllRequestAction
  | GetAllSuccessAction
  | GetCategoriesRequestAction
  | GetCategoriesSuccessAction;
