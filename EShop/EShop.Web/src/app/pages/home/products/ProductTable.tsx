import React, { useEffect } from 'react';
import Checkbox from '@material-ui/core/Checkbox';
import Paper from '@material-ui/core/Paper';
import IconButton from '@material-ui/core/IconButton';
import DeleteIcon from '@material-ui/icons/Delete';
import EditIcon from '@material-ui/icons/Edit';
import Spinner from 'react-bootstrap/Spinner';
import { AgGridReact } from 'ag-grid-react';
import has from 'lodash/has';
import padStart from 'lodash/padStart';
import {
  ICellRendererParams,
  ColumnPinnedEvent,
  ColDef,
  DragStoppedEvent,
  ValueFormatterParams,
  SelectionChangedEvent,
} from 'ag-grid-community';
import classNames from 'classnames';
import Carousel from '../../../widgets/Carousel';
import { actions, Pinned, ProductData } from './product.duck';
import {
  useSelector,
  useDispatch,
  shallowEqual,
  RootState,
} from '../../../store/store';
import { useOnMount } from '../helpers/hookHelpers';
import { useAgGrid, useStickyHeader } from '../helpers/agGridHelpers';
import ProductTableHeader from './ProductTableHeader';
import { AgSelect } from '../../../widgets/Common';
import { makeStyles, theme } from '../../../styles';
import useColumnDefs from './useColumnDefs';
import { WeightUnit } from './product.duck';

// TODO: use intl https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Intl/NumberFormat
function formatNumber(number: number) {
  return Math.floor(number)
    .toString()
    .replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
}

const suffixCurrencyCode = {
  đ: true,
};

type ValueWithUnit = {
  prefixUnit: boolean;
  value: number;
  unit: string;
};

let SYMBOL = '';
const currencyFormatter = (params: ValueFormatterParams) => {
  const value = formatNumber(params.value);
  const unit = SYMBOL;
  return { value, unit, prefixUnit: !has(suffixCurrencyCode, unit) } as any;
};
// TODO: remove module-scope variable by passing to grid's context
let WEIGHT_UNIT = WeightUnit.Kg;
const weightFormatter = (params: ValueFormatterParams) => {
  const { value } = params;
  const unit = WEIGHT_UNIT;
  return { value, unit, prefixUnit: false } as any;
};
const idFormatter = (params: ValueFormatterParams) => {
  const { value } = params;
  return padStart(value, 4, '0');
};

function markAsDirty(params: ICellRendererParams) {
  params.colDef.cellClass = p =>
    p.rowIndex.toString() === params.node.id ? 'ag-cell-dirty' : '';
  params.api.refreshCells({
    columns: [params.column.getId()],
    rowNodes: [params.node],
    force: true, // without this line, the cell style is not refreshed at the first time
  });
}

class CheckboxRenderer extends React.Component<
  ICellRendererParams,
  { value: any }
> {
  constructor(props) {
    super(props);
    this.state = {
      value: this.props.value,
    };
  }

  refresh(newParams: ICellRendererParams) {
    if (newParams.value !== this.state.value) {
      this.setState(state => ({
        ...state,
        value: newParams.value,
      }));
    }
    return true;
  }

  render() {
    const { value } = this.state;
    return (
      <Checkbox
        // TODO: style
        // className={styles.root}
        checked={value}
        onChange={e => {
          // mark as dirty visually
          markAsDirty(this.props);
          this.props.setValue(e.target.checked);
        }}
      />
    );
  }
}

function ActionRenderer() {
  return (
    <div className='actions'>
      <IconButton>
        <EditIcon htmlColor={theme.color.blue} />
      </IconButton>
      <IconButton>
        <DeleteIcon htmlColor={theme.color.danger} />
      </IconButton>
    </div>
  );
}

// TODO: add factory for ag-grid class component
class SelectRenderer extends React.Component<
  ICellRendererParams,
  { value: any }
> {
  constructor(props) {
    super(props);
    this.state = {
      value: this.getValue(props),
    };
  }

  getOptions() {
    return this.props.context.categories;
  }

  getValue(params: ICellRendererParams) {
    const { value } = params;
    const options = this.getOptions();
    return options.find(o => o.value === parseInt(value.id, 10));
  }

  refresh(newParams: ICellRendererParams) {
    if (newParams.value.id !== this.state.value.value) {
      this.setState(state => ({
        ...state,
        value: this.getValue(newParams),
      }));
    }
    return true;
  }

  render() {
    const options = this.getOptions();
    const { value } = this.state;

    return (
      <AgSelect
        options={options}
        placeholder='Category'
        defaultValue={value}
        value={value}
        isSearchable={false}
        onChange={(e: any) => {
          // TODO: immer doesn't like products.category being mutated
          // params.setValue({ id: e.value, name: e.label });
          markAsDirty(this.props);
        }}
      />
    );
  }
}

