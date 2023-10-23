import { lazy } from 'react';
import { Route, Routes, Navigate } from 'react-router-dom';
import withLazyLoad from '../lib/withLazyLoad';

const PackageUpdatePage = lazy(() => import('../components/PackageUpdateView'));
const PackageVersionsPage = lazy(() => import('../components/PackageVersionView'));

const Router = () => {
  return (
      <Routes>
        <Route index Component={withLazyLoad(PackageUpdatePage)} />
        <Route index path="version" Component={withLazyLoad(PackageVersionsPage)} />
        <Route path="*" element={<Navigate to="/" />} />
      </Routes>
  );
};

export default Router;
