import axiosClient from "../../../app/axiosClient";
import { createAsyncThunk } from "@reduxjs/toolkit";

export const fetchAttribute = createAsyncThunk(
  "catalog/fetchAttribute",
  async (attributeId, thunkAPI) => {
    try {
      const response = await axiosClient.get(
        `/catalog/Attribute/${attributeId}`
      );
      return response.data;
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  }
);

export const fetchAllAttributes = createAsyncThunk(
  "catalog/fetchAllAttributes",
  async (thunkAPI) => {
    try {
      const response = await axiosClient.get(`/catalog/Attribute`);
      return response.data;
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  }
);

export const fetchAttributesByCatalogId = createAsyncThunk(
  "catalog/fetchAttributesByCatalogId",
  async (catalogId, thunkAPI) => {
    try {
      const response = await axiosClient.get(
        `/catalog/Attribute/ByCatalogId?id=${catalogId}`
      );
      return response.data;
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  }
);

export const fetchAllAttributeTypes = createAsyncThunk(
  "catalog/fetchAllAttributeTypes",
  async (thunkAPI) => {
    try {
      const response = await axiosClient.get(
        `/catalog/Attribute/AttributeTypes`
      );
      return response.data;
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  }
);

export const createAttribute = createAsyncThunk(
  "brand/createAttribute",
  async (data, thunkAPI) => {
    try {
      const response = await axiosClient.post(`/catalog/Attribute`, data);
      return response.data;
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  }
);

export const updateAttribute = createAsyncThunk(
  "catalog/updateAttribute",
  async (data, thunkAPI) => {
    try {
      const response = await axiosClient.put(`/catalog/Attribute`, data);
      return response.data;
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  }
);

export const deleteAttribute = createAsyncThunk(
  "catalog/deleteAttribute",
  async (attributeId, thunkAPI) => {
    try {
      const response = await axiosClient.delete(
        `/catalog/Attribute?id=${attributeId}`
      );
      return response.data;
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  }
);
