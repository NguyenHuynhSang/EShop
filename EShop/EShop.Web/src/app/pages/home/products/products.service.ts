import axios from "axios";
import Product, { ProductCategory } from "./product.model";
import { Params } from "./product.duck";
import Currency from "../base/currency/currency.model";

export const PRODUCT_GET_URL = "api/products/";
export const PRODUCT_CATEGORY_GET_URL = "api/product-categories/";
export const CURRENCY_GET_URL = "api/currencies/";

export default class ProductService {
  //   login(email, password) {
  //     return axios.post(LOGIN_URL, { email, password });
  //   }

  //   register(email, fullname, username, password) {
  //     return axios.post(REGISTER_URL, { email, fullname, username, password });
  //   }

  //   requestPassword(email) {
  //     return axios.post(REQUEST_PASSWORD_URL, { email });
  //   }

  static getAll(params?: Params) {
    return axios.get<Product[]>(PRODUCT_GET_URL, {
      params,
    });
  }

  static getCategories() {
    return axios.get<ProductCategory[]>(PRODUCT_CATEGORY_GET_URL);
  }

  static getCurrencies() {
    return axios.get<Currency[]>(CURRENCY_GET_URL);
  }
}
