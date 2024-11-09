import { configureStore } from '@reduxjs/toolkit';
import loginReducer from '../features/auth/model/loginSlice';
import registrationReducer from '../features/auth/model/registrationSlice';
import notificationsReducer from '../entities/notifications/model/notificationsSlice';
import userReducer from '../entities/user/model/userSlice';
import catalogReducer from '../entities/catalog/model/catalogSlice';
import brandReducer from '../entities/brand/model/brandSlice';
import attributesReducer from '../entities/attribute/model/attributeSlice';
import productReducer from '../entities/product/model/productSlice';
import cartReducer from '../entities/cart/model/cartSlice';
import orderReducer from '../entities/order/model/orderSlice';

export const store = configureStore({
  reducer: {
    login: loginReducer,
    registration: registrationReducer,
    user: userReducer,
    catalog: catalogReducer,
    brand: brandReducer,
    attribute: attributesReducer,
    product: productReducer,
    cart: cartReducer,
    order: orderReducer,
    notifications: notificationsReducer,
  },
});
