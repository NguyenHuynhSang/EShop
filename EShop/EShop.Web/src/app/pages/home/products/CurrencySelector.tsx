import React from 'react';
import { useDispatch, useSelector, shallowEqual } from '../../../store/store';
import { useOnMount } from '../helpers/hookHelpers';
import { actions } from './product.duck';
import {
  Select,
  UsaIcon,
  VietnamIcon,
  EuIcon,
  JapanIcon,
} from '../../../widgets/Common';
import { makeStyles, theme } from '../../../styles';
import Currency from '../base/currency/currency.model';

const flagIconSize = 20;
const currencyCodeToFlag = {
  USD: <UsaIcon size={flagIconSize} />,
  VND: <VietnamIcon size={flagIconSize} />,
  EUR: <EuIcon size={flagIconSize} />,
  JPY: <JapanIcon size={flagIconSize} />,
};

const useStyles = makeStyles({
  root: {
    display: 'flex',
    alignItems: 'center',
    '& > :not(:last-child)': {
      marginRight: theme.spacing.md,
    },
  },
});
const OptionContainer = ({ children }) => (
  <span className={useStyles().root}>{children}</span>
);

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
  const currency = useSelector(state => state.products.currency, shallowEqual);
  const defaultValue = currency !== undefined ? toOption(currency) : undefined;
  const currencies = useSelector(
    state => state.products.currencies,
    shallowEqual
  );

  // TODO: can be defered until the user press the dropdown
  useOnMount(() => {
    dispatch(actions.getCurrenciesRequest());
  });

  return (
    <Select
      placeholder='Currency'
      isSearchable={false}
      defaultValue={defaultValue}
      // fix pinned rows (has zIndex: 1) overlapping currency selector
      menuPortalTarget={document.body}
      onChange={(e: any) => dispatch(actions.setCurrency(e.value))}
      options={currencies.map(toOption)}
    />
  );
}
