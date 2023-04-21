import React, { lazy, Suspense } from 'react';

const LazyResourceGroups = lazy(() => import('./ResourceGroups'));

const ResourceGroups = props => (
  <Suspense fallback={null}>
    <LazyResourceGroups {...props} />
  </Suspense>
);

export default ResourceGroups;
