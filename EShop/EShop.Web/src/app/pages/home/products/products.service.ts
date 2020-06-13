import axios from "axios";
import Product, { ProductCategory } from "./product.model";

export const PRODUCT_GET_URL = 'api/products';
export const PRODUCT_CATEGORY_GET_URL = 'api/product-categories';

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

  static getAll() {
    return axios.get<Product[]>(PRODUCT_GET_URL);
  }


  static getCategories() {
    return axios.get<ProductCategory[]>(PRODUCT_CATEGORY_GET_URL);
  }
}
