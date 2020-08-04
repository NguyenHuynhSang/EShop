/* eslint-disable no-restricted-imports */
import React from 'react';
import Notice from '../../../../partials/content/Notice';
import CodeExample from '../../../../partials/content/CodeExample';
import { makeStyles, emphasize, withStyles } from '@material-ui/core/styles';
import {
  Paper,
  Breadcrumbs,
  Link,
  Typography,
  Chip,
  Avatar,
} from '@material-ui/core';
import NavigateNextIcon from '@material-ui/icons/NavigateNext';
import HomeIcon from '@material-ui/icons/Home';
import WhatshotIcon from '@material-ui/icons/Whatshot';
import GrainIcon from '@material-ui/icons/Grain';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';

// Example1
const useStyles1 = makeStyles(theme => ({
  root: {
    justifyContent: 'center',
    flexWrap: 'wrap',
  },
  paper: {
    padding: theme.spacing(1, 2),
  },
}));

function handleClick1(event) {
  event.preventDefault();
  alert('You clicked a breadcrumb.');
}

// Example 2
const useStyles2 = makeStyles(theme => ({
  root: {
    justifyContent: 'center',
    flexWrap: 'wrap',
  },
  paper: {
    padding: theme.spacing(1, 2),
  },
}));

function handleClick2(event) {
  event.preventDefault();
  alert('You clicked a breadcrumb.');
}

// Example 3
const useStyles3 = makeStyles(theme => ({
  root: {
    padding: theme.spacing(1, 2),
  },
  link: {
    display: 'flex',
  },
  icon: {
    marginRight: theme.spacing(0.5),
    width: 20,
    height: 20,
  },
}));

function handleClick3(event) {
  event.preventDefault();
  alert('You clicked a breadcrumb.');
}

// Example 4
const useStyles4 = makeStyles(theme => ({
  root: {
    justifyContent: 'center',
    flexWrap: 'wrap',
  },
  paper: {
    padding: theme.spacing(1, 2),
  },
}));

function handleClick4(event) {
  event.preventDefault();
  alert('You clicked a breadcrumb.');
}

// Example 5
const StyledBreadcrumb = withStyles(theme => ({
  root: {
    backgroundColor: theme.palette.grey[100],
    height: 24,
    color: theme.palette.grey[800],
    fontWeight: theme.typography.fontWeightRegular,
    '&:hover, &:focus': {
      backgroundColor: theme.palette.grey[300],
    },
    '&:active': {
      boxShadow: theme.shadows[1],
      backgroundColor: emphasize(theme.palette.grey[300], 0.12),
    },
  },
}))(Chip); // TypeScript only: need a type cast here because https://github.com/Microsoft/TypeScript/issues/26591

function handleClick5(event) {
  event.preventDefault();
  alert('You clicked a breadcrumb.');
}

const useStyles5 = makeStyles(theme => ({
  root: {
    padding: theme.spacing(1),
  },
  avatar: {
    background: 'none',
    marginRight: -theme.spacing(1.5),
  },
}));

