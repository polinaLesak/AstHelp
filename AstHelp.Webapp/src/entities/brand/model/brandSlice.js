import { createSlice } from '@reduxjs/toolkit';
import { createBrand, deleteBrand, fetchAllBrands, updateBrand } from '../api/brandApi';

const brandSlice = createSlice({
  name: 'brand',
  initialState: {
    brands: null,
    loading: false,
    success: null,
    error: null,
  },
  reducers: {
    clearBrandError(state) {
      state.error = null
    },
    clearBrandSuccess(state) {
      state.success = null
    }
  },
  extraReducers: (builder) => {
    handleAsyncActions(builder, fetchAllBrands, 'brands');
    handleAsyncActionsWithSuccessAlert(builder, createBrand, 'Бренд успешно добавлен');
    handleAsyncActionsWithSuccessAlert(builder, deleteBrand, 'Бренд успешно удалён');
    handleAsyncActionsWithSuccessAlert(builder, updateBrand, 'Бренд успешно изменён');
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
      state.error = action.payload === undefined 
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
      state.error = action.payload.response === undefined 
        ? `Непредвиденная ошибка сервера. ${action.payload.message}` 
        : action.payload.response.data;
    });
};

export const { clearBrandError, clearBrandSuccess } = brandSlice.actions;
export default brandSlice.reducer;
