import React from 'react';
import { useSelector } from 'react-redux';
import { Backdrop, CircularProgress } from '@mui/material';

const LoadingOverlay = () => {
  const isLoginLoading = useSelector((state) => state.login.loading);
  const isRegistrationLoading = useSelector((state) => state.registration.loading);

  const isLoading = isLoginLoading || isRegistrationLoading;

  return (
    <Backdrop
      sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
      open={isLoading}
    >
      <CircularProgress color="inherit" />
    </Backdrop>
  );
};

export default LoadingOverlay;
