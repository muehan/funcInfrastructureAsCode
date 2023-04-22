import React, { lazy, Suspense } from 'react';

const LazyVirtualMachines = lazy(() => import('./VirtualMachines'));

const VirtualMachines = props => (
  <Suspense fallback={null}>
    <LazyVirtualMachines {...props} />
  </Suspense>
);

export default VirtualMachines;
