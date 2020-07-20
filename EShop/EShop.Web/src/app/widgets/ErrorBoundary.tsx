import React from "react";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Typography from "@material-ui/core/Typography";
import Collapse from "@material-ui/core/Collapse";
import Paper from "@material-ui/core/Paper";
import IconButton from "@material-ui/core/IconButton";
import ExpandMoreIcon from "@material-ui/icons/ExpandMore";
import { Light as SyntaxHighlighter } from "react-syntax-highlighter";
import { makeStyles, important } from "../styles";
import classNames from "classnames";

const useStyles = makeStyles((theme) => ({
  card: {
    backgroundColor: theme.palette.error.main,
    color: theme.palette.error.contrastText,
  },
  header: {
    fontSize: theme.typography.h4.fontSize,
    padding: theme.spacing(3, 4),
    paddingBottom: theme.spacing(2),
  },
  title: {
    display: "flex",
    alignItems: "center",
    padding: theme.spacing(1, 3),
  },
  expand: {
    marginLeft: theme.spacing(1),
    color: theme.palette.error.contrastText,
    transform: "rotate(0deg)",
    transition: theme.transitions.create("transform", {
      duration: theme.transitions.duration.shortest,
    }),
  },
  expandOpen: {
    transform: "rotate(180deg)",
  },
  body: {
    color: theme.palette.error.contrastText,
    backgroundColor: theme.palette.error.light,
  },
  stacktraces: {
    marginBottom: 0,
    paddingBottom: theme.spacing(2),
    borderRadius: 4,
    backgroundColor: important("rgba(255,255,255,0.7)"),
    "& code": {
      backgroundColor: important("transparent"),
      color: theme.palette.error.dark,
    },
    '& [class$="keyword"]': {
      color: theme.palette.info.main,
    },
    '& [class$="number"]': {
      backgroundColor: theme.palette.warning.main,
      color: theme.palette.warning.contrastText,
    },
  },
}));

type FallbackProps = {
  title: string;
  description: string;
};

function Fallback({ title, description }: FallbackProps) {
  const styles = useStyles();
  const [expanded, setExpanded] = React.useState(false);
  const toggleExpand = () => setExpanded(!expanded);

  return (
    <Card className={styles.card}>
      <Typography variant="h3" className={styles.header}>
        Something went wrong.
      </Typography>
      <CardContent>
        <Paper className={styles.body}>
          <div className={styles.title}>
            <Typography>
              <strong>Error:</strong> {title}
            </Typography>
            <IconButton
              className={classNames(styles.expand, {
                [styles.expandOpen]: expanded,
              })}
              onClick={toggleExpand}
            >
              <ExpandMoreIcon />
            </IconButton>
          </div>
          <Collapse in={expanded} timeout="auto">
            <SyntaxHighlighter
              language="javascript"
              className={styles.stacktraces}
            >
              {description}
            </SyntaxHighlighter>
          </Collapse>
        </Paper>
      </CardContent>
    </Card>
  );
}

type ErrorBoundaryState = {
  error?: Error;
  errorInfo?: React.ErrorInfo;
};

export default class ErrorBoundary extends React.Component<
  {},
  ErrorBoundaryState
> {
  constructor(props) {
    super(props);
    this.state = { error: undefined, errorInfo: undefined };
  }

  componentDidCatch(error: Error, errorInfo: React.ErrorInfo) {
    // Catch errors in any components below and re-render with error message
    this.setState({ error, errorInfo });

    // TODO: log to reporting service
    console.log("You have one job!", error, errorInfo);
    // You can also log error messages to an error reporting service here
  }

  render() {
    if (!this.state.errorInfo) {
      // Normally, just render children
      return this.props.children;
    }

    // Error path
    return (
      <Fallback
        title={this.state.error?.message!}
        description={this.state.errorInfo.componentStack}
      />
    );
  }
}
