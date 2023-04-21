import React, { lazy, Suspense } from 'react';

const LazyVirtualNetwork = lazy(() => import('./VirtualNetwork'));

const VirtualNetwork = props => (
  <Suspense fallback={null}>
    <LazyVirtualNetwork {...props} />
  </Suspense>
);

export default VirtualNetwork;
