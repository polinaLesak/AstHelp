import { createSlice } from '@reduxjs/toolkit';
import { createAttribute, deleteAttribute, fetchAllAttributes, fetchAllAttributeTypes, updateAttribute } from '../api/attributeApi';

const attributeSlice = createSlice({
  name: 'attribute',
  initialState: {
    attributes: null,
    attributeTypes: null,
    loading: false,
    success: null,
    error: null,
  },
  reducers: {
    clearAttributeError(state) {
      state.error = null
    },
    clearAttributeSuccess(state) {
      state.success = null
    }
  },
  extraReducers: (builder) => {
    handleAsyncActions(builder, fetchAllAttributes, 'attributes');
    handleAsyncActions(builder, fetchAllAttributeTypes, 'attributeTypes');
    handleAsyncActionsWithSuccessAlert(builder, createAttribute, 'Утрибут успешно добавлен');
    handleAsyncActionsWithSuccessAlert(builder, deleteAttribute, 'Утрибут успешно удалён');
    handleAsyncActionsWithSuccessAlert(builder, updateAttribute, 'Атрибут успешно изменён');
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
      state.success = null;
      state.error = null;
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

export const { clearAttributeError, clearAttributeSuccess } = attributeSlice.actions;
export default attributeSlice.reducer;
