import axiosClient from '../../../app/axiosClient';
import { createAsyncThunk } from '@reduxjs/toolkit';

export const fetchUser = createAsyncThunk('user/fetchUser', async (userId) => {
  const response = await axiosClient.get(`/identity/Auth/Login${userId}`);
  return response.data;
});
