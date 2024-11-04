import React from 'react';
import { Route, Routes } from 'react-router-dom';
import HomePage from '../pages/HomePage';
import NotFoundPage from '../pages/NotFoundPage';
import SignInPage from '../pages/SignInPage';
import SignUpPage from '../pages/SignUpPage';
import UsersPage from '../pages/UsersPage';
import SettingsPage from '../pages/SettingsPage';

const AppRoutes = () => {
  return (
      <Routes>
        <Route path="/sign_in" element={<SignInPage />} />
        <Route path="/sign_up" element={<SignUpPage />} />
        <Route path="/" element={<HomePage />} />
        <Route path="/users" element={<UsersPage />} />
        <Route path="/settings" element={<SettingsPage />} />
        <Route path="*" element={<NotFoundPage />} />
      </Routes>
  );
};

export default AppRoutes;