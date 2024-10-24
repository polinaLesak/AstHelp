import { createSlice } from '@reduxjs/toolkit';
import { fetchUser } from '../api/userApi';

const userSlice = createSlice({
  name: 'user',
  initialState: {
    data: null,
    loading: false,
    error: null,
  },
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(fetchUser.pending, (state) => {
      console.log('userSlice fetchUser.pending')
      state.loading = true;
    });
    builder.addCase(fetchUser.fulfilled, (state, action) => {
      console.log('userSlice fetchUser.fulfilled')
      state.loading = false;
      state.data = action.payload;
      console.log(state.data, 'state.data')
    });
    builder.addCase(fetchUser.rejected, (state, action) => {
      console.log('userSlice fetchUser.rejected')
      state.loading = false;
      state.error = action.error.message;
    });
  },
});

export default userSlice.reducer;
