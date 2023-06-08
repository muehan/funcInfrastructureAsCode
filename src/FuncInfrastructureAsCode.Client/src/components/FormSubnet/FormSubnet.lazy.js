import React, { lazy, Suspense } from 'react';

const LazyFormSubnet = lazy(() => import('./FormSubnet'));

const FormSubnet = props => (
  <Suspense fallback={null}>
    <LazyFormSubnet {...props} />
  </Suspense>
);

export default FormSubnet;
