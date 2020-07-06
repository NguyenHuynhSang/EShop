import { ColumnApi, GridApi, GridReadyEvent } from "ag-grid-community";
import { useRef, useCallback } from "react";

type GridReadyCb = (api: AgGridApi) => void;
export type AgGridApi = {
  grid?: GridApi;
  column?: ColumnApi;
};
type GridReadyFunction = (event: GridReadyEvent) => void;
type AutoSizeFunction = (columns?: string[]) => void;

export function useGridApi(
  onGridReadyCb: GridReadyCb
): [AgGridApi, GridReadyFunction, AutoSizeFunction] {
  const apiRef = useRef<AgGridApi>({});
  const onGridReady = useCallback(
    (params: GridReadyEvent) => {
      apiRef.current.grid = params.api;
      apiRef.current.column = params.columnApi;

      onGridReadyCb(apiRef.current);
    },
    [onGridReadyCb]
  );
  const autoSizeColumnsCb = useCallback<AutoSizeFunction>(
    (columns) => autoSizeColumns(apiRef.current.column, columns),
    []
  );

  return [apiRef.current, onGridReady, autoSizeColumnsCb];
}

export function autoSizeColumns(gridColumnApi?: ColumnApi, columns?: string[]) {
  const allColumnIds =
    columns || gridColumnApi?.getAllColumns().map((c) => c.getId()) || [];
  gridColumnApi?.autoSizeColumns(allColumnIds, false);
  // when using custom header component, autosize does not work on the first try
  // especially when there too many columns to fit on one screen
  setTimeout(() => {
    gridColumnApi?.autoSizeColumns(allColumnIds, false);
  });
}
