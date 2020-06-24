import React from "react";
import styled from "styled-components";
import { IHeaderParams } from "ag-grid-community";
import { useSelector, useDispatch, shallowEqual } from "../../../store/store";
import { actions } from "./product.duck";

type HeaderWrapperProps = {
  isNumericColumn: boolean;
};

const HeaderWrapper = styled.div<HeaderWrapperProps>`
  width: 100%;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: ${(props) => (props.isNumericColumn ? "end" : "start")};
`;

enum SortMode {
  None = "none",
  Ascending = "asc",
  Descending = "desc",
}
type SortFunction = () => void;

const sortModes = [SortMode.None, SortMode.Ascending, SortMode.Descending];

const useSort = (
  enableSorting: boolean,
  sortBy?: string
): [SortMode, SortFunction] => {
  const dispatch = useDispatch();
  const sortIndexRef = React.useRef<number>(0);
  const [sortMode, setSortMode] = React.useState(
    sortModes[sortIndexRef.current]
  );

  const cycleSort = () => {
    if (!enableSorting) return;

    sortIndexRef.current =
      sortIndexRef.current >= sortModes.length - 1
        ? 0
        : sortIndexRef.current + 1;

    const nextSortMode = sortModes[sortIndexRef.current];
    setSortMode(nextSortMode);
    dispatch(actions.getAllRequest({ sort: nextSortMode, sortBy }));
  };

  return [sortMode, cycleSort];
};

const getSortIndicator = (sortMode: SortMode) => {
  if (sortMode === SortMode.None) return null;

  const arrowClassModifier = sortMode === SortMode.Ascending ? "up" : "down";
  return (
    <span>
      &nbsp;
      <i className={"fa fa-long-arrow-alt-" + arrowClassModifier} />
    </span>
  );
};

export default function ProductTableHeader(props: IHeaderParams) {
  const { displayName, column, enableSorting } = props;
  const colDef = column.getColDef();
  const isNumericColumn =
    colDef.type !== undefined && colDef.type?.indexOf("numericColumn") !== -1;
  const [sortMode, cycleSort] = useSort(enableSorting, colDef.field);

  return (
    <HeaderWrapper
      isNumericColumn={isNumericColumn}
      role="button"
      onClick={() => cycleSort()}
    >
      {displayName}
      {enableSorting && getSortIndicator(sortMode)}
    </HeaderWrapper>
  );
}
