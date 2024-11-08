import { createSlice } from "@reduxjs/toolkit";
import {
  addProductToUserCart,
  deleteProductFromUserCart,
  fetchCartByUserId,
  fetchCartProductsCountByUserId
} from "../api/cartApi";

const cartSlice = createSlice({
  name: "cart",
  initialState: {
    cart: null,
    countProducts: null,
    loading: false,
    success: null,
    error: null,
  },
  reducers: {
    clearCartError(state) {
      state.error = null;
    },
    clearCartSuccess(state) {
      state.success = null;
    },
  },
  extraReducers: (builder) => {
    handleAsyncActions(builder, fetchCartByUserId, "cart");
    handleAsyncActions(builder, fetchCartProductsCountByUserId, "countProducts");
    handleAsyncActionsWithSuccessAlert(
      builder,
      addProductToUserCart,
      "Оборудование успешно добавлено к заказу"
    );
    handleAsyncActionsWithSuccessAlert(
      builder,
      deleteProductFromUserCart,
      "Оборудование успешно удалено из заказа"
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

export const { clearCartError, clearCartSuccess } = cartSlice.actions;
export default cartSlice.reducer;
