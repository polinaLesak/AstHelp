import React from 'react';
import { useSelector } from 'react-redux';
import { Backdrop, CircularProgress } from '@mui/material';

const LoadingOverlay = () => {
  const isLoginLoading = useSelector((state) => state.login.loading);
  const isRegistrationLoading = useSelector((state) => state.registration.loading);
  const isUserLoading = useSelector((state) => state.user.loading);
  const isCatalogLoading = useSelector((state) => state.catalog.loading);
  const isBrandLoading = useSelector((state) => state.brand.loading);
  const isAttributeLoading = useSelector((state) => state.attribute.loading);

  const isLoading = isLoginLoading || isRegistrationLoading || isUserLoading
    || isCatalogLoading || isBrandLoading || isAttributeLoading;

  return (
    <Backdrop
      sx={{ color: '#fff', zIndex: 9999 }}
      open={isLoading}
    >
      <CircularProgress color="inherit" />
    </Backdrop>
  );
};

export default LoadingOverlay;
