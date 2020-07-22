import React from "react";
import Snackbar from "@material-ui/core/Snackbar";
import IconButton from "@material-ui/core/IconButton";
import CloseIcon from "@material-ui/icons/Close";
import ErrorIcon from "@material-ui/icons/HighlightOff";
import WarningIcon from "@material-ui/icons/Warning";
import InfoIcon from "@material-ui/icons/Info";
import SuccessIcon from "@material-ui/icons/CheckCircle";
import immer from "@reduxjs/toolkit/node_modules/immer";
import { makeStyles } from "../styles";

type ProviderContext = [
  (message: string, option?: SnackbarOption) => void,
  (key?: string) => void
];

const SnackbarContext = React.createContext<ProviderContext>([
  () => {},
  () => {},
]);
export const useSnackbar = () => React.useContext(SnackbarContext);

type Variant = "success" | "info" | "warning" | "error";
type SnackParams = {
  message: React.ReactNode;
  open: boolean;
  key: string;
  action?: React.ReactNode;
  variant?: Variant;
  autoHideDuration?: number;
};
type SnackbarOption = Partial<Omit<SnackParams, "open" | "message">>;

const useStyles = makeStyles<SnackbarContainerProps>((theme) => ({
  root: {
    "& .MuiSnackbarContent-root": {
      backgroundColor: (props) =>
        props.variant
          ? theme.palette[props.variant].main
          : theme.palette.background,
    },
    "& button.MuiButton-text": {
      backgroundColor: (props) =>
        props.variant ? theme.palette[props.variant].light : "transparent",
    },
    "& button": {
      color: (props) =>
        props.variant ? theme.palette[props.variant].contrastText : "inherit",
    },
  },
  content: {
    display: "flex",
    alignItems: "center",
    "& .MuiSvgIcon-root": {
      marginRight: theme.spacing(1),
    },
  },
}));

function getIcon(variant?: Variant) {
  switch (variant) {
    case "error":
      return <ErrorIcon />;
    case "warning":
      return <WarningIcon />;
    case "info":
      return <InfoIcon />;
    case "success":
      return <SuccessIcon />;
    default:
      return null;
  }
}

type SnackbarContainerProps = SnackParams & {
  onClose: () => void;
  onKill: () => void;
};
function SnackbarContainer(props: SnackbarContainerProps) {
  const styles = useStyles(props);
  const {
    message,
    open,
    variant,
    autoHideDuration = 3000,
    action,
    onClose,
    onKill,
  } = props;

  return (
    <Snackbar
      className={styles.root}
      anchorOrigin={{
        vertical: "bottom",
        horizontal: "right",
      }}
      autoHideDuration={autoHideDuration}
      onClose={(_, reason) => reason !== "clickaway" && onClose()}
      onExited={onKill}
      message={
        <div className={styles.content}>
          {getIcon(variant)}
          {message}
        </div>
      }
      open={open}
      action={
        action ?? (
          <IconButton color="inherit" size="small" onClick={onClose}>
            <CloseIcon />
          </IconButton>
        )
      }
    />
  );
}

export default function SnackbarProvider({ children }) {
  const [snacks, setSnacks] = React.useState<SnackParams[]>([]);
  const createSnackbar = (message: string, option?: SnackbarOption) => {
    const key = option?.key ?? message + "_" + Math.random();
    const snack: SnackParams = { ...option, key, message, open: true };

    setSnacks((qu) => {
      const isDuplicated =
        qu.findIndex((s) => s.key === key || s.message === message) !== -1;

      if (isDuplicated) return qu;
      return immer(qu, (q) => void q.push(snack));
    });
  };
  const closeSnackbar = (key?: string) => {
    setSnacks((qu) =>
      immer(qu, (q) => {
        if (key) {
          const snackToClose = q.find((s) => s.key === key);
          if (snackToClose) snackToClose.open = false;
        } else {
          const [current] = q;
          if (current) {
            return [{...current, open: false}]
          }
        }
      })
    );
  };
  const killSnackbar = (key: string) => {
    setSnacks((qu) => immer(qu, (q) => (q = q.filter((s) => s.key !== key))));
  };
  const contextValue = React.useRef<ProviderContext>([
    createSnackbar,
    closeSnackbar,
  ]);
  const [snack] = snacks;
  const handleClose = () => closeSnackbar(snack.key);
  const handleKill = () => killSnackbar(snack.key);

  return (
    <SnackbarContext.Provider value={contextValue.current}>
      {children}
      {snack && (
        <SnackbarContainer
          onClose={handleClose}
          onKill={handleKill}
          {...snack}
        />
      )}
    </SnackbarContext.Provider>
  );
}
