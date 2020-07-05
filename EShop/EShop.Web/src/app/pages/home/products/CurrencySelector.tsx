import React from "react";
import { useDispatch, useSelector, shallowEqual } from "../../../store/store";
import { useOnMount } from "../helpers/hookHelpers";
import { actions } from "./product.duck";
import { Select } from "../../../widgets/Common";
import {
  UsaIcon,
  VietnamIcon,
  EuIcon,
  JapanIcon,
} from "../../../widgets/SvgIcons";
import styled, { theme } from "../../../styles/styled";
import Currency from "../base/currency/currency.model";

const flagIconSize = 20;
const currencyCodeToFlag = {
  USD: <UsaIcon size={flagIconSize} />,
  VND: <VietnamIcon size={flagIconSize} />,
  EUR: <EuIcon size={flagIconSize} />,
  JPY: <JapanIcon size={flagIconSize} />,
};

const OptionContainer = styled("span")({
  display: "flex",
  alignItems: "center",
  "& > :not(:last-child)": {
    marginRight: theme.spacing.md,
  },
});

const toOption = (currency: Currency): any => ({
  label: (
    <OptionContainer>
      {currencyCodeToFlag[currency.code]}
      <span>{currency.code}</span>
    </OptionContainer>
  ),
  value: currency.id,
});

export default function CurrencySelector() {
  const dispatch = useDispatch();
  const currency = useSelector(
    (state) => state.products.currency,
    shallowEqual
  );
  const defaultValue = currency !== undefined ? toOption(currency) : undefined;
  const currencies = useSelector(
    (state) => state.products.currencies,
    shallowEqual
  );

  // TODO: can be defered until the user press the dropdown
  useOnMount(() => {
    dispatch(actions.getCurrenciesRequest());
  });

  return (
    <Select
      name="colors"
      width="150px"
      placeholder="Currency"
      defaultValue={defaultValue}
      onChange={(e: any) => dispatch(actions.setCurrency(e.value))}
      options={currencies.map(toOption)}
      className="basic-multi-select"
      classNamePrefix="select"
    />
  );
}
