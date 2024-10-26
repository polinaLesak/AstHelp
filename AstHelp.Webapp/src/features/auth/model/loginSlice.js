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
    isAuthenticated: !!getCookie('jwtToken'),
    user: getCookie('user') ? JSON.parse(getCookie('user')) : null,
    token: getCookie('jwtToken') || null,
    loading: false,
    success: null,
    error: null,
  },
  reducers: {
    logout(state) {
      state.user = null;
      state.token = null;
      state.isAuthenticated = false
      document.cookie = `jwtToken=; max-age=0; path=/;`;
      document.cookie = `user=; max-age=0; path=/;`;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(login.pending, (state) => {
        state.loading = true
        state.success = null
        state.isAuthenticated = false
        state.user = null
        state.token = null
        state.error = null
      })
      .addCase(login.fulfilled, (state, action) => {
        state.loading = false;
        state.token = action.payload.jwtToken;
        state.isAuthenticated = true;
        state.success = "Авторизация прошла успешно";
        state.user = {
          id: action.payload.id,
          fullname: action.payload.fullname,
          username: action.payload.username,
          roles: action.payload.roles
        };
        document.cookie = `jwtToken=${state.token}; max-age=${60 * 30}; path=/; samesite=strict`;
        document.cookie = `user=${JSON.stringify(state.user)}; max-age=${60 * 30}; path=/; samesite=strict`;
      })
      .addCase(login.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload.response === undefined 
          ? `Непредвиденная ошибка сервера. ${action.payload.message}` 
          : action.payload.response.data;
      });
  },
});

function getCookie(name) {
  const value = `; ${document.cookie}`;
  const parts = value.split(`; ${name}=`);
  if (parts.length === 2) return parts.pop().split(';').shift();
}

export const { logout } = loginSlice.actions;
export default loginSlice.reducer;