export default function BreadcrumbsExamplesPage() {
  // Example 1
  const classes1 = useStyles1();

  // Example 2
  const classes2 = useStyles2();

  // Example 3
  const classes3 = useStyles3();

  // Example 4
  const classes4 = useStyles4();

  // Example 5
  const classes5 = useStyles5();

  return (
    <>
      <Notice icon='flaticon-warning kt-font-primary'>
        <p>
          Breadcrumbs allow users to make selections from a range of values.
        </p>
        <p>
          For more info please check the components's official{' '}
          <a
            target='_blank'
            className='kt-link'
            rel='noopener noreferrer'
            href='https://material-ui.com/components/breadcrumbs/'
          >
            demos & documentation
          </a>
        </p>
      </Notice>

      <div className='row'>
        <div className='col-md-6'>
          <CodeExample jsCode={jsCode1} beforeCodeTitle='Simple breadcrumbs'>
            <div className='kt-section'>
              <span className='kt-section__sub'></span>
              <div className='kt-section__content'>
                <div className={classes1.root}>
                  <Paper elevation={0} className={classes1.paper}>
                    <Breadcrumbs aria-label='Breadcrumb'>
                      <Link color='inherit' href='/' onClick={handleClick1}>
                        Material-UI
                      </Link>
                      <Link
                        color='inherit'
                        href='/getting-started/installation/'
                        onClick={handleClick1}
                      >
                        Core
                      </Link>
                      <Typography color='textPrimary'>Breadcrumb</Typography>
                    </Breadcrumbs>
                  </Paper>
                  <br />
                  <Paper elevation={0} className={classes1.paper}>
                    <Breadcrumbs aria-label='Breadcrumb'>
                      <Link color='inherit' href='/' onClick={handleClick1}>
                        Material-UI
                      </Link>
                      <Link
                        color='inherit'
                        href='/getting-started/installation/'
                        onClick={handleClick1}
                      >
                        Core
                      </Link>
                      <Link
                        color='textPrimary'
                        href='/components/breadcrumbs/'
                        onClick={handleClick1}
                        aria-current='page'
                      >
                        Breadcrumb
                      </Link>
                    </Breadcrumbs>
                  </Paper>
                </div>
              </div>
            </div>
          </CodeExample>
        </div>
        <div className='col-md-6'>
          <CodeExample jsCode={jsCode2} beforeCodeTitle='Custom separator'>
            <div className='kt-section'>
              <span className='kt-section__sub'>
                In the following examples, we are using two string separators,
                and an SVG icon.
              </span>
              <div className='kt-separator kt-separator--dashed'></div>
              <div className='kt-section__content'>
                <div className={classes2.root}>
                  <Paper elevation={0} className={classes2.paper}>
                    <Breadcrumbs separator='›' aria-label='Breadcrumb'>
                      <Link color='inherit' href='/' onClick={handleClick2}>
                        Material-UI
                      </Link>
                      <Link
                        color='inherit'
                        href='/getting-started/installation/'
                        onClick={handleClick2}
                      >
                        Core
                      </Link>
                      <Typography color='textPrimary'>Breadcrumb</Typography>
                    </Breadcrumbs>
                  </Paper>
                  <br />
                  <Paper elevation={0} className={classes2.paper}>
                    <Breadcrumbs separator='-' aria-label='Breadcrumb'>
                      <Link color='inherit' href='/' onClick={handleClick2}>
                        Material-UI
                      </Link>
                      <Link
                        color='inherit'
                        href='/getting-started/installation/'
                        onClick={handleClick2}
                      >
                        Core
                      </Link>
                      <Typography color='textPrimary'>Breadcrumb</Typography>
                    </Breadcrumbs>
                  </Paper>
                  <br />
                  <Paper elevation={0} className={classes2.paper}>
                    <Breadcrumbs
                      separator={<NavigateNextIcon fontSize='small' />}
                      aria-label='Breadcrumb'
                    >
                      <Link color='inherit' href='/' onClick={handleClick2}>
                        Material-UI
                      </Link>
                      <Link
                        color='inherit'
                        href='/getting-started/installation/'
                        onClick={handleClick2}
                      >
                        Core
                      </Link>
                      <Typography color='textPrimary'>Breadcrumb</Typography>
                    </Breadcrumbs>
                  </Paper>
                </div>
              </div>
            </div>
          </CodeExample>
        </div>
      </div>

      <div className='row'>
        <div className='col-md-6'>
          <CodeExample
            jsCode={jsCode3}
            beforeCodeTitle='Breadcrumbs with icons'
          >
            <div className='kt-section'>
              <span className='kt-section__sub'></span>
              <div className='kt-section__content'>
                <Paper elevation={0} className={classes3.root}>
                  <Breadcrumbs aria-label='Breadcrumb'>
                    <Link
                      color='inherit'
                      href='/'
                      onClick={handleClick3}
                      className={classes3.link}
                    >
                      <HomeIcon className={classes3.icon} />
                      Material-UI
                    </Link>
                    <Link
                      color='inherit'
                      href='/getting-started/installation/'
                      onClick={handleClick3}
                      className={classes3.link}
                    >
                      <WhatshotIcon className={classes3.icon} />
                      Core
                    </Link>
                    <Typography color='textPrimary' className={classes3.link}>
                      <GrainIcon className={classes3.icon} />
                      Breadcrumb
                    </Typography>
                  </Breadcrumbs>
                </Paper>
              </div>
            </div>
          </CodeExample>
        </div>
        <div className='col-md-6'>
          <CodeExample jsCode={jsCode4} beforeCodeTitle='Collapsed breadcrumbs'>
            <div className='kt-section'>
              <span className='kt-section__sub'></span>
              <div className='kt-separator kt-separator--dashed'></div>
              <div className='kt-section__content'>
                <Paper elevation={0} className={classes4.paper}>
                  <Breadcrumbs maxItems={2} aria-label='Breadcrumb'>
                    <Link color='inherit' href='#' onClick={handleClick4}>
                      Home
                    </Link>
                    <Link color='inherit' href='#' onClick={handleClick4}>
                      Catalog
                    </Link>
                    <Link color='inherit' href='#' onClick={handleClick4}>
                      Accessories
                    </Link>
                    <Link color='inherit' href='#' onClick={handleClick4}>
                      New Collection
                    </Link>
                    <Typography color='textPrimary'>Belts</Typography>
                  </Breadcrumbs>
                </Paper>
              </div>
            </div>
          </CodeExample>
        </div>
      </div>

      <div className='row'>
        <div className='col-md-6'>
          <CodeExample
            jsCode={jsCode5}
            beforeCodeTitle='Customized breadcrumbs'
          >
            <div className='kt-section'>
              <span className='kt-section__sub'>
                Here is an example of customizing the component. You can learn
                more about this in the overrides documentation page.
              </span>
              <div className='kt-separator kt-separator--dashed'></div>
              <div className='kt-section__content'>
                <Paper elevation={0} className={classes5.root}>
                  <Breadcrumbs aria-label='Breadcrumb'>
                    <StyledBreadcrumb
                      component='a'
                      href='#'
                      label='Home'
                      avatar={
                        <Avatar className={classes5.avatar}>
                          <HomeIcon />
                        </Avatar>
                      }
                      onClick={handleClick5}
                    />
                    <StyledBreadcrumb
                      component='a'
                      href='#'
                      label='Catalog'
                      onClick={handleClick5}
                    />
                    <StyledBreadcrumb
                      label='Accessories'
                      deleteIcon={<ExpandMoreIcon />}
                      onClick={handleClick5}
                      onDelete={handleClick5}
                    />
                  </Breadcrumbs>
                </Paper>
              </div>
            </div>
          </CodeExample>
        </div>
      </div>
    </>
  );
}

