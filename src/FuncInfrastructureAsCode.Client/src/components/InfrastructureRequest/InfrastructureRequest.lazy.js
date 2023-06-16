import React, { lazy, Suspense } from 'react';

const LazyInfrastructureRequest = lazy(() => import('./InfrastructureRequest'));

const InfrastructureRequest = props => (
  <Suspense fallback={null}>
    <LazyInfrastructureRequest {...props} />
  </Suspense>
);

export default InfrastructureRequest;
