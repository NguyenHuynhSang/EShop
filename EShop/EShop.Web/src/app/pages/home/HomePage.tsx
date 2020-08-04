import React, { Suspense, lazy } from 'react';
import { Redirect, Route, Switch } from 'react-router-dom';
import Builder from './Builder';
import Dashboard from './Dashboard';
import DocsPage from './docs/DocsPage';
import ProductsPage from './products/ProductsPage';
import { LayoutSplashScreen } from '../../../_metronic';
import ErrorBoundary from '../../widgets/ErrorBoundary';

const GoogleMaterialPage = lazy(() =>
  import('./google-material/GoogleMaterialPage')
);
const ReactBootstrapPage = lazy(() =>
  import('./react-bootstrap/ReactBootstrapPage')
);

type HomePageProps = {
  userLastLocation: string;
};

export default function HomePage(props: HomePageProps): JSX.Element {
  return (
    <ErrorBoundary>
      <Suspense fallback={<LayoutSplashScreen />}>
        <Switch>
          {
            /* Redirect from root URL to /dashboard. */
            <Redirect exact from='/' to='/dashboard' />
          }
          <Route path='/products' component={ProductsPage} />
          <Route path='/builder' component={Builder} />
          <Route path='/dashboard' component={Dashboard} />
          <Route path='/google-material' component={GoogleMaterialPage} />
          <Route path='/react-bootstrap' component={ReactBootstrapPage} />
          <Route path='/docs' component={DocsPage} />
          <Redirect to='/error' />
        </Switch>
      </Suspense>
    </ErrorBoundary>
  );
}
