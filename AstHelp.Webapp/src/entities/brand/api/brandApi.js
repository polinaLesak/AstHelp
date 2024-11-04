import axiosClient from '../../../app/axiosClient';
import { createAsyncThunk } from '@reduxjs/toolkit';

export const fetchBrand = createAsyncThunk('brand/fetchUser', async (brandId, thunkAPI) => {
  try {
    const response = await axiosClient.get(`/catalog/Brand/${brandId}`);
    return response.data;
  } catch (error) {
    return thunkAPI.rejectWithValue(error);
  }
});

export const fetchAllBrands = createAsyncThunk('brand/fetchAllBrands', async (thunkAPI) => {
  try {
    const response = await axiosClient.get(`/catalog/Brand`)
    return response.data;
  } catch (error) {
    return thunkAPI.rejectWithValue(error);
  }
});

export const createBrand = createAsyncThunk('brand/createBrand', async (data, thunkAPI) => {
  try {
    const response = await axiosClient.post(`/catalog/Brand`, data);
    return response.data;
  } catch (error) {
    return thunkAPI.rejectWithValue(error);
  }
});

export const updateBrand = createAsyncThunk('brand/updateBrand', async (data, thunkAPI) => {
  try {
    const response = await axiosClient.put(`/catalog/Brand`, data);
    return response.data;
  } catch (error) {
    return thunkAPI.rejectWithValue(error);
  }
});

export const deleteBrand = createAsyncThunk('brand/deleteBrand', async (brandId, thunkAPI) => {
  try {
    const response = await axiosClient.delete(`/catalog/Brand?id=${brandId}`);
    return response.data;
  } catch (error) {
    return thunkAPI.rejectWithValue(error);
  }
});
