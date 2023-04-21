import React, { lazy, Suspense } from 'react';

const LazyAppNavbar = lazy(() => import('./AppNavbar'));

const AppNavbar = props => (
  <Suspense fallback={null}>
    <LazyAppNavbar {...props} />
  </Suspense>
);

export default AppNavbar;
