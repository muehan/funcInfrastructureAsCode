import React, { lazy, Suspense } from 'react';

const LazyNetworkInterfaces = lazy(() => import('./NetworkInterfaces'));

const NetworkInterfaces = props => (
  <Suspense fallback={null}>
    <LazyNetworkInterfaces {...props} />
  </Suspense>
);

export default NetworkInterfaces;
