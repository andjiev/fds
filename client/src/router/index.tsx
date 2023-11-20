import { lazy, useEffect } from 'react';
import { Route, Routes, Navigate } from 'react-router-dom';
import withLazyLoad from '../lib/withLazyLoad';
import { useAppDispatch } from '@/hooks';
import { startSignalRConnection, stopSignalRConnection } from '@/store/shared-store';

const PackageUpdateView = lazy(() => import('../components/PackageUpdateView'));

const Router = () => {
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(startSignalRConnection());

    return () => {
      dispatch(stopSignalRConnection());
    }
  }, []);

  return (
    <Routes>
      <Route index Component={withLazyLoad(PackageUpdateView)} />
      <Route path="*" element={<Navigate to="/" />} />
    </Routes>
  );
};

export default Router;
