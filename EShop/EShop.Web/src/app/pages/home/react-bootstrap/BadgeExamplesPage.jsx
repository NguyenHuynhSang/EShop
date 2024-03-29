import React from 'react';
import Notice from '../../../partials/content/Notice';
import CodeExample from '../../../partials/content/CodeExample';
import { Badge, Button } from 'react-bootstrap';

export default class BadgeExamplesPage extends React.Component {
  render() {
    return (
      <>
        <Notice icon='flaticon-warning kt-font-primary'>
          <p>
            Badges scale to match the size of the immediate parent element by
            using relative font sizing and em units.
          </p>
          <p>
            For more info please check the components's official{' '}
            <a
              target='_blank'
              className='kt-link'
              rel='noopener noreferrer'
              href='https://react-bootstrap.github.io/components/badge/'
            >
              demos & documentation
            </a>
          </p>
        </Notice>

        <div className='row'>
          <div className='col-md-6'>
            <CodeExample jsCode={jsCode1} beforeCodeTitle='Basic Example'>
              <div className='kt-section'>
                <span className='kt-section__sub'></span>
                <div>
                  <h1>
                    Example heading <Badge variant='secondary'>New</Badge>
                  </h1>
                  <h2>
                    Example heading <Badge variant='secondary'>New</Badge>
                  </h2>
                  <h3>
                    Example heading <Badge variant='secondary'>New</Badge>
                  </h3>
                  <h4>
                    Example heading <Badge variant='secondary'>New</Badge>
                  </h4>
                  <h5>
                    Example heading <Badge variant='secondary'>New</Badge>
                  </h5>
                  <h6>
                    Example heading <Badge variant='secondary'>New</Badge>
                  </h6>
                </div>
              </div>
            </CodeExample>
          </div>
          <div className='col-md-6'>
            <CodeExample jsCode={jsCode2} beforeCodeTitle='Button'>
              <div className='kt-section'>
                <span className='kt-section__sub'>
                  Badges can be used as part of links or buttons to provide a
                  counter.
                </span>
                <div className='kt-separator kt-separator--dashed'></div>
                <Button variant='primary'>
                  Profile <Badge variant='light'>9</Badge>
                  <span className='sr-only'>unread messages</span>
                </Button>
              </div>
            </CodeExample>
          </div>
        </div>

        <div className='row'>
          <div className='col-md-6'>
            <CodeExample
              jsCode={jsCode3}
              beforeCodeTitle='Contextual variations'
            >
              <div className='kt-section'>
                <span className='kt-section__sub'>
                  Add any of the below mentioned modifier classes to change the
                  appearance of a badge.
                </span>
                <div className='kt-separator kt-separator--dashed'></div>
                <div>
                  <Badge variant='primary'>Primary</Badge>
                  <Badge variant='secondary'>Secondary</Badge>
                  <Badge variant='success'>Success</Badge>
                  <Badge variant='danger'>Danger</Badge>
                  <Badge variant='warning'>Warning</Badge>
                  <Badge variant='info'>Info</Badge>
                  <Badge variant='light'>Light</Badge>
                  <Badge variant='dark'>Dark</Badge>
                </div>
              </div>
            </CodeExample>
          </div>
          <div className='col-md-6'>
            <CodeExample jsCode={jsCode4} beforeCodeTitle='Pill'>
              <div className='kt-section'>
                <span className='kt-section__sub'>
                  badges Use the <code>pill</code> modifier class to make badges
                  more rounded (with a larger border-radius and additional
                  horizontal <code>padding</code>). Useful if you miss the
                  badges from v3.
                </span>
                <div className='kt-separator kt-separator--dashed'></div>
                <div>
                  <Badge pill variant='primary'>
                    Primary
                  </Badge>
                  <Badge pill variant='secondary'>
                    Secondary
                  </Badge>
                  <Badge pill variant='success'>
                    Success
                  </Badge>
                  <Badge pill variant='danger'>
                    Danger
                  </Badge>
                  <Badge pill variant='warning'>
                    Warning
                  </Badge>
                  <Badge pill variant='info'>
                    Info
                  </Badge>
                  <Badge pill variant='light'>
                    Light
                  </Badge>
                  <Badge pill variant='dark'>
                    Dark
                  </Badge>
                </div>
              </div>
            </CodeExample>
          </div>
        </div>
      </>
    );
  }
}

const jsCode1 = `
<div>
  <h1>
    Example heading <Badge variant="secondary">New</Badge>
  </h1>
  <h2>
    Example heading <Badge variant="secondary">New</Badge>
  </h2>
  <h3>
    Example heading <Badge variant="secondary">New</Badge>
  </h3>
  <h4>
    Example heading <Badge variant="secondary">New</Badge>
  </h4>
  <h5>
    Example heading <Badge variant="secondary">New</Badge>
  </h5>
  <h6>
    Example heading <Badge variant="secondary">New</Badge>
  </h6>
</div>
`;
const jsCode2 = `
<Button variant="primary">
  Profile <Badge variant="light">9</Badge>
  <span className="sr-only">unread messages</span>
</Button>
`;
const jsCode3 = `
<div>
  <Badge variant="primary">Primary</Badge>
  <Badge variant="secondary">Secondary</Badge>
  <Badge variant="success">Success</Badge>
  <Badge variant="danger">Danger</Badge>
  <Badge variant="warning">Warning</Badge>
  <Badge variant="info">Info</Badge>
  <Badge variant="light">Light</Badge>
  <Badge variant="dark">Dark</Badge>
</div>
`;
const jsCode4 = `
<div>
  <Badge pill variant="primary">
    Primary
  </Badge>
  <Badge pill variant="secondary">
    Secondary
  </Badge>
  <Badge pill variant="success">
    Success
  </Badge>
  <Badge pill variant="danger">
    Danger
  </Badge>
  <Badge pill variant="warning">
    Warning
  </Badge>
  <Badge pill variant="info">
    Info
  </Badge>
  <Badge pill variant="light">
    Light
  </Badge>
  <Badge pill variant="dark">
    Dark
  </Badge>
</div>
`;
