import { all } from "redux-saga/effects";

import * as auth from "./ducks/auth.duck";
import * as table from "../pages/home/base/table.duck";
import * as product from "../pages/home/products/product.duck";
import { metronic } from "../../_metronic";

export const reducer = {
  auth: auth.reducer,
  table: table.reducer,
  products: product.reducer,
  i18n: metronic.i18n.reducer,
  builder: metronic.builder.reducer,
}

export function* rootSaga() {
  yield all([auth.saga(), product.saga()]);
}
