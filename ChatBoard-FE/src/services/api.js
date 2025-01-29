import AsyncStorage from '@react-native-async-storage/async-storage';
import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

const baseQuery = fetchBaseQuery({
  baseUrl: process.env.API_URL,
  // timeout:2000,
  _prepareHeaders: (headers, { getState }) => {
    const token = AsyncStorage.getItem('Token');
    // console.log("token ::: ",token)
    if (token) {
      headers.set('authorization', `Bearer ${token}`);
    }
    return headers;
  },
  get prepareHeaders() {
    return this._prepareHeaders;
  },
  set prepareHeaders(value) {
    this._prepareHeaders = value;
  },
});

const baseQueryWithInterceptor = async (args, api, extraOptions) => {
  let result = await baseQuery(args, api, extraOptions);
  // Get the token from the Redux store
  if (result.error && (result.error.status === 401 || result.error.status === 400)) {
    // Handle unauthorized access here
    // return
  }
  // Handle unauthorized access here

  // If the token exists, add it to the request headers
  // if (token) {
  //   result = {
  //     ...result,
  //     headers: {
  //       ...result.headers,
  //       Authorization: `Bearer ${token}`,
  //     },
  //   };
  // }

  return result;
};
export const api = createApi({
  keepUnusedDataFor: 60 * 1000,
  baseQuery: baseQueryWithInterceptor,
  tagTypes: [
    'TAGS',
  ],
  endpoints: () => ({}),
});

