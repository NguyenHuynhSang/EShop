/* eslint-disable no-restricted-imports */
import React from 'react';
import Notice from '../../../../partials/content/Notice';
import CodeExample from '../../../../partials/content/CodeExample';
import { makeStyles } from '@material-ui/core/styles';
import { Typography } from '@material-ui/core';

// Example 1
const useStyles1 = makeStyles({
  root: {
    width: '100%',
    maxWidth: 500,
  },
});

// Example 2
const useStyles2 = makeStyles(theme => ({
  root: {
    ...theme.typography.button,
    backgroundColor: theme.palette.background.paper,
    padding: theme.spacing(1),
  },
}));

export default function TypographyExamplesPage() {
  // Example 1
  const classes1 = useStyles1();

  // Example 2
  const classes2 = useStyles2();

  return (
    <>
      <Notice icon='flaticon-warning kt-font-primary'>
        <p>
          Use typography to present your design and content as clearly and
          efficiently as possible.
        </p>
        <p>
          For more info please check the components's official{' '}
          <a
            target='_blank'
            className='kt-link'
            rel='noopener noreferrer'
            href='https://material-ui.com/components/typography/'
          >
            demos & documentation
          </a>
        </p>
      </Notice>

      <CodeExample jsCode={jsCode1} beforeCodeTitle='Component'>
        <div className='kt-section'>
          <span className='kt-section__sub'></span>
          <div className='kt-section__content'>
            <div className={classes1.root}>
              <Typography variant='h1' component='h2' gutterBottom>
                h1. Heading
              </Typography>
              <Typography variant='h2' gutterBottom>
                h2. Heading
              </Typography>
              <Typography variant='h3' gutterBottom>
                h3. Heading
              </Typography>
              <Typography variant='h4' gutterBottom>
                h4. Heading
              </Typography>
              <Typography variant='h5' gutterBottom>
                h5. Heading
              </Typography>
              <Typography variant='h6' gutterBottom>
                h6. Heading
              </Typography>
              <Typography variant='subtitle1' gutterBottom>
                subtitle1. Lorem ipsum dolor sit amet, consectetur adipisicing
                elit. Quos blanditiis tenetur
              </Typography>
              <Typography variant='subtitle2' gutterBottom>
                subtitle2. Lorem ipsum dolor sit amet, consectetur adipisicing
                elit. Quos blanditiis tenetur
              </Typography>
              <Typography variant='body1' gutterBottom>
                body1. Lorem ipsum dolor sit amet, consectetur adipisicing elit.
                Quos blanditiis tenetur unde suscipit, quam beatae rerum
                inventore consectetur, neque doloribus, cupiditate numquam
                dignissimos laborum fugiat deleniti? Eum quasi quidem quibusdam.
              </Typography>
              <Typography variant='body2' gutterBottom>
                body2. Lorem ipsum dolor sit amet, consectetur adipisicing elit.
                Quos blanditiis tenetur unde suscipit, quam beatae rerum
                inventore consectetur, neque doloribus, cupiditate numquam
                dignissimos laborum fugiat deleniti? Eum quasi quidem quibusdam.
              </Typography>
              <Typography variant='button' display='block' gutterBottom>
                button text
              </Typography>
              <Typography variant='caption' display='block' gutterBottom>
                caption text
              </Typography>
              <Typography variant='overline' display='block' gutterBottom>
                overline text
              </Typography>
            </div>
          </div>
        </div>
      </CodeExample>

      <CodeExample jsCode={jsCode2} beforeCodeTitle='Theme'>
        <div className='kt-section'>
          <span className='kt-section__sub'>
            In some situations you might not be able to use the{' '}
            <code>Typography</code> component. Hopefully, you might be able to
            take advantage of the <code>typography</code> keys of the theme.
          </span>
          <div className='kt-separator kt-separator--dashed'></div>
          <div className='kt-section__content'>
            <div className={classes2.root}>
              {"This div's text looks like that of a button."}
            </div>
          </div>
        </div>
      </CodeExample>
    </>
  );
}

const jsCode1 = `
import React from 'react';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles({
  root: {
    width: '100%',
    maxWidth: 500,
  },
});

export default function Types() {
  const classes = useStyles();

  return (
    <div className={classes.root}>
      <Typography variant="h1" component="h2" gutterBottom>
        h1. Heading
      </Typography>
      <Typography variant="h2" gutterBottom>
        h2. Heading
      </Typography>
      <Typography variant="h3" gutterBottom>
        h3. Heading
      </Typography>
      <Typography variant="h4" gutterBottom>
        h4. Heading
      </Typography>
      <Typography variant="h5" gutterBottom>
        h5. Heading
      </Typography>
      <Typography variant="h6" gutterBottom>
        h6. Heading
      </Typography>
      <Typography variant="subtitle1" gutterBottom>
        subtitle1. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quos blanditiis tenetur
      </Typography>
      <Typography variant="subtitle2" gutterBottom>
        subtitle2. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quos blanditiis tenetur
      </Typography>
      <Typography variant="body1" gutterBottom>
        body1. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quos blanditiis tenetur
        unde suscipit, quam beatae rerum inventore consectetur, neque doloribus, cupiditate numquam
        dignissimos laborum fugiat deleniti? Eum quasi quidem quibusdam.
      </Typography>
      <Typography variant="body2" gutterBottom>
        body2. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quos blanditiis tenetur
        unde suscipit, quam beatae rerum inventore consectetur, neque doloribus, cupiditate numquam
        dignissimos laborum fugiat deleniti? Eum quasi quidem quibusdam.
      </Typography>
      <Typography variant="button" display="block" gutterBottom>
        button text
      </Typography>
      <Typography variant="caption" display="block" gutterBottom>
        caption text
      </Typography>
      <Typography variant="overline" display="block" gutterBottom>
        overline text
      </Typography>
    </div>
  );
}
`;
const jsCode2 = `
import React from 'react';
import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles(theme => ({
  root: {
    ...theme.typography.button,
    backgroundColor: theme.palette.background.paper,
    padding: theme.spacing(1),
  },
}));

export default function TypographyTheme() {
  const classes = useStyles();

  return <div className={classes.root}>{"This div's text looks like that of a button."}</div>;
}
`;
