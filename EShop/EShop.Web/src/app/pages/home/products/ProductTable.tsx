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
import { actions, Pinned } from './product.duck';
import { useSelector, useDispatch, shallowEqual } from '../../../store/store';
import { useOnMount } from '../helpers/hookHelpers';
import { useAgGrid, useStickyHeader } from '../helpers/agGridHelpers';
import ProductTableHeader from './ProductTableHeader';
import { AgSelect } from '../../../widgets/Common';
import { makeStyles, important, theme } from '../../../styles';
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

const useCheckboxStyle = makeStyles({
  root: {
    padding: important(0),
  },
});

function CheckboxRenderer(params: ICellRendererParams) {
  const styles = useCheckboxStyle();
  return (
    <Checkbox
      className={styles.root}
      checked={params.value}
      onChange={e => {
        // mark as dirty visually
        markAsDirty(params);
        params.setValue(e.target.checked);
      }}
    />
  );
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

function SelectRenderer(params: ICellRendererParams) {
  const options = useSelector(state => state.products.categories, shallowEqual);
  const value = options.find(o => o.value === parseInt(params.value.id, 10));
  return (
    <AgSelect
      options={options}
      placeholder='Category'
      defaultValue={value}
      isSearchable={false}
      onChange={(e: any) => {
        // TODO: immer doesn't like products.category being mutated
        // params.setValue({ id: e.value, name: e.label });
        markAsDirty(params);
      }}
    />
  );
}

function NumberWithUnitRenderer(params: ICellRendererParams) {
  const val = params.valueFormatted as ValueWithUnit;
  const comp = [
    val.value,
    <span key='unit' className='unit'>
      {val.unit}
    </span>,
  ];

  if (val.prefixUnit) comp.reverse();
  return <span>{comp}</span>;
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

function ImageRenderer(params: ICellRendererParams) {
  const images = params.value as string[];
  const name = params.data['name'];
  const [open, setOpen] = React.useState(false);
  const [display, setDisplay] = React.useState(false);

  if (images.length > 0) {
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
  return null;
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
  const products = useSelector<any[]>(
    state => state.products.products,
    shallowEqual
  );
  const [api, onGridReady, autoSizeColumns] = useAgGrid();
  const [columnDefs] = useColumnDefs(api.column);
  const onFirstDataRendered = () => autoSizeColumns();
  const dispatch = useDispatch();
  const symbol = useSelector(state => state.products.currency?.symbol) ?? '';
  const weightUnit = useSelector(state => state.products.weightUnit);
  const loading = useSelector(state => state.products.loading);

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
    if (loading) api.grid?.showLoadingOverlay();
    else api.grid?.hideOverlay();
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
        // column virtualization make it very laggy when scrolling horizontally
        suppressColumnVirtualisation
        // you can already toggle show/hide columns. dragging outside to hide
        // column just makes it more confusing
        suppressDragLeaveHidesColumns
        {...rest}
      />
    </div>
  );
}
