import { ColumnApi, GridApi, GridReadyEvent } from "ag-grid-community";
import { useRef, useCallback, MutableRefObject } from "react";

type GridReadyCb = (gridApi: GridApi, columnApi: ColumnApi) => void;
type GridReadyFunction = (event: GridReadyEvent) => void;

export function useGridApi(
  onGridReadyCb: GridReadyCb
): [MutableRefObject<GridApi | undefined>, MutableRefObject<ColumnApi | undefined>, GridReadyFunction?] {
  const gridApiRef = useRef<GridApi>();
  const gridColumnApiRef = useRef<ColumnApi>();
  const onGridReady = useCallback(
    (params: GridReadyEvent) => {
      gridApiRef.current = params.api;
      gridColumnApiRef.current = params.columnApi;

      onGridReadyCb(gridApiRef.current, gridColumnApiRef.current);
    },
    [onGridReadyCb]
  );

  return [gridApiRef, gridColumnApiRef, onGridReady];
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
