import MockAdapter from "axios-mock-adapter";
import {
  PRODUCT_GET_URL,
  PRODUCT_CATEGORY_GET_URL,
  CURRENCY_GET_URL,
} from "./products.service";
import productData, { productCategories } from "./product.data";
import currencies from "../base/currency/currency.data";
import sortBy from "lodash/sortBy";
import Product from "./product.model";
import { Params } from "./product.duck.d";
import Currency from "../base/currency/currency.model";

const vndCurrency = currencies.find((c) => c.code === "VND")!;

function convert(price: number, currency: Currency) {
  // use VND currency as base
  return Math.floor((price / vndCurrency.rate) * currency.rate);
}

export default function mockProduct(mock: MockAdapter) {
  mock.onGet(PRODUCT_GET_URL).reply((config): [number, Product[]] => {
    const params: Params = config.params;
    const sortField = params?.sortBy || "name";
    const sort = params?.sort || "none";
    let products = productData;

    if (sort !== "none") {
      if (sort === "asc") {
        products = sortBy<Product>(productData, [sortField]);
      } else if (sort === "desc") {
        products = sortBy<Product>(productData, [sortField]).reverse();
      }
    }

    const currency = currencies.find((c) => c.id === params?.currency);

    if (currency !== undefined) {
      products = products.map((p) => {
        return {
          ...p,
          price: convert(p.price, currency),
          originalPrice: convert(p.originalPrice, currency),
          discountPrice: convert(p.discountPrice, currency),
        };
      });
    }

    console.log("GET", PRODUCT_GET_URL, params);
    return [200, products];
  });

  mock.onGet(PRODUCT_CATEGORY_GET_URL).reply((response) => {
    console.log("GET", PRODUCT_CATEGORY_GET_URL);
    return [200, productCategories];
  });

  // TODO: move to currency.mock
  mock.onGet(CURRENCY_GET_URL).reply((response) => {
    console.log("GET", CURRENCY_GET_URL);
    return [200, currencies];
  });
}
