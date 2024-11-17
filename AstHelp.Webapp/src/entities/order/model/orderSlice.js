import { createSlice } from "@reduxjs/toolkit";
import {
  createOrder,
  fetchAllOrders,
  fetchOrderById,
  fetchOrdersByManagerId,
  fetchOrdersByUserId,
  generateOrderAct,
  updateOrderStatus
} from "../api/orderApi";

const orderSlice = createSlice({
  name: "order",
  initialState: {
    act: null,
    order: null,
    orders: null,
    loading: false,
    success: null,
    error: null,
  },
  reducers: {
    clearOrderError(state) {
      state.error = null;
    },
    clearOrderSuccess(state) {
      state.success = null;
    },
    changeOrderStatus(state, status) {
      state.order.status = status.payload;
    },
  },
  extraReducers: (builder) => {
    handleAsync(builder, generateOrderAct);
    handleAsyncActions(builder, fetchOrderById, "order");
    handleAsyncActions(builder, fetchOrdersByUserId, "orders");
    handleAsyncActions(builder, fetchOrdersByManagerId, "orders");
    handleAsyncActions(builder, fetchAllOrders, "orders");
    handleAsyncActionsWithSuccessAlert(
      builder,
      createOrder,
      "Заявка успешно оформлена"
    );
    handleAsyncActionsWithSuccessAlert(
      builder,
      updateOrderStatus,
      "Статус заявки успешно изменён"
    );
  },
});

const handleAsync = (builder, thunk) => {
  builder
    .addCase(thunk.pending, (state) => {
      state.loading = true;
      state.error = null;
      state.success = null;
    })
    .addCase(thunk.fulfilled, (state) => {
      state.loading = false;
    })
    .addCase(thunk.rejected, (state, action) => {
      state.loading = false;
      state.error =
        action.payload === undefined
          ? `Непредвиденная ошибка сервера.`
          : action.payload.response === undefined
          ? `Непредвиденная ошибка сервера. ${action.payload.message}`
          : action.payload.response.data;
    });
};

const handleAsyncActions = (builder, thunk, field) => {
  builder
    .addCase(thunk.pending, (state) => {
      state.loading = true;
      state.error = null;
      state.success = null;
    })
    .addCase(thunk.fulfilled, (state, action) => {
      state.loading = false;
      state[field] = action.payload;
    })
    .addCase(thunk.rejected, (state, action) => {
      state.loading = false;
      state.error =
        action.payload === undefined
          ? `Непредвиденная ошибка сервера.`
          : action.payload.response === undefined
          ? `Непредвиденная ошибка сервера. ${action.payload.message}`
          : action.payload.response.data;
    });
};

const handleAsyncActionsWithSuccessAlert = (builder, thunk, successText) => {
  builder
    .addCase(thunk.pending, (state) => {
      state.loading = true;
      state.error = null;
      state.success = null;
    })
    .addCase(thunk.fulfilled, (state, action) => {
      state.loading = false;
      state.success = successText;
    })
    .addCase(thunk.rejected, (state, action) => {
      state.loading = false;
      state.error =
        action.payload.response === undefined
          ? `Непредвиденная ошибка сервера. ${action.payload.message}`
          : action.payload.response.data;
    });
};

export const { clearOrderError, clearOrderSuccess, changeOrderStatus } = orderSlice.actions;
export default orderSlice.reducer;
