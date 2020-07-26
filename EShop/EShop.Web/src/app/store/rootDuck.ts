import { all } from "redux-saga/effects";

import * as auth from "./ducks/auth.duck";
import * as errors from "../pages/home/base/errors/error.duck";
import * as table from "../pages/home/base/table.duck";
import * as products from "../pages/home/products/product.duck";
import { metronic } from "../../_metronic";

export const reducer = {
  auth: auth.reducer,
  errors: errors.reducer,
  table: table.reducer,
  products: products.reducer,
  i18n: metronic.i18n.reducer,
  builder: metronic.builder.reducer,
};

export function* rootSaga() {
  yield all([auth.saga(), products.saga()]);
}
