import axiosClient from '../../../app/axiosClient';
import { createAsyncThunk } from '@reduxjs/toolkit';

export const fetchCatalog = createAsyncThunk('catalog/fetchCatalog', async (catalogId, thunkAPI) => {
  try {
    const response = await axiosClient.get(`/catalog/Catalog/${catalogId}`);
    return response.data;
  } catch (error) {
    return thunkAPI.rejectWithValue(error);
  }
});

export const fetchAllCatalogs = createAsyncThunk('catalog/fetchAllCatalogs', async (thunkAPI) => {
  try {
    const response = await axiosClient.get(`/catalog/Catalog`)
    return response.data;
  } catch (error) {
    return thunkAPI.rejectWithValue(error);
  }
});

export const createCatalog = createAsyncThunk('catalog/createCatalog', async (data, thunkAPI) => {
  try {
    const response = await axiosClient.post(`/catalog/Catalog`, data);
    return response.data;
  } catch (error) {
    return thunkAPI.rejectWithValue(error);
  }
});

export const updateCatalog = createAsyncThunk('catalog/updateCatalog', async (data, thunkAPI) => {
  try {
    const response = await axiosClient.put(`/catalog/Catalog`, data);
    return response.data;
  } catch (error) {
    return thunkAPI.rejectWithValue(error);
  }
});

export const deleteCatalog = createAsyncThunk('catalog/deleteCatalog', async (catalogId, thunkAPI) => {
  try {
    const response = await axiosClient.delete(`/catalog/Catalog?id=${catalogId}`);
    return response.data;
  } catch (error) {
    return thunkAPI.rejectWithValue(error);
  }
});
