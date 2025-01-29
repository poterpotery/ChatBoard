import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

const baseQuery = fetchBaseQuery({
  baseUrl: process.env.API_URL,
  // timeout:2000,
  _prepareHeaders: (headers, { getState }) => {
    const token = getState().token;
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


  return result;
};
export const apiTags = createApi({
  keepUnusedDataFor: 60 * 1000,
  reducerPath: 'apiTags',
  baseQuery: baseQueryWithInterceptor,
  tagTypes: [
    'POSTS',
    'PROFILE',
    // 'COMMUNITY',
    'TAGS',
    'CHAT'
    // 'APPOINTMENTS',
    // 'COMMENT',
    // 'NOTIFICATIONS',
    // 'MEDIA',
    // 'FLASHES',
    // 'GlobalSearch',
    // 'EXERCISES'
  ],
  endpoints: () => ({}),
});