class NumberWithUnitRenderer extends React.Component<
  ICellRendererParams,
  { value: any }
> {
  constructor(props) {
    super(props);
    this.state = {
      value: this.getValue(this.props),
    };
  }

  getValue(params: ICellRendererParams) {
    return params.valueFormatted as ValueWithUnit;
  }

  refresh(newParams: ICellRendererParams) {
    if (newParams.value !== this.state.value) {
      this.setState(state => ({
        ...state,
        value: this.getValue(newParams),
      }));
    }
    return true;
  }

  render() {
    const { value } = this.state;
    const comp = [
      value.value,
      <span key='unit' className='unit'>
        {value.unit}
      </span>,
    ];

    if (value.prefixUnit) comp.reverse();
    return <span>{comp}</span>;
  }
}

const useLoaderStyle = makeStyles({
  root: {
    display: 'flex',
    alignItems: 'center',
    padding: theme.space('md', 'lg'),
  },
});

function AgCustomLoading() {
  const styles = useLoaderStyle();
  return (
    <Paper className={styles.root}>
      <span>Please wait...</span>
      &nbsp; &nbsp;
      <Spinner animation='grow' variant='primary' size='sm' />
    </Paper>
  );
}

// TODO: add cross fade effect when changing images
class ImageRenderer extends React.Component<
  ICellRendererParams,
  { value: string[]; display: boolean; open: boolean }
> {
  constructor(props) {
    super(props);
    this.state = {
      value: this.getValue(props),
      open: false,
      display: false,
    };
  }

  getValue(params: ICellRendererParams) {
    return params.value as string[];
  }

  refresh(newParams: ICellRendererParams) {
    if (newParams.value !== this.state.value) {
      this.setState(state => ({
        ...state,
        value: this.getValue(newParams),
      }));
    }
    return true;
  }

  render() {
    const { value: images, open, display } = this.state;
    const setOpen = (open: boolean) =>
      this.setState(state => ({ ...state, open }));
    const setDisplay = (display: boolean) =>
      this.setState(state => ({ ...state, display }));
    const name = this.props.data['name'];

    if (images.length === 0) {
      return null;
    }
    return (
      <>
        <img
          style={{
            opacity: display ? '100%' : '0',
            transition: 'opacity .25s ease',
          }}
          src={images[0]}
          alt={name}
          onClick={() => setOpen(true)}
          onLoad={() => setDisplay(true)}
        />
        <Carousel
          title={name}
          images={images}
          open={open}
          onClose={() => setOpen(false)}
        />
      </>
    );
  }
}

const columnTypes: Record<string, ColDef> = {
  editable: {
    editable: true,
    onCellValueChanged: markAsDirty,
  },
  id: {
    valueFormatter: idFormatter,
    cellClass: 'ag-right-aligned-cell',
    lockPosition: true,
    checkboxSelection: true,
    headerCheckboxSelection: true,
    resizable: false,
  },
  image: {
    cellRenderer: 'ImageRenderer',
    editable: false,
  },
  currency: {
    // NOTE: type: 'numericColumn' not working here
    cellClass: 'ag-right-aligned-cell',
    cellRenderer: 'NumberWithUnitRenderer',
    valueFormatter: currencyFormatter,
  },
  weight: {
    cellClass: 'ag-right-aligned-cell',
    cellRenderer: 'NumberWithUnitRenderer',
    valueFormatter: weightFormatter,
  },
  checkbox: {
    cellRenderer: 'CheckboxRenderer',
  },
  selector: {
    // remove padding so select's width is the same as container width
    cellClass: 'p0',
    cellRenderer: 'SelectRenderer',
  },
  largeText: {
    cellEditor: 'agLargeTextCellEditor',
    maxWidth: 250,
    sortable: false,
  },
};

const frameworkComponents = {
  CheckboxRenderer,
  ActionRenderer,
  SelectRenderer,
  NumberWithUnitRenderer,
  ImageRenderer,
  agColumnHeader: ProductTableHeader,
  AgCustomLoading,
};

