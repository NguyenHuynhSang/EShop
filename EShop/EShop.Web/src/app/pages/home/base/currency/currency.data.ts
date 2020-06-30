import Currency from "./currency.model";
import { randomDateBeforeNow } from "../../helpers/random";

const lastUpdate = randomDateBeforeNow(20);

// base: USD
const currencies: Currency[] = [
  {
    id: 0,
    name: "Vietnamese Dong",
    code: "VND",
    symbol: "đ",
    rate: 23211,
    lastUpdate,
  },
  {
    id: 1,
    name: "US dollar",
    code: "USD",
    symbol: "$",
    rate: 1.1213,
    lastUpdate,
  },
  {
    id: 2,
    name: "Euro",
    code: "EUR",
    symbol: "€",
    rate: 0.89,
    lastUpdate,
  },
  {
    id: 3,
    name: "Japanese yen",
    code: "JPY",
    symbol: "¥",
    rate: 107.18,
    lastUpdate,
  },
];

export default currencies;
