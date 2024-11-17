import React from 'react';
import { Route, Routes } from 'react-router-dom';
import HomePage from '../pages/HomePage';
import NotFoundPage from '../pages/NotFoundPage';
import SignInPage from '../pages/SignInPage';
import SignUpPage from '../pages/SignUpPage';
import UsersPage from '../pages/UsersPage';
import SettingsPage from '../pages/SettingsPage';
import ProductPage from '../pages/ProductPage';
import CartPage from '../pages/CartPage';
import OrdersPage from '../pages/OrdersPage';
import OrderDetailsPage from '../pages/OrderDetailsPage';

const AppRoutes = () => {
  return (
      <Routes>
        <Route path="/sign_in" element={<SignInPage />} />
        <Route path="/sign_up" element={<SignUpPage />} />
        <Route path="/" element={<HomePage />} />
        <Route path="/users" element={<UsersPage />} />
        <Route path="/settings" element={<SettingsPage />} />
        <Route path="/catalog" element={<ProductPage />} />
        <Route path="/cart" element={<CartPage />} />
        <Route path="/my_orders" element={<OrdersPage pageType="Мои заказы" />} />
        <Route path="/orders" element={<OrdersPage pageType="Заказы" />} />
        <Route path="/request_orders" element={<OrdersPage pageType="Заявки" />} />
        <Route path="/orders/:orderId" element={<OrderDetailsPage />} />
        <Route path="*" element={<NotFoundPage />} />
      </Routes>
  );
};

export default AppRoutes;