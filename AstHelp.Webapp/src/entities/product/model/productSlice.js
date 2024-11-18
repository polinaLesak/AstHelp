import { createSlice } from "@reduxjs/toolkit";
import {
  createProduct,
  deleteProduct,
  fetchAllProducts,
  updateProduct,
} from "../api/productApi";

const productSlice = createSlice({
  name: "product",
  initialState: {
    products: null,
    sortOptions: [
      { sortingField: "createdAt", sortingDirection: "Asc", label: "По дате создания (возр.)" },
      { sortingField: "createdAt", sortingDirection: "Desc", label: "По дате создания (убыв.)" },
      { sortingField: "quantity", sortingDirection: "Asc", label: "По количеству (возр.)" },
      { sortingField: "quantity", sortingDirection: "Desc", label: "По количеству (убыв.)" },
    ],
    sortState: { sortingField: "createdAt", sortingDirection: "Asc", label: "По дате создания (возр.)" },
    loading: false,
    success: null,
    error: null,
  },
  reducers: {
    clearProductError(state) {
      state.error = null;
    },
    clearProductSuccess(state) {
      state.success = null;
    },
    setSortState(state, { payload }) {
      state.sortState = payload;
    },
  },
  extraReducers: (builder) => {
    handleAsyncActions(builder, fetchAllProducts, "products");
    handleAsyncActionsWithSuccessAlert(
      builder,
      createProduct,
      "Оборудование успешно добавлено"
    );
    handleAsyncActionsWithSuccessAlert(
      builder,
      deleteProduct,
      "Оборудование успешно удалено"
    );
    handleAsyncActionsWithSuccessAlert(
      builder,
      updateProduct,
      "Оборудование успешно изменено"
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

export const { clearProductError, clearProductSuccess, setSortState } = productSlice.actions;
export default productSlice.reducer;
