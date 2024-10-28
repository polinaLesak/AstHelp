import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import axios from 'axios';
import config from "../../../config";

export const registration = createAsyncThunk('auth/registration', async (data, thunkAPI) => {
  try {
    const response = await axios.post(`${config.apiUrl}identity/Auth/Register`, data);
    return response.data;
  } catch (error) {
    return thunkAPI.rejectWithValue(error);
  }
});

const registrationSlice = createSlice({
  name: 'registration',
  initialState: {
    loading: false,
    success: null,
    error: null,
  },
  extraReducers: (builder) => {
    builder
      .addCase(registration.pending, (state) => {
        state.loading = true;
        state.success = null;
        state.error = null;
      })
      .addCase(registration.fulfilled, (state, action) => {
        state.loading = false;
        state.success = "Пользователь успешно зарегестрирован";
      })
      .addCase(registration.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload.response === undefined 
          ? `Непредвиденная ошибка сервера. ${action.payload.message}` 
          : action.payload.response.data;
      });
  },
});

export default registrationSlice.reducer;
