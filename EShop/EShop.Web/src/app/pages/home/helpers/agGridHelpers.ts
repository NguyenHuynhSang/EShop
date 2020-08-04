import { useCallback, useEffect, useRef } from 'react';
import {
  ColumnApi,
  GridApi,
  GridReadyEvent,
  ExportParams,
  Column,
  AgGridEvent,
} from 'ag-grid-community';
import { useDispatch } from 'react-redux';
import { useEventListener, useOnMount } from './hookHelpers';
import download from './download';
import { ExportFormat } from '../base/table.duck';
import { actions, ColumnSettings } from '../products/product.duck';

export type AgGridApi = {
  grid?: GridApi;
  column?: ColumnApi;
};
type GridReadyFunc = (event: GridReadyEvent) => void;

let api: Record<string, AgGridApi> = {};
const emptyApi = Object.freeze({ grid: undefined, column: undefined });

const columnEvents = new Set([
  // dragStopped has better performance than onColumnMoved: https://stackoverflow.com/a/57287276/9449426
  'dragStopped',
  'columnPinned',
  'columnVisible',
]);

export function useAgGrid(
  name: string,
  columnDefs: ColumnSettings[]
): GridReadyFunc {
  const dispatch = useDispatch();
  const globalCallback = useCallback(
    (type: string, e: AgGridEvent) => {
      if (columnEvents.has(type)) {
        dispatch(actions.setColumnSettings(e.columnApi.getColumnState()));
      }
    },
    [dispatch]
  );
  const onGridReady = useCallback(
    (params: GridReadyEvent) => {
      api[name] = {
        grid: params.api,
        column: params.columnApi,
      };

      params.api.addGlobalListener(globalCallback);
      params.columnApi.setColumnState(
        columnDefs.map(c => ({ ...c, colId: c.field }))
      );
    },
    [columnDefs, globalCallback, name]
  );

  useEffect(() => {
    return () => {
      api[name].grid?.removeGlobalListener(globalCallback);
      api[name] = emptyApi;
    };
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return onGridReady;
}

export function useGridApi(name: string) {
  // TODO: replace this hack with context
  return api[name] ?? emptyApi;
}

type AutoSizeFunc = (columns?: string[]) => void;

export function useAutosizeColumns(name: string): AutoSizeFunc {
  return useCallback<AutoSizeFunc>(
    columns => autoSizeColumns(api[name].column, columns),
    [name]
  );
}

export function autoSizeColumns(gridColumnApi?: ColumnApi, columns?: string[]) {
  const allColumnIds =
    columns || gridColumnApi?.getAllColumns()?.map(c => c.getId()) || [];
  gridColumnApi?.autoSizeColumns(allColumnIds, false);
  // when using custom header component, autosize does not work on the first try
  // especially when there too many columns to fit on one screen
  setTimeout(() => {
    gridColumnApi?.autoSizeColumns(allColumnIds, false);
  });
}

export const useStickyHeader = () => {
  const headerElementRef = useRef<HTMLDivElement>();
  const bodyElementRef = useRef<HTMLDivElement>();
  const stickyRef = useRef(false);
  const originalStyles = useRef({ position: '', top: '', zIndex: '' });

  useOnMount(() => {
    headerElementRef.current = document.querySelector(
      '[ref="headerRoot"]'
    ) as HTMLDivElement;
    bodyElementRef.current = document.querySelector(
      '[ref="eBodyViewport"]'
    ) as HTMLDivElement;

    const header = headerElementRef.current;

    originalStyles.current.position = header.style.position;
    originalStyles.current.top = header.style.top;
    originalStyles.current.zIndex = header.style.zIndex;
  });

  const onScroll = useCallback(() => {
    const header = headerElementRef.current;
    const body = bodyElementRef.current;
    if (!header || !body) return;

    let shouldStick = false;
    let shouldUnstick = false;

    if (!stickyRef.current) {
      shouldStick = header.getBoundingClientRect().top <= 0;
      if (shouldStick) stickyRef.current = true;
    } else {
      shouldUnstick =
        body.getBoundingClientRect().top -
          header.getBoundingClientRect().height >
        0;
      if (shouldUnstick) stickyRef.current = false;
    }

    if (shouldStick) {
      header.style.position = 'fixed';
      header.style.top = '0';
      header.style.zIndex = '2';
    }
    if (shouldUnstick) {
      const original = originalStyles.current;
      header.style.position = original.position;
      header.style.top = original.top;
      header.style.zIndex = original.zIndex;
    }
  }, []);

  useEventListener('scroll', onScroll);

  return stickyRef;
};

export const idToField = (colId: string) => colId.replace(/_[\d]+$/, '');

export const getExportParams = (api: AgGridApi): ExportParams<undefined> => {
  const selectedRows = api.grid?.getSelectedNodes().length || 0;
  const visibleCols = api.column?.getAllDisplayedColumns();

  return {
    onlySelected: selectedRows > 0,
    columnKeys: visibleCols?.filter(c => c.getColDef().field !== 'action'),
    processCellCallback: params => {
      if (!params.node) return null;

      const field = params.column.getColDef().field!;
      if (field === 'category') {
        return params.node.data[field].name;
      }

      return params.node.data[field];
    },
  };
};

export const getDataAsJson = (
  params: ExportParams<undefined>,
  api: AgGridApi
) => {
  const columns = params.columnKeys as Column[];
  const result: object[] = [];
  const columnFields: Record<string, boolean> = {};
  const selectAllRows = !params.onlySelected;
  const selectedRows: Record<string, boolean> = {};

  if (!selectAllRows) {
    api.grid?.getSelectedNodes().map(n => (selectedRows[n.id] = true));
  }

  columns.forEach(c => (columnFields[idToField(c.getId())] = true));

  api.grid?.forEachNode(rowNode => {
    if (!selectAllRows && !selectedRows[rowNode.id]) return;

    const row = {};

    Object.keys(rowNode.data)
      .filter(field => columnFields[field])
      .forEach(field => (row[field] = rowNode.data[field]));
    result.push(row);
  });

  return JSON.stringify(result, null, 4);
};

export const exportDataAsJson = (
  params: ExportParams<undefined>,
  api: AgGridApi
) => {
  download('export.json', getDataAsJson(params, api));
};

export const useExportData = (name: string, format: ExportFormat) => {
  const api = useGridApi(name);
  const params = getExportParams(api);

  switch (format) {
    case ExportFormat.Csv:
      return () => api.grid?.getDataAsCsv(params);
    case ExportFormat.Excel:
      // TODO: write my own excel implementation to reduce extra dependency
      return () => api.grid?.getDataAsExcel(params);
    case ExportFormat.Json:
      return () => getDataAsJson(params, api);
  }
};
export const useExportDownload = (name: string, format: ExportFormat) => {
  const api = useGridApi(name);
  const params = getExportParams(api);

  switch (format) {
    case ExportFormat.Csv:
      return () => api.grid?.exportDataAsCsv(params);
    case ExportFormat.Excel:
      return () => api.grid?.exportDataAsExcel(params);
    case ExportFormat.Json:
      return () => exportDataAsJson(params, api);
  }
};
