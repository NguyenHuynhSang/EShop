import { persistReducer } from "redux-persist";
import storage from "redux-persist/lib/storage";
import { put, takeLatest } from "redux-saga/effects";
import ProductService from "./products.service";
import {
  ProductAction,
  ProductActionType,
  ProductState,
  GetAllRequestAction,
} from "./product.duck.d";
import Product, { ProductCategory } from "./product.model";
import { AxiosResponse } from "axios";

export { ProductAction };

const initialState: ProductState = {
  loading: false,
  cachedQueries: {},
  productCategories: [],
  lastQuery: "", // TODO: workaround to prevent refetching data. First on mount, second on lastQuery changed
};

export const reducer = persistReducer<ProductState, ProductActionType>(
  { storage, key: "products", whitelist: ["cachedQueries"] },
  (state = initialState, action) => {
    switch (action.type) {
      case ProductAction.GetAllRequest: {
        const { params } = action.payload;

        return {
          ...state,
          lastQuery: params,
        };
      }
      case ProductAction.GetAllSuccess: {
        const { params, results } = action.payload;
        const { cachedQueries } = state;

        cachedQueries[params] = results;

        return {
          ...state,
          cachedQueries,
        };
      }
      case ProductAction.GetCategoriesSuccess: {
        const { results } = action.payload;

        return {
          ...state,
          productCategories: results,
        };
      }
      default:
        return state;
    }
  }
);

export const actions = {
  getAllRequest: (params: string): ProductActionType => ({
    type: ProductAction.GetAllRequest,
    payload: { params },
  }),
  getAllSuccess: (params: string, results: Product[]): ProductActionType => ({
    type: ProductAction.GetAllSuccess,
    payload: { params, results },
  }),
  getCategoriesRequest: (): ProductActionType => ({
    type: ProductAction.GetCategoriesRequest,
  }),
  getCategoriesSuccess: (results: ProductCategory[]): ProductActionType => ({
    type: ProductAction.GetCategoriesSuccess,
    payload: { results },
  }),
};

export function* saga() {
  yield takeLatest(ProductAction.GetAllRequest, function* getAllSaga(
    action: GetAllRequestAction
  ) {
    // TODO: error handling
    const response: AxiosResponse<Product[]> = yield ProductService.getAll();
    yield put(actions.getAllSuccess(action.payload.params, response.data));
  });

  yield takeLatest(ProductAction.GetCategoriesRequest, function* getAllSaga() {
    // TODO: error handling
    const response: AxiosResponse<ProductCategory[]> = yield ProductService.getCategories();
    yield put(actions.getCategoriesSuccess(response.data));
  });
}
