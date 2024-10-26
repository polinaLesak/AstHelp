import { createSlice } from '@reduxjs/toolkit';
import { deleteUser, fetchAllUsers, updateUserProfile, updateUserStatus } from '../api/userApi';

const userSlice = createSlice({
  name: 'user',
  initialState: {
    users: null,
    loading: false,
    success: null,
    error: null,
  },
  extraReducers: (builder) => {
    handleAsyncActions(builder, fetchAllUsers, 'users');
    handleAsyncActionsWithSuccessAlert(builder, updateUserStatus, 'Активация аккаунта успешно изменена');
    handleAsyncActionsWithSuccessAlert(builder, deleteUser, 'Пользователь успешно удалён');
    handleAsyncActionsWithSuccessAlert(builder, updateUserProfile, 'Информация о пользователе успешно изменена');
  },
});

const handleAsyncActions = (builder, thunk, field) => {
  builder
    .addCase(thunk.pending, (state) => {
      state.loading = true;
      state.error = null;
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

export default userSlice.reducer;
