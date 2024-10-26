import axiosClient from '../../../app/axiosClient';
import { createAsyncThunk } from '@reduxjs/toolkit';

export const fetchUser = createAsyncThunk('user/fetchUser', async (userId) => {
  const response = await axiosClient.get(`/identity/Auth/Login${userId}`);
  return response.data;
});

export const fetchAllUsers = createAsyncThunk('user/fetchAllUsers', async () => {
  const response = await axiosClient.get(`/identity/User/AllUsers`)
  return response.data;
});

export const updateUserStatus = createAsyncThunk('user/updateUserStatus', async (userId) => {
  const response = await axiosClient.post(`/identity/User/Deactivate?id=${userId}`);
  return response.data;
});

export const updateUserProfile = createAsyncThunk('user/updateUserProfile', async (data) => {
  const response = await axiosClient.put(`/identity/User/Update/Profile`, data);
  return response.data;
});

export const deleteUser = createAsyncThunk('user/deleteUser', async (userId) => {
  const response = await axiosClient.delete(`/identity/User/Delete?id=${userId}`);
  return response.data;
});
