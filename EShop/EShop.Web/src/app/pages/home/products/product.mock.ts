import MockAdapter from "axios-mock-adapter";
import { PRODUCT_GET_URL, PRODUCT_CATEGORY_GET_URL } from "./products.service";
import productData, { productCategories } from "./product.data";

export default function mockProduct(mock: MockAdapter) {
  mock.onGet(PRODUCT_GET_URL).reply(response => {
      console.log('GET', PRODUCT_GET_URL, response);
      return [200, productData];
  });

  mock.onGet(PRODUCT_CATEGORY_GET_URL).reply(response => {
      console.log('GET', PRODUCT_CATEGORY_GET_URL, response);
      return [200, productCategories];
  });
}
