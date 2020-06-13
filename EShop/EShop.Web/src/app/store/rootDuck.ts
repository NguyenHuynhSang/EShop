import { all } from "redux-saga/effects";
import { combineReducers } from "redux";

import * as auth from "./ducks/auth.duck";
import * as product from "../pages/home/products/product.duck";
import { metronic } from "../../_metronic";

export const rootReducer = combineReducers({
  auth: auth.reducer,
  i18n: metronic.i18n.reducer,
  builder: metronic.builder.reducer,
  products: product.reducer,
});

export type RootState = ReturnType<typeof rootReducer>

export function* rootSaga() {
  yield all([auth.saga(), product.saga()]);
}
