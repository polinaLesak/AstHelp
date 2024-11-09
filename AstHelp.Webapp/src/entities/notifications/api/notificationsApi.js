import axiosClient from "../../../app/axiosClient";
import { createAsyncThunk } from "@reduxjs/toolkit";
import {
  clearAlerts,
  markAsReadById,
  markAllAsRead,
  deleteById,
  deleteAll,
} from "../model/notificationsSlice";

export const fetchAllNotificationsByUserId = createAsyncThunk(
  "notifications/fetchAllNotificationsByUserId",
  async (userId, { rejectWithValue, dispatch }) => {
    try {
      dispatch(clearAlerts());
      const response = await axiosClient.get(`/notification/Notification?userId=${userId}`);
      if (!response.data) return [];
      return response.data;
    } catch (error) {
      return rejectWithValue(error);
    }
  }
);

export const markNotificationAsReadById = createAsyncThunk(
  "notifications/markNotificationAsReadById",
  async (notificationId, { rejectWithValue, dispatch }) => {
    try {
      dispatch(clearAlerts());
      await axiosClient
        .put(`/notification/Notification/MarkAsRead`, {}, {
          params: { id: notificationId },
        })
        .then(function () {
          dispatch(markAsReadById(notificationId));
        });
    } catch (error) {
      return rejectWithValue(error);
    }
  }
);

export const markAllNotificationsAsReadByUserId = createAsyncThunk(
  "notifications/markAllNotificationsAsRead",
  async (userId, { rejectWithValue, dispatch }) => {
    try {
      dispatch(clearAlerts());
      await axiosClient
        .post(`/notification/Notification/All/MarkAsRead`, {}, {
          params: { userId },
        })
        .then(function () {
          dispatch(markAllAsRead());
        });
    } catch (error) {
      return rejectWithValue(error);
    }
  }
);

export const deleteNotificationById = createAsyncThunk(
  "notifications/deleteNotificationById",
  async (notificationId, { rejectWithValue, dispatch }) => {
    try {
      dispatch(clearAlerts());
      await axiosClient
        .delete(`/notification/Notification`, {
          params: { id: notificationId },
        })
        .then(function () {
          dispatch(deleteById(notificationId));
        });
    } catch (error) {
      return rejectWithValue(error);
    }
  }
);

export const deleteAllNotificationsByUserId = createAsyncThunk(
  "notifications/deleteAllNotifications",
  async (userId, { rejectWithValue, dispatch }) => {
    try {
      dispatch(clearAlerts());
      await axiosClient
        .delete(`/notification/Notification/All`, {
          params: { userId },
        })
        .then(function () {
          dispatch(deleteAll());
        });
    } catch (error) {
      return rejectWithValue(error);
    }
  }
);
