import MockAdapter from "axios-mock-adapter";
import {
  PRODUCT_GET_URL,
  PRODUCT_CATEGORY_GET_URL,
  CURRENCY_GET_URL,
} from "./products.service";
import productData, { productCategories } from "./product.data";
import currencies from "../base/currency/currency.data";
import sortBy from "lodash/sortBy";
import round from 'lodash/round'
import Product from "./product.model";
import { Params, WeightUnit } from "./product.duck";
import Currency from "../base/currency/currency.model";

const vndCurrency = currencies.find((c) => c.code === "VND")!;

function convertCurrency(price: number, currency: Currency) {
  // use VND currency as base
  return Math.floor((price / vndCurrency.rate) * currency.rate);
}
function convertWeight(weight: number, weightUnit: WeightUnit) {
  if (weightUnit === WeightUnit.Lb) return round(weight * 2.20462, 2);
  if (weightUnit === WeightUnit.Kg) return weight;
  throw Error("what dis? " + weightUnit);
}

function getSortComparator(sortField?: string) {
  if (!sortField) return "name";
  if (sortField === "category") {
    return (product: Product) => product.category.name;
  }

  return sortField;
}

export default function mockProduct(mock: MockAdapter) {
  mock.onGet(PRODUCT_GET_URL).reply((config): [number, Product[]] => {
    const params: Params = config.params;
    const {
      currency: currencyId,
      sortBy: sortField,
      sort = "none",
      weight,
    } = params;
    const sortComparator = getSortComparator(sortField);
    let products = productData;

    if (sort !== "none") {
      if (sort === "asc") {
        products = sortBy<Product>(productData, sortComparator);
      } else if (sort === "desc") {
        products = sortBy<Product>(productData, sortComparator).reverse();
      }
    }

    const currency = currencies.find((c) => c.id === currencyId);
    if (currency !== undefined) {
      products = products.map((p) => {
        return {
          ...p,
          price: convertCurrency(p.price, currency),
          originalPrice: convertCurrency(p.originalPrice, currency),
          discountPrice: convertCurrency(p.discountPrice, currency),
        };
      });
    }

    if (weight !== undefined) {
      products = products.map((p) => {
        return {
          ...p,
          weight: convertWeight(p.weight, weight),
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
