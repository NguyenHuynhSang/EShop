import { persistReducer } from "redux-persist";
import storage from "redux-persist/lib/storage";
import { put, takeLatest, select } from "redux-saga/effects";
import ProductService from "./products.service";
import {
  ProductAction,
  ProductActionType,
  ProductState,
  ColumnInfo,
  Params,
  SetCurrentCurrencyAction,
} from "./product.duck.d";
import Product, { ProductCategory } from "./product.model";
import { AxiosResponse } from "axios";
import Currency from "../base/currency/currency.model";

export { ProductAction };

const initialState: ProductState = {
  loading: false,
  productCategories: [],
  // TODO: workaround to prevent refetching data. First on mount, second on lastQuery changed
  params: {},
  products: [],
  currencies: [],
  currency: undefined,
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
  {
    storage,
    key: "products",
    whitelist: ["params", "products", "columnInfos", "currency"],
  },
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
          params: {
            ...state.params,
            ...params,
          },
        };
      }
      case ProductAction.GetAllSuccess: {
        const { results: products } = action.payload;

        return {
          ...state,
          products,
        };
      }
      case ProductAction.GetCategoriesSuccess: {
        const { results } = action.payload;

        return {
          ...state,
          productCategories: results,
        };
      }
      // TODO: store currency in a seperate shared store
      case ProductAction.GetCurrenciesSuccess: {
        const { results } = action.payload;

        if (state.currency === undefined) {
          // set default currency
          return {
            ...state,
            currency: results[0],
            currencies: results,
          };
        }

        return {
          ...state,
          currencies: results,
        };
      }
      // TODO: store currency in a seperate shared store
      case ProductAction.SetCurrentCurrency: {
        const { currencyId } = action.payload;
        const currency = state.currencies.find((c) => c.id === currencyId);

        return {
          ...state,
          currency,
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
  setCurrency: (currencyId: number): ProductActionType => ({
    type: ProductAction.SetCurrentCurrency,
    payload: { currencyId },
  }),
  getAllRequest: (params?: Params): ProductActionType => ({
    type: ProductAction.GetAllRequest,
    payload: { params },
  }),
  getAllSuccess: (results: Product[]): ProductActionType => ({
    type: ProductAction.GetAllSuccess,
    payload: { results },
  }),
  getCategoriesRequest: (): ProductActionType => ({
    type: ProductAction.GetCategoriesRequest,
  }),
  getCategoriesSuccess: (results: ProductCategory[]): ProductActionType => ({
    type: ProductAction.GetCategoriesSuccess,
    payload: { results },
  }),
  getCurrenciesRequest: (): ProductActionType => ({
    type: ProductAction.GetCurrenciesRequest,
  }),
  getCurrenciesSuccess: (results: Currency[]): ProductActionType => ({
    type: ProductAction.GetCurrenciesSuccess,
    payload: { results },
  }),
};

export function* saga() {
  yield takeLatest(ProductAction.GetAllRequest, function* func() {
    // TODO: error handling

    // NOTE: dont get params from action.payload.params, get params from state as it merges all previous applied params
    const params =
      (yield select((state: any) => state.products?.params) as Params) ?? {};

    if (params.currency === undefined) {
      // TODO: add type for select
      const currency: Currency = yield select(
        (state: any) => state.products.currency
      );
      params.currency = currency?.id;
    }
    // TODO: if have tests, replace yield Api.fetch('/products') with yield call(Api.fetch, '/products')
    const response: AxiosResponse<Product[]> = yield ProductService.getAll(
      params
    );
    yield put(actions.getAllSuccess(response.data));
  });

  yield takeLatest(ProductAction.GetCategoriesRequest, function* func() {
    // TODO: error handling
    const response: AxiosResponse<ProductCategory[]> = yield ProductService.getCategories();
    yield put(actions.getCategoriesSuccess(response.data));
  });

  yield takeLatest(ProductAction.GetCurrenciesRequest, function* func() {
    // TODO: error handling
    const response: AxiosResponse<Currency[]> = yield ProductService.getCurrencies();
    yield put(actions.getCurrenciesSuccess(response.data));
  });

  yield takeLatest(ProductAction.SetCurrentCurrency, function* func(
    action: SetCurrentCurrencyAction
  ) {
    // TODO: error handling
    const { currencyId: currency } = action.payload;
    yield put(actions.getAllRequest({ currency }));
  });
}
