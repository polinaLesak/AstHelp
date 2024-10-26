import React, { useEffect, useMemo } from 'react';
import { useSelector } from 'react-redux';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const ErrorToast = () => {
  const loginError = useSelector((state) => state.login.error);
  const registrationError = useSelector((state) => state.registration.error);
  const userError = useSelector((state) => state.user.error);

  const loginSuccess = useSelector((state) => state.login.success);
  const registrationSuccess = useSelector((state) => state.registration.success);
  const userSuccess = useSelector((state) => state.user.success);

  const states = useMemo(() => [
    { error: loginError, success: loginSuccess },
    { error: registrationError, success: registrationSuccess },
    { error: userError, success: userSuccess }
  ], [loginError, registrationError, userError, loginSuccess, registrationSuccess, userSuccess]);


  useEffect(() => {
    states.forEach(({ error }) => {
      if (error) {
        const message = error.statusCode
          ? `Ошибка ${error.statusCode}: ${error.message}`
          : `Ошибка: ${JSON.stringify(error)}`;
        toast.error(message);
      }
    });
  }, [states]);

  useEffect(() => {
    states.forEach(({ success }) => {
      if (success) {
        toast.success(success);
      }
    });
  }, [states]);

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