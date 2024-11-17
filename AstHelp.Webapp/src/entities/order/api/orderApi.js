import axiosClient from "../../../app/axiosClient";
import { createAsyncThunk } from "@reduxjs/toolkit";
import { clearCart } from "../../cart/model/cartSlice";
import { changeOrderStatus } from "../model/orderSlice";

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
  async (data, { rejectWithValue, dispatch }) => {
    try {
      await axiosClient.post(`/orders/Orders`, data)
        .then(function () {
          dispatch(clearCart());
        });
    } catch (error) {
      return rejectWithValue(error);
    }
  }
);

export const updateOrderStatus = createAsyncThunk(
  "orders/updateOrderStatus",
  async ({ orderId, status }, { rejectWithValue, dispatch }) => {
    try {
      await axiosClient.put(`/orders/Orders/UpdateStatus`, {}, {
        params: { orderId, status },
      }).then(function() {
        dispatch(changeOrderStatus(status))
      });
    } catch (error) {
      return rejectWithValue(error);
    }
  }
);

export const generateOrderAct = createAsyncThunk(
  "orders/generateOrderAct",
  async (orderId, { rejectWithValue, dispatch }) => {
    try {
      return await axiosClient.get(`/orders/Orders/GenerateAct?orderId=${orderId}`, {
        responseType: "blob"
      })
    } catch (error) {
      return rejectWithValue(error);
    }
  }
);