type ProductTableProps = {
  name: string;
  className?: string;
};
export default function ProductTable(props: ProductTableProps) {
  const { name, className, ...rest } = props;
  const products = useSelector(state => state.products.products, shallowEqual);
  const [api, onGridReady, autoSizeColumns] = useAgGrid();
  const [columnDefs] = useColumnDefs(api.column);
  const onFirstDataRendered = () => autoSizeColumns();
  const dispatch = useDispatch();
  const symbol = useSelector(state => state.products.currency?.symbol) ?? '';
  const weightUnit = useSelector(state => state.products.weightUnit);
  const loading = useSelector(state => state.products.loading);
  const context = {
    categories: useSelector(state => state.products.categories, shallowEqual),
  };

  useOnMount(() => {
    dispatch(actions.getCategoriesRequest());
    dispatch(actions.getAllRequest());
  });
  useStickyHeader();

  useEffect(() => {
    // refresh to update valueFormatter to display latest currency format
    // valueFormatter is only registered once on mount so we have to use module-scope variable
    // which is referenced by valueFormatter.
    SYMBOL = symbol;
  }, [symbol]);
  useEffect(() => {
    WEIGHT_UNIT = weightUnit;
  }, [weightUnit]);

  useEffect(() => {
    if (loading) {
      // mimic redraw behavior. https://www.ag-grid.com/javascript-grid-data-update/#setting-fresh-row-data.
      // I do a refresh instead of redraw since refreshing
      // with immutableData option is more performant because it uses transaction under the hood
      // which skips the unnecessary remount of the custom react cell renderer on every data changes
      // when implementing refresh lifecycle correctly
      // TODO: reset custom pagination
      api.grid?.deselectAll();
      // TODO: call this will make select text centered in 1 frame
      // api.grid?.collapseAll();
      api.grid?.clearFocusedCell();
      api.grid?.stopEditing(true);

      api.grid?.showLoadingOverlay();
    } else {
      api.grid?.hideOverlay();
    }
    autoSizeColumns();
  }, [api.grid, autoSizeColumns, loading]);

  const onColumnPinned = (e: ColumnPinnedEvent) => {
    if (e.columns !== null && e.pinned !== null) {
      const pinned = e.pinned as Pinned;
      for (let col of e.columns) {
        const column = col.getColDef().field!;
        dispatch(actions.setPinned({ column, pinned }));
      }
    }
  };
  // has better performance than onColumnMoved: https://stackoverflow.com/a/57287276/9449426
  const onDragStopped = (e: DragStoppedEvent) => {
    const columnOrder = e.columnApi
      ?.getColumnState()
      // remove suffix _[digit]. field: id -> colId: id_1
      .map(c => c.colId.replace(/_[\d]+$/, ''));
    if (columnOrder) dispatch(actions.setColumnOrder(columnOrder));
  };
  const onSelectionChanged = (e: SelectionChangedEvent) =>
    dispatch(actions.setRowsSelected(e.api.getSelectedNodes().length));

  return (
    <div
      key={name}
      className={classNames('ag-theme-balham table-wrapper', className)}
    >
      <AgGridReact
        // animateRows
        onDragStopped={onDragStopped}
        onColumnPinned={onColumnPinned}
        columnDefs={columnDefs}
        columnTypes={columnTypes}
        defaultColDef={{
          sortable: true,
          resizable: true,
        }}
        // https://www.ag-grid.com/javascript-grid-immutable-data/
        immutableData
        // For the Immutable Data Mode to work, you must be providing IDs for the row nodes
        getRowNodeId={(data: ProductData) => data.rowIndex.toString()}
        context={context}
        rowHeight={theme.tableRowHeight}
        rowSelection='multiple'
        suppressRowClickSelection
        rowData={products}
        headerHeight={45}
        onFirstDataRendered={onFirstDataRendered}
        onGridReady={onGridReady}
        onSelectionChanged={onSelectionChanged}
        // getRowClass={this.getRowClass}
        frameworkComponents={frameworkComponents}
        loadingOverlayComponent='AgCustomLoading'
        // column virtualization make it very laggy when scrolling horizontally with many custom cell renderer
        // due to the constant mount/unmount operations when the cell is in/out of the viewport
        suppressColumnVirtualisation
        // you can already toggle show/hide columns. dragging outside to hide
        // column just makes it more confusing
        suppressDragLeaveHidesColumns
        // By default, the grid will not stop editing the currently editing cell when the grid loses focus.
        // I revert this because it's more sensible this way.
        stopEditingWhenGridLosesFocus
        {...rest}
      />
    </div>
  );
}
