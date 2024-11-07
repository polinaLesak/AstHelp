import React, { useEffect, useMemo } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { clearLoginError, clearLoginSuccess } from '../../features/auth/model/loginSlice';
import { clearRegistrationError, clearRegistrationSuccess } from '../../features/auth/model/registrationSlice';
import { clearUserError, clearUserSuccess } from '../../entities/user/model/userSlice';
import { clearCatalogError, clearCatalogSuccess } from '../../entities/catalog/model/catalogSlice';
import { clearBrandError, clearBrandSuccess } from '../../entities/brand/model/brandSlice';
import { clearAttributeError, clearAttributeSuccess } from '../../entities/attribute/model/attributeSlice';
import { clearProductError, clearProductSuccess } from '../../entities/product/model/productSlice';

const ErrorToast = () => {
  const dispatch = useDispatch();

  const loginError = useSelector((state) => state.login.error);
  const registrationError = useSelector((state) => state.registration.error);
  const userError = useSelector((state) => state.user.error);
  const catalogError = useSelector((state) => state.catalog.error);
  const brandError = useSelector((state) => state.brand.error);
  const attributeError = useSelector((state) => state.attribute.error);
  const productError = useSelector((state) => state.product.error);

  const loginSuccess = useSelector((state) => state.login.success);
  const registrationSuccess = useSelector((state) => state.registration.success);
  const userSuccess = useSelector((state) => state.user.success);
  const catalogSuccess = useSelector((state) => state.catalog.success);
  const brandSuccess = useSelector((state) => state.brand.success);
  const attributeSuccess = useSelector((state) => state.attribute.success);
  const productSuccess = useSelector((state) => state.product.success);

  const states = useMemo(() => [
    { error: loginError, success: loginSuccess, clearError: clearLoginError, clearSuccess: clearLoginSuccess },
    { error: registrationError, success: registrationSuccess, clearError: clearRegistrationError, clearSuccess: clearRegistrationSuccess },
    { error: userError, success: userSuccess, clearError: clearUserError, clearSuccess: clearUserSuccess },
    { error: catalogError, success: catalogSuccess, clearError: clearCatalogError, clearSuccess: clearCatalogSuccess },
    { error: brandError, success: brandSuccess, clearError: clearBrandError, clearSuccess: clearBrandSuccess },
    { error: attributeError, success: attributeSuccess, clearError: clearAttributeError, clearSuccess: clearAttributeSuccess },
    { error: productError, success: productSuccess, clearError: clearProductError, clearSuccess: clearProductSuccess },
  ], [loginError, registrationError, userError, catalogError, brandError, attributeError, productError,
    loginSuccess, registrationSuccess, userSuccess, catalogSuccess, brandSuccess, attributeSuccess, productSuccess]);


  useEffect(() => {
    states.forEach(({ error, clearError }) => {
      if (error) {
        const message = error.statusCode
          ? `Ошибка ${error.statusCode}: ${error.message}`
          : `Ошибка: ${JSON.stringify(error)}`;
        toast.error(message);
        dispatch(clearError())
      }
    });
  }, [states, dispatch]);

  useEffect(() => {
    states.forEach(({ success, clearSuccess }) => {
      if (success) {
        toast.success(success);
        dispatch(clearSuccess())
      }
    });
  }, [states, dispatch]);

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