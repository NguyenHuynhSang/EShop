import { persistReducer, PersistConfig } from "redux-persist";
import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import storage from "redux-persist/lib/storage";
import { put, takeLatest, select } from "redux-saga/effects";
import ProductService from "./products.service";
import { ProductState, ColumnInfo, Params } from "./product.duck.d";
import Product, { ProductCategory } from "./product.model";
import { AxiosResponse } from "axios";
import Currency from "../base/currency/currency.model";

const initialState: ProductState = {
  loading: false,
  productCategories: [],
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

const slice = createSlice({
  initialState,
  name: "product",
  reducers: {
    setColumnDisplay(state, action: PayloadAction<ColumnInfo[]>) {
      state.columnInfos = action.payload;
    },
    getAllRequest(state, action: PayloadAction<Params | undefined>) {
      state.params = {
        currency: state.currency?.id,
        ...state.params,
        ...action?.payload,
      };
    },
    getAllSuccess(state, action: PayloadAction<Product[]>) {
      state.products = action.payload;
    },
    getAllFailure(state, action: PayloadAction<string>) {
      // TODO: implement
      const error = action.payload;
    },
    getCategoriesRequest() {},
    getCategoriesSuccess(state, action: PayloadAction<ProductCategory[]>) {
      state.productCategories = action.payload;
    },
    getCategoriesFailure(state, action: PayloadAction<string>) {
      // TODO: implement
      const error = action.payload;
    },
    // TODO: store currency in a seperate shared store
    getCurrenciesRequest() {},
    getCurrenciesSuccess(state, action: PayloadAction<Currency[]>) {
      state.currencies = action.payload;

      if (state.currency === undefined) {
        state.currency = state.currencies[0];
      }
    },
    getCurrenciesFailure(state, action: PayloadAction<string>) {
      // TODO: implement
      const error = action.payload;
    },
    // TODO: store currency in a seperate shared store
    setCurrency(state, action: PayloadAction<number>) {
      const currencyId = action.payload;
      state.currency = state.currencies.find((c) => c.id === currencyId);
    },
  },
});

const persistConfig: PersistConfig<ProductState> = {
  storage,
  key: "products",
  whitelist: ["params", "products", "columnInfos", "currency"],
};

export const { actions } = slice;
export const reducer = persistReducer(persistConfig, slice.reducer);

export function* saga() {
  yield takeLatest(actions.getAllRequest.type, function* func() {
    // TODO: error handling

    // NOTE: dont get params from action.payload.params, get params from state as it merges all previous applied params
    const params = yield select((state: any) => state.products?.params);

    // TODO: if have tests, replace yield Api.fetch('/products') with yield call(Api.fetch, '/products')
    const response: AxiosResponse<Product[]> = yield ProductService.getAll(
      params
    );
    yield put(actions.getAllSuccess(response.data));
  });

  yield takeLatest(actions.getCategoriesRequest.type, function* func() {
    // TODO: error handling
    const response: AxiosResponse<ProductCategory[]> = yield ProductService.getCategories();
    yield put(actions.getCategoriesSuccess(response.data));
  });

  yield takeLatest(actions.getCurrenciesRequest.type, function* func() {
    // TODO: error handling
    const response: AxiosResponse<Currency[]> = yield ProductService.getCurrencies();
    yield put(actions.getCurrenciesSuccess(response.data));
  });

  yield takeLatest(actions.setCurrency.type, function* func(
    action: ReturnType<typeof actions.setCurrency>
  ) {
    // TODO: error handling
    const currency = action.payload;
    yield put(actions.getAllRequest({ currency }));
  });
}
