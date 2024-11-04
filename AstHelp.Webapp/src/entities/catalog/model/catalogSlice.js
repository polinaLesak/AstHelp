import { createSlice } from '@reduxjs/toolkit';
import { createCatalog, deleteCatalog, fetchAllCatalogs, updateCatalog } from '../api/catalogApi';

const catalogSlice = createSlice({
  name: 'brand',
  initialState: {
    catalogs: null,
    loading: false,
    success: null,
    error: null,
  },
  reducers: {
    clearCatalogError(state) {
      state.error = null
    },
    clearCatalogSuccess(state) {
      state.success = null
    }
  },
  extraReducers: (builder) => {
    handleAsyncActions(builder, fetchAllCatalogs, 'catalogs');
    handleAsyncActionsWithSuccessAlert(builder, createCatalog, 'Каталог успешно добавлен');
    handleAsyncActionsWithSuccessAlert(builder, deleteCatalog, 'Каталог успешно удалён');
    handleAsyncActionsWithSuccessAlert(builder, updateCatalog, 'Каталог успешно изменён');
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
      console.log(action, 'actionactionactionactionaction')
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

export const { clearCatalogError, clearCatalogSuccess } = catalogSlice.actions;
export default catalogSlice.reducer;
