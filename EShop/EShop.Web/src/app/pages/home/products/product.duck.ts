import { persistReducer } from "redux-persist";
import storage from "redux-persist/lib/storage";
import { put, takeLatest } from "redux-saga/effects";
import ProductService from "./products.service";
import {
  ProductAction,
  ProductActionType,
  ProductState,
  GetAllRequestAction,
  ColumnInfo,
  Params,
} from "./product.duck.d";
import Product, { ProductCategory } from "./product.model";
import { AxiosResponse } from "axios";

export { ProductAction };

const initialState: ProductState = {
  loading: false,
  cachedQueries: {},
  productCategories: [],
  lastQuery: "", // TODO: workaround to prevent refetching data. First on mount, second on lastQuery changed
  columnInfos: [
    { columnName: "id", visible: true, alwaysVisible: true },
    { columnName: "name", visible: true },
    { columnName: "description", visible: false },
    { columnName: "content", visible: false },
    { columnName: "weight", visible: false },
    { columnName: "category", visible: true },
    { columnName: "numberOfVersions", visible: true },
    { columnName: "price", visible: true },
    { columnName: "originalPrice", visible: false },
    { columnName: "discountPrice", visible: false },
    { columnName: "quantity", visible: true },
    { columnName: "display", visible: true },
    { columnName: "deliver", visible: false },
    { columnName: "applyPromotion", visible: false },
    { columnName: "action", visible: true, alwaysVisible: true },
  ],
};

export const reducer = persistReducer<ProductState, ProductActionType>(
  { storage, key: "products", whitelist: ["cachedQueries", "columnInfos"] },
  (state = initialState, action) => {
    switch (action.type) {
      case ProductAction.SetColumnDisplay: {
        const { columnInfos } = action.payload;

        return {
          ...state,
          columnInfos,
        };
      }
      case ProductAction.GetAllRequest: {
        const { params } = action.payload;

        return {
          ...state,
          lastQuery: JSON.stringify(params),
        };
      }
      case ProductAction.GetAllSuccess: {
        const { params, results } = action.payload;
        const { cachedQueries } = state;

        cachedQueries[JSON.stringify(params)] = results;

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
  setColumnDisplay: (columnInfos: ColumnInfo[]): ProductActionType => ({
    type: ProductAction.SetColumnDisplay,
    payload: { columnInfos },
  }),
  getAllRequest: (params?: Params): ProductActionType => ({
    type: ProductAction.GetAllRequest,
    payload: { params },
  }),
  getAllSuccess: (results: Product[], params?: Params): ProductActionType => ({
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
    const { params } = action.payload;
    const response: AxiosResponse<Product[]> = yield ProductService.getAll(
      params
    );
    yield put(actions.getAllSuccess(response.data, params));
  });

  yield takeLatest(ProductAction.GetCategoriesRequest, function* getAllSaga() {
    // TODO: error handling
    const response: AxiosResponse<ProductCategory[]> = yield ProductService.getCategories();
    yield put(actions.getCategoriesSuccess(response.data));
  });
}
