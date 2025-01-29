import { createSlice } from '@reduxjs/toolkit';

const tokenSlice = createSlice({
  name: 'token',
  initialState: null,
  reducers: {
    setToken: (state, action) => {
      return action.payload;
    },
    getToken: (state, {payload}) => {
      state = payload;
    },
  },
});

export const { setToken, getToken } = tokenSlice.actions;
export default tokenSlice.reducer;
