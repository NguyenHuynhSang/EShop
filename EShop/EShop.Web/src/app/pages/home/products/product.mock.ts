import MockAdapter from "axios-mock-adapter";
import { PRODUCT_GET_URL, PRODUCT_CATEGORY_GET_URL } from "./products.service";
import productData, { productCategories } from "./product.data";
import sortBy from "lodash/sortBy";
import Product from "./product.model";

export default function mockProduct(mock: MockAdapter) {
  mock.onGet(PRODUCT_GET_URL).reply((config) => {
    const { params } = config;
    const sortField = params?.sortBy || "name";
    const sort = params?.sort || "none";
    let result = productData;

    if (sort !== "none") {
      if (sort === "asc") {
        result = sortBy<Product>(productData, [sortField]);
      }
      else if (sort === "desc") {
        result = sortBy<Product>(productData, [sortField]).reverse();
      }
    }
    return [200, result];
  });

  mock.onGet(PRODUCT_CATEGORY_GET_URL).reply((response) => {
    console.log("GET", PRODUCT_CATEGORY_GET_URL);
    return [200, productCategories];
  });
}
