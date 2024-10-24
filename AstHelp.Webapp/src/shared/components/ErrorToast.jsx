import React, { useEffect } from 'react';
import { useSelector } from 'react-redux';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const ErrorToast = () => {
  const loginError = useSelector((state) => state.login.error);
  const registrationError = useSelector((state) => state.registration.error);

  const loginSuccess = useSelector((state) => state.login.success);
  const registrationSuccess = useSelector((state) => state.registration.success);

  useEffect(() => {
    if (loginError) {
      if(loginError.statusCode)
        toast.error(`Ошибка ${loginError.statusCode}: ${loginError.message}`);
      else
        toast.error(`Ошибка: ${JSON.stringify(loginError)}`);
    }
    if (registrationError) {
      if(registrationError.statusCode)
        toast.error(`Ошибка ${registrationError.statusCode}: ${registrationError.message}`);
      else
        toast.error(`Ошибка: ${JSON.stringify(registrationError)}`);
    }
  }, [loginError, registrationError]);

  useEffect(() => {
    if (loginSuccess) {
      toast.success(loginSuccess);
    }
    if (registrationSuccess) {
      toast.success(registrationSuccess);
    }
  }, [loginSuccess, registrationSuccess]);

  return (
    <>
      <ToastContainer
        position="bottom-right"
        autoClose={5000}
        hideProgressBar={false}
        newestOnTop={true}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
      />
    </>
  );
};

export default ErrorToast;