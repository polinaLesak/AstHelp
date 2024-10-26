import { configureStore } from '@reduxjs/toolkit';
import loginReducer from '../features/auth/model/loginSlice';
import registrationReducer from '../features/auth/model/registrationSlice';
import userReducer from '../entities/user/model/userSlice';

export const store = configureStore({
  reducer: {
    login: loginReducer,
    registration: registrationReducer,
    user: userReducer,
  },
});
