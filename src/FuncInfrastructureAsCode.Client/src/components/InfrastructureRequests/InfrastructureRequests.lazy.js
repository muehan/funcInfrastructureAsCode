import React, { lazy, Suspense } from 'react';

const LazyInfrastructureRequests = lazy(() => import('./InfrastructureRequests'));

const InfrastructureRequests = props => (
  <Suspense fallback={null}>
    <LazyInfrastructureRequests {...props} />
  </Suspense>
);

export default InfrastructureRequests;
