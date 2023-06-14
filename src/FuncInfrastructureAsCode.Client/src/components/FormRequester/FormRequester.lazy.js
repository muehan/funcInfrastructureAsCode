import React, { lazy, Suspense } from 'react';

const LazyFormRequester = lazy(() => import('./FormRequester'));

const FormRequester = props => (
  <Suspense fallback={null}>
    <LazyFormRequester {...props} />
  </Suspense>
);

export default FormRequester;
