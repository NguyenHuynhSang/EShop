import { persistReducer, PersistConfig } from 'redux-persist';
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import storage from 'redux-persist/lib/storage';
import { ColumnState } from 'ag-grid-community/dist/lib/columnController/columnController';
import clamp from 'lodash/clamp';
import ProductService from './products.service';
import {
  ProductState,
  Params,
  ColumnSettings,
  WeightUnit,
} from './product.duck';
import { ProductCategory, ProductResult } from './product.model';
import Currency from '../base/currency/currency.model';
import { actions as errorActions } from '../base/errors/error.duck';
import { put, takeLatest, select, call } from '../../../store/saga';
import { idToField } from '../helpers/agGridHelpers';

export * from './product.duck.d';

const columnSettings: ColumnSettings[] = [
  { colId: 'id', alwaysVisible: true, pinned: 'left' },
  { colId: 'name', pinned: 'left' },
  { colId: 'image', hide: true },
  { colId: 'description', hide: true },
  { colId: 'content', hide: true },
  { colId: 'weight', hide: true },
  { colId: 'category' },
  { colId: 'numberOfVersions' },
  { colId: 'price' },
  { colId: 'originalPrice', hide: true },
  { colId: 'discountPrice', hide: true },
  { colId: 'quantity' },
  { colId: 'display' },
  { colId: 'deliver', hide: true },
  { colId: 'applyPromotion', hide: true },
  { colId: 'action', alwaysVisible: true },
];

const initialState: ProductState = {
  loading: false,
  rowsSelected: 0,
  productCategories: [],
  categories: [],
  params: {},
  products: [],
  currencies: [],
  currency: undefined,
  weightUnit: WeightUnit.Kg,
  columnSettings,
  pagination: {
    startResult: 0,
    endResult: 0,
    totalResults: 0,
    perPage: 10,
    currentPage: 1,
    totalPages: 0,
  },
};

const slice = createSlice({
  initialState,
  name: 'product',
  reducers: {
    setColumnSettings(state, action: PayloadAction<ColumnState[]>) {
      // undefined | null !== undefined. No thanks.
      state.columnSettings = action.payload.map(c => ({
        ...c,
        colId: idToField(c.colId),
      })) as any;
    },
    setRowsSelected(state, action: PayloadAction<number>) {
      state.rowsSelected = action.payload;
    },
    getAllRequest(state, action: PayloadAction<Params | undefined>) {
      state.loading = true;
      state.params = {
        currency: state.currency?.id,
        ...state.params,
        ...action?.payload,
      };
    },
    getAllSuccess(state, action: PayloadAction<ProductResult>) {
      const { results, totalResults } = action.payload;
      const { pagination } = state;
      const currentPage = state.params.page ?? pagination.currentPage;
      const perPage = state.params.perPage ?? pagination.perPage;

      state.loading = false;
      state.products = results.map((p, i) => ({ rowIndex: i, ...p }));

      pagination.totalPages = Math.ceil(totalResults / perPage);
      pagination.currentPage = clamp(currentPage, 1, pagination.totalPages);
      pagination.startResult = (pagination.currentPage - 1) * perPage + 1;
      pagination.endResult = Math.min(
        pagination.startResult + perPage - 1,
        totalResults
      );
      pagination.totalResults = totalResults;
      pagination.perPage = perPage;
    },
    getAllFailure(state) {
      state.loading = false;
    },
    getCategoriesRequest() {},
    getCategoriesSuccess(state, action: PayloadAction<ProductCategory[]>) {
      state.productCategories = action.payload;
      state.categories = action.payload.map(c => ({
        label: c.name,
        value: c.id as any,
      }));
    },
    getCategoriesFailure() {},
    // TODO: store currency in a separate shared store
    getCurrenciesRequest() {},
    getCurrenciesSuccess(state, action: PayloadAction<Currency[]>) {
      state.currencies = action.payload;

      if (state.currency === undefined) {
        state.currency = state.currencies[0];
      }
    },
    getCurrenciesFailure() {},
    // TODO: store currency in a separate shared store
    setCurrency(state, action: PayloadAction<number>) {
      const currencyId = action.payload;
      state.currency = state.currencies.find(c => c.id === currencyId);
    },
    setWeightUnit(state, action: PayloadAction<WeightUnit>) {
      state.weightUnit = action.payload;
    },
  },
});

const persistConfig: PersistConfig<ProductState> = {
  storage,
  key: 'products',
  blacklist: ['loading', 'rowsSelected'],
};

export const { actions } = slice;
export const reducer = persistReducer(persistConfig, slice.reducer);

function* fetchAll() {
  try {
    // NOTE: dont get params from action.payload.params, get params from state as it merges all previous applied params
    const params = yield* select(state => state.products.params);
    const response = yield* call(ProductService.getAll, params);
    yield* put(actions.getAllSuccess(response.data));
  } catch (err) {
    yield* put(errorActions.setError(err));
    yield* put(actions.getAllFailure());
  }
}

function* fetchCategories() {
  try {
    const response = yield* call(ProductService.getCategories);
    yield* put(actions.getCategoriesSuccess(response.data));
  } catch (err) {
    yield* put(errorActions.setError(err));
    yield* put(actions.getCategoriesFailure());
  }
}

function* fetchCurrencies() {
  try {
    const response = yield* call(ProductService.getCurrencies);
    yield* put(actions.getCurrenciesSuccess(response.data));
  } catch (err) {
    yield* put(errorActions.setError(err));
    yield* put(actions.getCurrenciesFailure());
  }
}

function* fetchCurrency(action: ReturnType<typeof actions.setCurrency>) {
  const currency = action.payload;
  yield* put(actions.getAllRequest({ currency }));
}

export function* saga() {
  yield* takeLatest(actions.getAllRequest.type, fetchAll);
  yield* takeLatest(actions.getCategoriesRequest.type, fetchCategories);
  yield* takeLatest(actions.getCurrenciesRequest.type, fetchCurrencies);
  yield* takeLatest(actions.setCurrency.type, fetchCurrency);
}
