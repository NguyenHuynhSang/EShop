import React from 'react';
import { ICellRendererParams } from 'ag-grid-community';

type HasValue<T> = {
  value: T;
};
type State<T> = HasValue<T>;
type WrappedComponentProps<T> = {
  params: ICellRendererParams;
} & HasValue<T>;
type ValueGetter<T> = (value: ICellRendererParams) => T;

// add refresh lifecycle for custom react cell renderers so they can rerender instead of
// having to unmount and mount again on every data transactions, which improve performance
// when there are complex cell renderers in many rows
export default function withRefreshLifecycle<T>(
  Component: React.ComponentType<WrappedComponentProps<T>>,
  valueGetter?: ValueGetter<T>
) {
  return class extends React.Component<ICellRendererParams, State<T>> {
    constructor(props) {
      super(props);
      this.state = {
        value: this.getValue(this.props),
      };
    }

    getValue(params: ICellRendererParams) {
      if (valueGetter) {
        return valueGetter(params);
      }
      return params.value as T;
    }

    refresh(newParams: ICellRendererParams) {
      const newValue = this.getValue(newParams);

      if (newValue !== this.state.value) {
        this.setState({ value: newValue });
      }
      return true;
    }

    render() {
      const { value } = this.state;
      return <Component value={value} params={this.props} />;
    }
  };
}
