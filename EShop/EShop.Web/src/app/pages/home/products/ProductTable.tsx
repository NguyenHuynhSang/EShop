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
import round from 'lodash/round';
import {
  ICellRendererParams,
  ColumnPinnedEvent,
  ColDef,
  DragStoppedEvent,
  ValueFormatterParams,
  SelectionChangedEvent,
  ValueGetterParams,
  GridApi,
} from 'ag-grid-community';
import Carousel from '../../../widgets/Carousel';
import { actions, Pinned, ProductData, WeightUnit } from './product.duck';
import { useSelector, useDispatch, shallowEqual } from '../../../store/store';
import { useOnMount } from '../helpers/hookHelpers';
import {
  useAgGrid,
  useAutosizeColumns,
  useStickyHeader,
} from '../helpers/agGridHelpers';
import ProductTableHeader from './ProductTableHeader';
import { AgSelect, OptionType } from '../../../widgets/Common';
import { makeStyles, theme } from '../../../styles';
import useColumnDefs from './useColumnDefs';
import withRefreshLifecycle from '../helpers/withRefreshLifecycle';

type GridContext = {
  categories: OptionType[];
  currency: string;
  weight: WeightUnit;
};

// TODO: use intl https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Intl/NumberFormat
function formatNumber(number: number) {
  return Math.floor(number)
    .toString()
    .replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
}

// noinspection NonAsciiCharacters
const suffixCurrencyCode = {
  đ: true,
};

type ValueWithUnit = {
  prefixUnit: boolean;
  value: number | string;
  unit: string;
};

const getContext = (params: { context: any }) => params.context as GridContext;

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

const CheckboxRenderer = withRefreshLifecycle<boolean>(({ value, params }) => (
  <Checkbox
    checked={value}
    onChange={e => {
      // mark as dirty visually
      markAsDirty(params);
      params.setValue(e.target.checked);
    }}
  />
));

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

const SelectRenderer = withRefreshLifecycle<OptionType>(
  ({ value, params }) => {
    const options = params.context.categories;

    return (
      <AgSelect
        options={options}
        placeholder='Category'
        defaultValue={value}
        value={value}
        isSearchable={false}
        onChange={() => {
          // TODO: immer doesn't like products.category being mutated
          // params.setValue({ id: e.value, name: e.label });
          markAsDirty(params);
        }}
      />
    );
  },
  params => {
    const { value } = params;
    const options = params.context.categories;
    return options.find(o => o.value === parseInt(value.id, 10));
  }
);

const NumberWithUnitComponent = (props: { value: ValueWithUnit }) => {
  const { value } = props;
  const comp = [
    value.value,
    <span key='unit' className='unit'>
      {value.unit}
    </span>,
  ];

  if (value.prefixUnit) comp.reverse();
  return <span>{comp}</span>;
};
const WeightRenderer = withRefreshLifecycle<ValueWithUnit>(
  NumberWithUnitComponent,
  params => {
    const { value } = params;
    const { weight } = getContext(params);
    return { value, unit: weight, prefixUnit: false };
  }
);
const CurrencyRenderer = withRefreshLifecycle<ValueWithUnit>(
  NumberWithUnitComponent,
  params => {
    const value = formatNumber(params.value);
    const { currency } = getContext(params);
    return {
      value,
      unit: currency,
      prefixUnit: !has(suffixCurrencyCode, currency),
    };
  }
);

// TODO: write weightParser when implementing form
function weightGetter(params: ValueGetterParams) {
  const context = getContext(params);
  const { weight } = context;
  const value = params.data[params.colDef.field!];

  if (weight === WeightUnit.Lb) return round(value * 2.20462, 2);
  if (weight === WeightUnit.Kg) return value;
  throw Error('what dis? ' + weight);
}

const useLoadingOverlayStyles = makeStyles({
  root: {
    position: 'relative',
  },
  overlay: {
    position: 'absolute',
    width: '100%',
    height: '100%',
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    zIndex: p => (p.display ? 1 : -1),
  },
  table: {
    filter: p => (p.blur ? 'blur(1.5px)' : 'none'),
    transition: 'filter .25s',
  },
});
type LoadingOverlayProps = {
  loading: boolean;
  api?: GridApi;
  children: React.ReactNode;
};

