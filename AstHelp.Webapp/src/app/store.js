import { configureStore } from '@reduxjs/toolkit';
import loginReducer from '../features/auth/model/loginSlice';
import registrationReducer from '../features/auth/model/registrationSlice';
import userReducer from '../entities/user/model/userSlice';
import catalogReducer from '../entities/catalog/model/catalogSlice';
import brandReducer from '../entities/brand/model/brandSlice';
import attributesReducer from '../entities/attribute/model/attributeSlice';
import productReducer from '../entities/product/model/productSlice';

export const store = configureStore({
  reducer: {
    login: loginReducer,
    registration: registrationReducer,
    user: userReducer,
    catalog: catalogReducer,
    brand: brandReducer,
    attribute: attributesReducer,
    product: productReducer,
  },
});
