import MockAdapter from 'axios-mock-adapter';
import {
  PRODUCT_GET_URL,
  PRODUCT_CATEGORY_GET_URL,
  CURRENCY_GET_URL,
} from './products.service';
import productData, { productCategories } from './product.data';
import clamp from 'lodash/clamp';
import currencies from '../base/currency/currency.data';
import sortBy from 'lodash/sortBy';
import Product, { ProductResult } from './product.model';
import { Params } from './product.duck';
import Currency from '../base/currency/currency.model';

const vndCurrency = currencies.find(c => c.code === 'VND')!;

function convertCurrency(price: number, currency: Currency) {
  // use VND currency as base
  return Math.floor((price / vndCurrency.rate) * currency.rate);
}

function getSortComparator(sortField?: string) {
  if (!sortField) return 'name';
  if (sortField === 'category') {
    return (product: Product) => product.category.name;
  }

  return sortField;
}

export default function mockProduct(mock: MockAdapter) {
  mock.onGet(PRODUCT_GET_URL).reply((config): [number, ProductResult] => {
    const params: Params = config.params;
    const {
      currency: currencyId,
      sortBy: sortField,
      sort = 'none',
      page = 1,
      perPage = 10,
    } = params;
    const sortComparator = getSortComparator(sortField);
    let products = productData;

    if (sort !== 'none') {
      if (sort === 'asc') {
        products = sortBy<Product>(productData, sortComparator);
      } else if (sort === 'desc') {
        products = sortBy<Product>(productData, sortComparator).reverse();
      }
    }

    const currency = currencies.find(c => c.id === currencyId);
    if (currency !== undefined) {
      products = products.map(p => ({
        ...p,
        price: convertCurrency(p.price, currency),
        originalPrice: convertCurrency(p.originalPrice, currency),
        discountPrice: convertCurrency(p.discountPrice, currency),
      }));
    }

    console.log('GET', PRODUCT_GET_URL, params);

    const totalResults = products.length;
    const lastPage = Math.ceil(totalResults / perPage);
    const pageIndex = clamp(page, 1, lastPage) - 1;
    const startResult = pageIndex * perPage;
    const endResult = startResult + perPage;

    // mock failed request: uncomment this line and select US currency
    // if (currencyId === 1) return [503, { results: [], totalResults: 0 }];

    return [
      200,
      {
        results: products.slice(startResult, endResult),
        totalResults: products.length,
      },
    ];
  });

  mock.onGet(PRODUCT_CATEGORY_GET_URL).reply(response => {
    console.log('GET', PRODUCT_CATEGORY_GET_URL);
    return [200, productCategories];
  });

  // TODO: move to currency.mock
  mock.onGet(CURRENCY_GET_URL).reply(response => {
    console.log('GET', CURRENCY_GET_URL);
    return [200, currencies];
  });
}