function LoadingOverlay({ loading, api, children }: LoadingOverlayProps) {
  const [display, setDisplay] = React.useState(false);
  const [blur, setBlur] = React.useState(false);
  const styles = useLoadingOverlayStyles({ blur, display });
  const onTransitionEnd = () => {
    if (!loading) {
      setDisplay(false);
    }
  };

  useEffect(() => {
    if (loading) {
      // mimic redraw behavior. https://www.ag-grid.com/javascript-grid-data-update/#setting-fresh-row-data.
      // I do a refresh instead of redraw since refreshing
      // with immutableData option is more performant because it uses transaction under the hood
      // which skips the unnecessary remount of the custom react cell renderer on every data changes
      // when implementing refresh lifecycle correctly
      // TODO: reset custom pagination
      // TODO: reset dirty cells
      api?.deselectAll();
      // TODO: call this will make select text centered in 1 frame
      // api?.collapseAll();
      api?.clearFocusedCell();
      api?.stopEditing(true);
      setDisplay(true);
      setBlur(true);
    } else {
      setBlur(false);
    }
  }, [api, loading]);

  return (
    <div className={styles.root}>
      <div className={styles.overlay}>
        <LoadingContent display={blur} />
      </div>
      <div className={styles.table} onTransitionEnd={onTransitionEnd}>
        {children}
      </div>
    </div>
  );
}

const useLoaderStyle = makeStyles<number>({
  root: {
    display: 'flex',
    alignItems: 'center',
    padding: theme.space('md', 'lg'),
    transition: 'opacity .25s',
    opacity: opacity => opacity,
  },
});

type LoadingContentProps = {
  display: boolean;
};

function LoadingContent(params: LoadingContentProps) {
  const { display } = params;
  const styles = useLoaderStyle(display ? 1 : 0);

  return (
    <Paper className={styles.root}>
      <span>Please wait...</span>
      &nbsp; &nbsp;
      <Spinner animation='grow' variant='primary' size='sm' />
    </Paper>
  );
}

// TODO: add cross fade effect when changing images
const ImageRenderer = withRefreshLifecycle<string[]>(({ value, params }) => {
  const images = value;
  const [open, setOpen] = React.useState(false);
  const [display, setDisplay] = React.useState(false);
  const name = params.data['name'];

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
});

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
    cellRenderer: 'CurrencyRenderer',
  },
  weight: {
    cellClass: 'ag-right-aligned-cell',
    cellRenderer: 'WeightRenderer',
    valueGetter: weightGetter,
  },
  checkbox: {
    cellRenderer: 'CheckboxRenderer',
  },
  selector: {
    // remove padding so select width is the same as container width
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
  WeightRenderer,
  CurrencyRenderer,
  ImageRenderer,
  agColumnHeader: ProductTableHeader,
};

type ProductTableProps = {
  name: string;
};
export default function ProductTable(props: ProductTableProps) {
  const { name, ...rest } = props;
  const products = useSelector(state => state.products.products, shallowEqual);
  const columnDefs = useColumnDefs(name);
  const [api, onGridReady] = useAgGrid(name, columnDefs);
  const autoSizeColumns = useAutosizeColumns(name);
  const onFirstDataRendered = () => autoSizeColumns();
  const dispatch = useDispatch();
  const loading = useSelector(state => state.products.loading);
  const context: GridContext = {
    categories: useSelector(state => state.products.categories, shallowEqual),
    currency: useSelector(state => state.products.currency?.symbol) ?? '',
    weight: useSelector(state => state.products.weightUnit),
  };

  useOnMount(() => {
    dispatch(actions.getCategoriesRequest());
    dispatch(actions.getAllRequest());
  });
  useStickyHeader();

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
    <LoadingOverlay loading={loading} api={api.grid}>
      <div key={name} className='ag-theme-balham table-wrapper'>
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
          // the reason I have to write my own LoadingOverlay component is because there is no way to
          // manually hide the overlay as AgGrid manages the overlay automatically. Thus fade-out
          // animation is unachievable.
          suppressLoadingOverlay
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
    </LoadingOverlay>
  );
}
