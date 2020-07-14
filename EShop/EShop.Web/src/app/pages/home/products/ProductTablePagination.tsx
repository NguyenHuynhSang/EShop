import React from "react";
import { makeStyles, Button } from "@material-ui/core";
import FirstPageIcon from "@material-ui/icons/FirstPage";
import LastPageIcon from "@material-ui/icons/LastPage";
import PrevPageIcon from "@material-ui/icons/ChevronLeft";
import NextPageIcon from "@material-ui/icons/ChevronRight";
import classNames from "classnames";
import {
  // Button,
  Select,
  SelectComps,
  toSimpleOption,
} from "../../../widgets/Common";
import { actions } from "./product.duck";
import { useSelector, useDispatch, shallowEqual } from "../../../store/store";
import { Pagination } from "./product.duck";
import theme from "../../../styles/theme";
import { important } from "../../../styles/styled";

const useStyles = makeStyles({
  pagination: {
    display: "grid",
    alignItems: "center",
    gridTemplateColumns: "repeat(7, max-content)",
    columnGap: theme.spacing.sm,
  },
  rowSummary: {
    fontSize: "14px",
  },
  button: {
    minWidth: 0,
    padding: 0,
    width: "40px",
    height: "40px",
  },
  select: {
    marginRight: theme.spacing.sm,
  },
  selectControl: {
    borderColor: important("transparent"),
    transition: important("background-color .25s"),
    "&:hover": {
      backgroundColor: important(theme.color.primaryLight),
      "& > *": {
        color: important(theme.color.primary),
      },
    },
  },
  selectSingleValue: {
    color: important("inherit"),
    transition: important("color .25s"),
  },
  pageDescription: {
    fontSize: "14px",
    margin: theme.space(0, "md"),
  },
});

type PageFunc = (page: number) => () => void;
type PerPageFunc = (page: number) => void;

const usePagination = (): [Pagination, PageFunc, PageFunc, PerPageFunc] => {
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
      <Select
        className={styles.select}
        isSearchable={false}
        defaultValue={toSimpleOption(perPage)}
        onChange={(e: any) => setPerPage(e.value)}
        components={{
          Control: ({ children, className, ...rest }) => (
            <SelectComps.Control
              {...rest}
              className={classNames(className, styles.selectControl)}
            >
              {children}
            </SelectComps.Control>
          ),
          SingleValue: ({ children, className, ...rest }) => (
            <SelectComps.SingleValue
              {...rest}
              className={classNames(className, styles.selectSingleValue)}
            >
              <span className={styles.rowSummary}>
                <strong>{startResult}</strong> to <strong>{endResult}</strong>{" "}
                of <strong>{totalResults}</strong>
              </span>
            </SelectComps.SingleValue>
          ),
          IndicatorsContainer: () => null,
        }}
        options={[
          {
            label: "Rows",
            options: [5, 10, 20, 50, 100].map(toSimpleOption),
          },
        ]}
      />
      <Button
        className={styles.button}
        variant="outlined"
        disabled={isFirstPage}
        onClick={changeToPage(1)}
      >
        <FirstPageIcon />
      </Button>
      <Button
        className={styles.button}
        variant="outlined"
        disabled={isFirstPage}
        onClick={changePage(-1)}
      >
        <PrevPageIcon />
      </Button>
      <span className={styles.pageDescription}>
        Page <strong>{currentPage}</strong> of <strong>{totalPages}</strong>
      </span>
      <Button
        className={styles.button}
        variant="outlined"
        disabled={isLastPage}
        onClick={changePage(1)}
      >
        <NextPageIcon />
      </Button>
      <Button
        className={styles.button}
        variant="outlined"
        disabled={isLastPage}
        onClick={changeToPage(totalPages)}
      >
        <LastPageIcon />
      </Button>
    </div>
  );
}
