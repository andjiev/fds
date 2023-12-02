import { lazy, useEffect } from 'react';
import { Route, Routes, Navigate } from 'react-router-dom';
import withLazyLoad from '../lib/withLazyLoad';
import { useAppDispatch } from '@/hooks';
import { startSignalRConnection, stopSignalRConnection } from '@/store/shared-store';

const PackageView = lazy(() => import('../pages/PackageView'));

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
      <Route index Component={withLazyLoad(PackageView)} />
      <Route path="*" element={<Navigate to="/" />} />
    </Routes>
  );
};

export default Router;
