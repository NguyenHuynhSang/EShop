import MockAdapter from "axios-mock-adapter";
import mockAuth from "./mockAuth";
import { AxiosStatic } from "axios";
import mockProduct from "../../app/pages/home/products/product.mock";

export default function mockAxios(axios: AxiosStatic) {
  const mock = new MockAdapter(axios);

  mockAuth(mock);
  mockProduct(mock);

  return mock;
}