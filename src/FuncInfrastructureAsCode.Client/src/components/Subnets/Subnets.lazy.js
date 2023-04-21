import React, { lazy, Suspense } from 'react';

const LazySubnets = lazy(() => import('./Subnets'));

const Subnets = props => (
  <Suspense fallback={null}>
    <LazySubnets {...props} />
  </Suspense>
);

export default Subnets;
