import axiosClient from "../../../app/axiosClient";
import { createAsyncThunk } from "@reduxjs/toolkit";

export const fetchProduct = createAsyncThunk(
  "product/fetchProduct",
  async (catalogId, thunkAPI) => {
    try {
      const response = await axiosClient.get(`/catalog/Product/${catalogId}`);
      return response.data;
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  }
);

export const fetchAllProducts = createAsyncThunk(
  "product/fetchAllProducts",
  async ({sortingField, sortingDirection}, thunkAPI) => {
    if(!sortingField) sortingField = "Id"
    if(!sortingDirection) sortingDirection = "Asc"
    try {
      const response = await axiosClient.get(
        `/catalog/Product?sortingField=${sortingField}&sortingDirection=${sortingDirection}`
      );
      return response.data;
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  }
);

export const fetchProductById = createAsyncThunk(
  "product/fetchProductById",
  async (productId, thunkAPI) => {
    try {
      const response = await axiosClient.get(`/catalog/Product/${productId}`);
      return response.data;
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  }
);

export const createProduct = createAsyncThunk(
  "product/createProduct",
  async (data, thunkAPI) => {
    try {
      const response = await axiosClient.post("/catalog/Product", data, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });
      return response.data;
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  }
);

export const updateProduct = createAsyncThunk(
  "product/updateProduct",
  async (data, thunkAPI) => {
    try {
      const response = await axiosClient.put(`/catalog/Product`, data, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });
      return response.data;
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  }
);

export const deleteProduct = createAsyncThunk(
  "product/deleteProduct",
  async (catalogId, thunkAPI) => {
    try {
      const response = await axiosClient.delete(
        `/catalog/Product?id=${catalogId}`
      );
      return response.data;
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  }
);
