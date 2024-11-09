import { createSlice } from "@reduxjs/toolkit";
import {
  deleteAllNotificationsByUserId,
  deleteNotificationById,
  fetchAllNotificationsByUserId,
  markAllNotificationsAsReadByUserId,
  markNotificationAsReadById,
} from "../api/notificationsApi";

const notificationsSlice = createSlice({
  name: "notifications",
  initialState: {
    notifications: null,
    loading: false,
    success: null,
    error: null,
  },
  reducers: {
    clearAlerts(state) {
      state.success = null;
      state.error = null;
    },
    markAsReadById: (state, action) => {
      const notification = state.notifications.find((n) => n.id === action.payload);
      if (notification) notification.isRead = true;
    },
    markAllAsRead: (state) => {
      state.notifications.forEach((notification) => (notification.isRead = true));
    },
    deleteById: (state, action) => {
      state.notifications = state.notifications.filter((n) => n.id !== action.payload);
    },
    deleteAll: (state) => {
      state.notifications = [];
    },
  },
  extraReducers: (builder) => {
    handleAsyncActions(builder, fetchAllNotificationsByUserId, "notifications");
    handleAsyncActionsWithSuccessAlert(
      builder,
      markAllNotificationsAsReadByUserId,
      "Все уведомления успешно прочитаны"
    );
    handleAsyncActionsWithSuccessAlert(
      builder,
      markNotificationAsReadById,
      "Уведомление успешно помечены прочитанным"
    );
    handleAsyncActionsWithSuccessAlert(
      builder,
      deleteAllNotificationsByUserId,
      "Все уведомления успешно удалены"
    );
    handleAsyncActionsWithSuccessAlert(
      builder,
      deleteNotificationById,
      "Уведомление успешно удалено"
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

export const {
  clearAlerts,
  markAsReadById,
  markAllAsRead,
  deleteById,
  deleteAll,
} = notificationsSlice.actions;
export default notificationsSlice.reducer;
