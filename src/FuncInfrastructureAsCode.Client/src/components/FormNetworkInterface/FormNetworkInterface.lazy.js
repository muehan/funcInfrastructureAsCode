import React, { lazy, Suspense } from 'react';

const LazyFormNetworkInterface = lazy(() => import('./FormNetworkInterface'));

const FormNetworkInterface = props => (
  <Suspense fallback={null}>
    <LazyFormNetworkInterface {...props} />
  </Suspense>
);

export default FormNetworkInterface;
