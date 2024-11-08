import axiosClient from "../../../app/axiosClient";
import { createAsyncThunk } from "@reduxjs/toolkit";

export const fetchAllOrders = createAsyncThunk(
  "orders/fetchAllOrders",
  async (thunkAPI) => {
    try {
      const response = await axiosClient.get(`/orders/Orders`);
      return response.data;
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  }
);

export const fetchOrderById = createAsyncThunk(
  "orders/fetchOrderById",
  async (orderId, thunkAPI) => {
    try {
      const response = await axiosClient.get(`/orders/Orders/${orderId}`);
      return response.data;
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  }
);

export const fetchOrdersByUserId = createAsyncThunk(
  "orders/fetchOrdersByUserId",
  async (userId, thunkAPI) => {
    try {
      const response = await axiosClient.get(`/orders/Orders/Customer/${userId}`);
      return response.data;
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  }
);

export const fetchOrdersByManagerId = createAsyncThunk(
  "orders/fetchOrdersByManagerId",
  async (managerId, thunkAPI) => {
    try {
      const response = await axiosClient.get(`/orders/Orders/Manager/${managerId}`);
      return response.data;
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  }
);

export const createOrder = createAsyncThunk(
  "orders/createOrder",
  async (data, thunkAPI) => {
    try {
      const response = await axiosClient.post(`/orders/Orders`, data);
      return response.data;
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  }
);

export const updateOrderStatus = createAsyncThunk(
  "orders/updateOrderStatus",
  async ({ orderId, status }, thunkAPI) => {
    try {
      const response = await axiosClient.put(`/orders/Orders`, {
        params: { orderId, status },
      });
      return response.data;
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  }
);
