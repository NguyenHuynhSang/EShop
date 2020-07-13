import React from "react";
import { makeStyles } from "@material-ui/core";
import classNames from "classnames";
import {
  ArrowLeftIcon,
  ArrowRightIcon,
  ArrowToLeftIcon,
  ArrowToRightIcon,
  Button,
  Select,
  toSimpleOption,
} from "../../../widgets/Common";
import { actions } from "./product.duck";
import { useSelector, useDispatch, shallowEqual } from "../../../store/store";
import { Pagination } from "./product.duck";
import theme from "../../../styles/theme";

const useStyles = makeStyles({
  pagination: {
    display: "flex",
    alignItems: "center",
    "& > :not(:last-child)": {
      marginRight: theme.spacing.sm,
    },
  },
});

type PageFunc = (page: number) => void;

const usePagination = (): [Pagination, PageFunc, PageFunc, PageFunc] => {
  const pagination = useSelector(
    (state) => state.products.pagination,
    shallowEqual
  );
  const dispatch = useDispatch();
  const changeToPage = (page: number) => () =>
    dispatch(actions.getAllRequest({ page }));
  const changePage = (page: number) =>
    changeToPage(pagination.currentPage + page);
  const setPerPage = (perPage: number) =>
    dispatch(actions.getAllRequest({ perPage }));

  return [pagination, changeToPage, changePage, setPerPage];
};

export default function ProductTablePagination() {
  const [pagination, changeToPage, changePage, setPerPage] = usePagination();
  const {
    startResult,
    endResult,
    totalResults,
    perPage,
    currentPage,
    totalPages,
  } = pagination;
  const isFirstPage = currentPage === 1;
  const isLastPage = currentPage === totalPages;
  const styles = useStyles();

  return (
    <div className={classNames(styles.pagination, "ag-pagination")}>
      <span>
        <strong>{startResult}</strong> to <strong>{endResult}</strong> of{" "}
        <strong>{totalResults}</strong>
      </span>
      <Select
        isSearchable={false}
        defaultValue={toSimpleOption(perPage)}
        onChange={(e: any) => setPerPage(e.value)}
        options={[5, 10, 20, 50, 100].map(toSimpleOption)}
      />
      <Button
        variant="outline-secondary"
        disabled={isFirstPage}
        onClick={changeToPage(1)}
      >
        <ArrowToLeftIcon />
      </Button>
      <Button
        variant="outline-secondary"
        disabled={isFirstPage}
        onClick={changePage(-1)}
      >
        <ArrowLeftIcon />
      </Button>
      <span>
        Page <strong>{currentPage}</strong> of <strong>{totalPages}</strong>
      </span>
      <Button
        variant="outline-secondary"
        disabled={isLastPage}
        onClick={changePage(1)}
      >
        <ArrowRightIcon />
      </Button>
      <Button
        variant="outline-secondary"
        disabled={isLastPage}
        onClick={changeToPage(totalPages)}
      >
        <ArrowToRightIcon />
      </Button>
    </div>
  );
}
