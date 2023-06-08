import React, { lazy, Suspense } from 'react';

const LazyFormResourceGroup = lazy(() => import('./FormResourceGroup'));

const FormResourceGroup = props => (
  <Suspense fallback={null}>
    <LazyFormResourceGroup {...props} />
  </Suspense>
);

export default FormResourceGroup;
