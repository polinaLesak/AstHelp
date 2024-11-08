import { createSlice } from "@reduxjs/toolkit";
import {
  createOrder
} from "../api/orderApi";

const orderSlice = createSlice({
  name: "order",
  initialState: {
    order: null,
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
  },
  extraReducers: (builder) => {
    // handleAsyncActions(builder, fetchCartByUserId, "order");
    // handleAsyncActions(builder, fetchCartProductsCountByUserId, "countProducts");
    // handleAsyncActionsWithSuccessAlert(
    //   builder,
    //   addProductToUserCart,
    //   "Оборудование успешно добавлено к заказу"
    // );
    handleAsyncActionsWithSuccessAlert(
      builder,
      createOrder,
      "Заявка успешно оформлена"
    );
  },
});

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

export const { clearOrderError, clearOrderSuccess } = orderSlice.actions;
export default orderSlice.reducer;
