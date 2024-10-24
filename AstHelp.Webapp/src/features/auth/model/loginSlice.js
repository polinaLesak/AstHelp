import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import axios from 'axios';

export const login = createAsyncThunk('auth/login', async (data, thunkAPI) => {
  try {
    const response = await axios.post(`${process.env.REACT_APP_API_URL}identity/Auth/Login`, data);
    return response.data;
  } catch (error) {
    return thunkAPI.rejectWithValue(error);
  }
});

const loginSlice = createSlice({
  name: 'login',
  initialState: {
    user: null,
    token: null,
    loading: false,
    success: null,
    error: null,
  },
  reducers: {
    logout(state) {
      state.user = null;
      state.token = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(login.pending, (state) => {
        state.loading = true;
        state.success = null;
        state.error = null;
      })
      .addCase(login.fulfilled, (state, action) => {
        state.loading = false;
        state.token = action.payload.jwtToken;
        state.success = "Авторизация прошла успешно";
        state.user = {
          id: action.payload.id,
          fullname: action.payload.fullname,
          username: action.payload.username,
        };
        document.cookie = `jwtToken=${state.token}; max-age=${60 * 30}; path=/; samesite=strict`;
        document.cookie = `fullname=${state.fullname}; max-age=${60 * 30}; path=/; samesite=strict`;
        document.cookie = `fullname=${state.username}; max-age=${60 * 30}; path=/; samesite=strict`;
      })
      .addCase(login.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload.response === undefined 
          ? `Непредвиденная ошибка сервера. ${action.payload.message}` 
          : action.payload.response.data;
      });
  },
});

export const { logout } = loginSlice.actions;
export default loginSlice.reducer;
