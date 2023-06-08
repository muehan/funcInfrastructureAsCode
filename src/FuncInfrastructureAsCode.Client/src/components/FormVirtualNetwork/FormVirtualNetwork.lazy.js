import React, { lazy, Suspense } from 'react';

const LazyFormVirtualNetwork = lazy(() => import('./FormVirtualNetwork'));

const FormVirtualNetwork = props => (
  <Suspense fallback={null}>
    <LazyFormVirtualNetwork {...props} />
  </Suspense>
);

export default FormVirtualNetwork;
