import React, { lazy, Suspense } from 'react';

const LazyRequestVirtualMachine = lazy(() => import('./RequestVirtualMachine'));

const RequestVirtualMachine = props => (
  <Suspense fallback={null}>
    <LazyRequestVirtualMachine {...props} />
  </Suspense>
);

export default RequestVirtualMachine;
