import React, { lazy, Suspense } from 'react';

const LazyFormVirtualMachine = lazy(() => import('./FormVirtualMachine'));

const FormVirtualMachine = props => (
  <Suspense fallback={null}>
    <LazyFormVirtualMachine {...props} />
  </Suspense>
);

export default FormVirtualMachine;
