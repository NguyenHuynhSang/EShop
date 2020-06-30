import React from "react";
import { useDispatch, useSelector, shallowEqual } from "../../../store/store";
import { useEffectOnce } from "../helpers/hookHelpers";
import { actions } from "./product.duck";
import { Select, SvgIcon } from "../../../widgets/Common";
import { ReactComponent as UsaIcon } from "../../../../assets/Flags/united-states.svg";
import { ReactComponent as VietnamIcon } from "../../../../assets/Flags/vietnam.svg";
import { ReactComponent as EuIcon } from "../../../../assets/Flags/european-union.svg";
import { ReactComponent as JapanIcon } from "../../../../assets/Flags/japan.svg";
import styled from "../../../styles/styled";
import theme from "../../../styles/theme";
import Currency from "../base/currency/currency.model";

const currencyCodeToFlag = {
  USD: <UsaIcon />,
  VND: <VietnamIcon />,
  EUR: <EuIcon />,
  JPY: <JapanIcon />,
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
      <SvgIcon size={20}>{currencyCodeToFlag[currency.code]}</SvgIcon>
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
  useEffectOnce(() => {
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
