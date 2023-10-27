import { lazy, useEffect } from 'react';
import { Route, Routes, Navigate } from 'react-router-dom';
import withLazyLoad from '../lib/withLazyLoad';
import { useAppDispatch } from '@/hooks';
import { setupSignalRConnection } from '@/store/shared-store';

const PackageUpdateView = lazy(() => import('../components/PackageUpdateView'));
const PackageVersionsView = lazy(() => import('../components/PackageVersionView'));

const Router = () => {
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(setupSignalRConnection());
  }, []);

  return (
    <Routes>
      <Route index Component={withLazyLoad(PackageUpdateView)} />
      <Route index path="versions" Component={withLazyLoad(PackageVersionsView)} />
      <Route path="*" element={<Navigate to="/" />} />
    </Routes>
  );
};

export default Router;
