import { createSlice } from '@reduxjs/toolkit';

const userLocationSlice = createSlice({
  name: 'userLocation',
  initialState: null,
  reducers: {
    setUserLocation: (state, action) => action.payload, // Save the user's location
  },
});

export const { setUserLocation } = userLocationSlice.actions;
export default userLocationSlice.reducer;
