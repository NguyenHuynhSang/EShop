import { persistReducer, PersistConfig } from "redux-persist";
import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import storage from "redux-persist/lib/storage";
import { put, takeLatest, select } from "redux-saga/effects";
import { AxiosResponse } from "axios";
import ProductService from "./products.service";
import {
  ProductState,
  Params,
  ColumnPinPayload,
  ColumnInfo,
  ColumnVisiblePayload,
  WeightUnit,
} from "./product.duck.d";
import Product, { ProductCategory } from "./product.model";
import Currency from "../base/currency/currency.model";

export * from "./product.duck.d";

const columnInfos: ColumnInfo[] = [
  { field: "id", alwaysVisible: true, pinned: "left" },
  { field: "name", pinned: "left" },
  { field: "description", hide: true },
  { field: "content", hide: true },
  { field: "weight", hide: true },
  { field: "category" },
  { field: "numberOfVersions" },
  { field: "price" },
  { field: "originalPrice", hide: true },
  { field: "discountPrice", hide: true },
  { field: "quantity" },
  { field: "display" },
  { field: "deliver", hide: true },
  { field: "applyPromotion", hide: true },
  { field: "action", alwaysVisible: true },
];

const initialState: ProductState = {
  loading: false,
  productCategories: [],
  categories: [],
  params: {},
  products: [],
  currencies: [],
  currency: undefined,
  weightUnit: "kg",
  columnInfos,
  columnInfosGen: 0,
};

const slice = createSlice({
  initialState,
  name: "product",
  reducers: {
    setColumnDisplay(state, action: PayloadAction<ColumnInfo[]>) {
      state.columnInfos = action.payload;
      state.columnInfosGen++;
    },
    setColumnVisible(state, action: PayloadAction<ColumnVisiblePayload>) {
      const { column, visible } = action.payload;
      const col = state.columnInfos.find((c) => c.field === column);
      if (col) col.hide = !visible;
    },
    setColumnOrder(state, action: PayloadAction<string[]>) {
      const columns = action.payload;
      const order: Record<string, number> = {};
      columns.forEach((c, i) => (order[c] = i));
      state.columnInfos.sort((a, b) => order[a.field] - order[b.field]);
    },
    setPinned(state, action: PayloadAction<ColumnPinPayload>) {
      const { column, pinned } = action.payload;
      const col = state.columnInfos.find((c) => c.field === column);

      if (col) col.pinned = pinned;
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
      state.categories = action.payload.map((c) => ({
        label: c.name,
        value: c.id,
      }));
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
    setWeightUnit(state, action: PayloadAction<WeightUnit>) {
      state.weightUnit = action.payload;
    },
  },
});

const persistConfig: PersistConfig<ProductState> = {
  storage,
  key: "products",
  blacklist: ["loading", "columnInfosGen"],
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
