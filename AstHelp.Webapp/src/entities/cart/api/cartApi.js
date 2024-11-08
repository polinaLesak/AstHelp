import axiosClient from "../../../app/axiosClient";
import { createAsyncThunk } from "@reduxjs/toolkit";

export const fetchCartByUserId = createAsyncThunk(
  "cart/fetchCartByUserId",
  async (userId, thunkAPI) => {
    try {
      const response = await axiosClient.get(`/cart/Cart?userId=${userId}`);
      return response.data;
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  }
);

export const fetchCartProductsCountByUserId = createAsyncThunk(
  "cart/fetchCartProductsCountByUserId",
  async (userId, thunkAPI) => {
    try {
      const response = await axiosClient.get(`/cart/Cart/ProductsCount?userId=${userId}`);
      return response.data;
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  }
);

export const addProductToUserCart = createAsyncThunk(
  "cart/addProductToUserCart",
  async (data, thunkAPI) => {
    try {
      const response = await axiosClient.post(`/cart/Cart/AddProduct`, data);
      return response.data;
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  }
);

export const deleteProductFromUserCart = createAsyncThunk(
  "cart/deleteProductFromUserCart",
  async ({ userId, productId }, thunkAPI) => {
    try {
      const response = await axiosClient.delete(`/cart/Cart/${userId}/DeleteProduct`, {
        params: { productId },
    });
      return response.data;
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  }
);
