import * as yup from 'yup';

export const loginValidationSchema = yup.object().shape({
  username: yup.string().min(3, 'Логин должен быть не менее 3 символов').required('Введите логин'),
  password: yup.string().min(3, 'Пароль должен быть не менее 3 символов').required('Введите пароль'),
});

export const registerValidationSchema = yup.object().shape({
  username: yup.string().required('Введите логин'),
  email: yup.string().email('Некорректный Email').required('Введите Email'),
  fullname: yup.string().required('Введите ФИО'),
  position: yup.string().required('Введите должность'),
  password: yup.string().min(3, 'Пароль должен быть не менее 3 символов').required('Введите пароль'),
  confirmPassword: yup.string()
    .oneOf([yup.ref('password'), null], 'Пароль не совпадает')
    .required('Введите пароль подтверждения'),
});