const jsCode1 = `
import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Typography from '@material-ui/core/Typography';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import Link from '@material-ui/core/Link';

const useStyles = makeStyles(theme => ({
  root: {
    justifyContent: 'center',
    flexWrap: 'wrap',
  },
  paper: {
    padding: theme.spacing(1, 2),
  },
}));

function handleClick(event) {
  event.preventDefault();
  alert('You clicked a breadcrumb.');
}

export default function SimpleBreadcrumbs() {
  const classes = useStyles();

  return (
    <div className={classes.root}>
      <Paper elevation={0} className={classes.paper}>
        <Breadcrumbs aria-label="Breadcrumb">
          <Link color="inherit" href="/" onClick={handleClick}>
            Material-UI
          </Link>
          <Link color="inherit" href="/getting-started/installation/" onClick={handleClick}>
            Core
          </Link>
          <Typography color="textPrimary">Breadcrumb</Typography>
        </Breadcrumbs>
      </Paper>
      <br />
      <Paper elevation={0} className={classes.paper}>
        <Breadcrumbs aria-label="Breadcrumb">
          <Link color="inherit" href="/" onClick={handleClick}>
            Material-UI
          </Link>
          <Link color="inherit" href="/getting-started/installation/" onClick={handleClick}>
            Core
          </Link>
          <Link
            color="textPrimary"
            href="/components/breadcrumbs/"
            onClick={handleClick}
            aria-current="page"
          >
            Breadcrumb
          </Link>
        </Breadcrumbs>
      </Paper>
    </div>
  );
}
`;
const jsCode2 = `
import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import Typography from '@material-ui/core/Typography';
import Link from '@material-ui/core/Link';
import NavigateNextIcon from '@material-ui/icons/NavigateNext';

const useStyles = makeStyles(theme => ({
  root: {
    justifyContent: 'center',
    flexWrap: 'wrap',
  },
  paper: {
    padding: theme.spacing(1, 2),
  },
}));

function handleClick(event) {
  event.preventDefault();
  alert('You clicked a breadcrumb.');
}

export default function CustomSeparator() {
  const classes = useStyles();

  return (
    <div className={classes.root}>
      <Paper elevation={0} className={classes.paper}>
        <Breadcrumbs separator="›" aria-label="Breadcrumb">
          <Link color="inherit" href="/" onClick={handleClick}>
            Material-UI
          </Link>
          <Link color="inherit" href="/getting-started/installation/" onClick={handleClick}>
            Core
          </Link>
          <Typography color="textPrimary">Breadcrumb</Typography>
        </Breadcrumbs>
      </Paper>
      <br />
      <Paper elevation={0} className={classes.paper}>
        <Breadcrumbs separator="-" aria-label="Breadcrumb">
          <Link color="inherit" href="/" onClick={handleClick}>
            Material-UI
          </Link>
          <Link color="inherit" href="/getting-started/installation/" onClick={handleClick}>
            Core
          </Link>
          <Typography color="textPrimary">Breadcrumb</Typography>
        </Breadcrumbs>
      </Paper>
      <br />
      <Paper elevation={0} className={classes.paper}>
        <Breadcrumbs separator={<NavigateNextIcon fontSize="small" />} aria-label="Breadcrumb">
          <Link color="inherit" href="/" onClick={handleClick}>
            Material-UI
          </Link>
          <Link color="inherit" href="/getting-started/installation/" onClick={handleClick}>
            Core
          </Link>
          <Typography color="textPrimary">Breadcrumb</Typography>
        </Breadcrumbs>
      </Paper>
    </div>
  );
}
`;
const jsCode3 = `
import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Typography from '@material-ui/core/Typography';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import Link from '@material-ui/core/Link';
import HomeIcon from '@material-ui/icons/Home';
import WhatshotIcon from '@material-ui/icons/Whatshot';
import GrainIcon from '@material-ui/icons/Grain';

const useStyles = makeStyles(theme => ({
  root: {
    padding: theme.spacing(1, 2),
  },
  link: {
    display: 'flex',
  },
  icon: {
    marginRight: theme.spacing(0.5),
    width: 20,
    height: 20,
  },
}));

function handleClick(event) {
  event.preventDefault();
  alert('You clicked a breadcrumb.');
}

export default function IconBreadcrumbs() {
  const classes = useStyles();

  return (
    <Paper elevation={0} className={classes.root}>
      <Breadcrumbs aria-label="Breadcrumb">
        <Link color="inherit" href="/" onClick={handleClick} className={classes.link}>
          <HomeIcon className={classes.icon} />
          Material-UI
        </Link>
        <Link
          color="inherit"
          href="/getting-started/installation/"
          onClick={handleClick}
          className={classes.link}
        >
          <WhatshotIcon className={classes.icon} />
          Core
        </Link>
        <Typography color="textPrimary" className={classes.link}>
          <GrainIcon className={classes.icon} />
          Breadcrumb
        </Typography>
      </Breadcrumbs>
    </Paper>
  );
}
`;
const jsCode4 = `
import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import Typography from '@material-ui/core/Typography';
import Link from '@material-ui/core/Link';

const useStyles = makeStyles(theme => ({
  root: {
    justifyContent: 'center',
    flexWrap: 'wrap',
  },
  paper: {
    padding: theme.spacing(1, 2),
  },
}));

function handleClick(event) {
  event.preventDefault();
  alert('You clicked a breadcrumb.');
}

export default function CollapsedBreadcrumbs() {
  const classes = useStyles();

  return (
    <Paper elevation={0} className={classes.paper}>
      <Breadcrumbs maxItems={2} aria-label="Breadcrumb">
        <Link color="inherit" href="#" onClick={handleClick}>
          Home
        </Link>
        <Link color="inherit" href="#" onClick={handleClick}>
          Catalog
        </Link>
        <Link color="inherit" href="#" onClick={handleClick}>
          Accessories
        </Link>
        <Link color="inherit" href="#" onClick={handleClick}>
          New Collection
        </Link>
        <Typography color="textPrimary">Belts</Typography>
      </Breadcrumbs>
    </Paper>
  );
}
`;
const jsCode5 = `
import React from 'react';
import { emphasize, withStyles, makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Breadcrumbs from '@material-ui/core/Breadcrumbs';
import Chip from '@material-ui/core/Chip';
import Avatar from '@material-ui/core/Avatar';
import HomeIcon from '@material-ui/icons/Home';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';

const StyledBreadcrumb = withStyles(theme => ({
  root: {
    backgroundColor: theme.palette.grey[100],
    height: 24,
    color: theme.palette.grey[800],
    fontWeight: theme.typography.fontWeightRegular,
    '&:hover, &:focus': {
      backgroundColor: theme.palette.grey[300],
    },
    '&:active': {
      boxShadow: theme.shadows[1],
      backgroundColor: emphasize(theme.palette.grey[300], 0.12),
    },
  },
}))(Chip); // TypeScript only: need a type cast here because https://github.com/Microsoft/TypeScript/issues/26591

function handleClick(event) {
  event.preventDefault();
  alert('You clicked a breadcrumb.');
}

const useStyles = makeStyles(theme => ({
  root: {
    padding: theme.spacing(1),
  },
  avatar: {
    background: 'none',
    marginRight: -theme.spacing(1.5),
  },
}));

export default function CustomizedBreadcrumbs() {
  const classes = useStyles();

  return (
    <Paper elevation={0} className={classes.root}>
      <Breadcrumbs aria-label="Breadcrumb">
        <StyledBreadcrumb
          component="a"
          href="#"
          label="Home"
          avatar={
            <Avatar className={classes.avatar}>
              <HomeIcon />
            </Avatar>
          }
          onClick={handleClick}
        />
        <StyledBreadcrumb component="a" href="#" label="Catalog" onClick={handleClick} />
        <StyledBreadcrumb
          label="Accessories"
          deleteIcon={<ExpandMoreIcon />}
          onClick={handleClick}
          onDelete={handleClick}
        />
      </Breadcrumbs>
    </Paper>
  );
}
`;
