import { configureStore, combineReducers } from '@reduxjs/toolkit';
import { setupListeners } from '@reduxjs/toolkit/query';
import {
  persistReducer,
  persistStore,
} from 'redux-persist';
import { MMKV } from 'react-native-mmkv';
import { api } from '../services/api';
import theme from './theme';
import token from './authentication';
import userLocation from './userLocation';
import { apiTags } from '../services/apiTags';
const reducers = combineReducers({
  [api.reducerPath]: api.reducer,
  [apiTags.reducerPath]: apiTags.reducer,
  theme,
  token,
  userLocation
});
const storage = new MMKV();

export const reduxStorage = {
  setItem: (key, value) => {
    storage.set(key, value);
    return Promise.resolve(true);
  },
  getItem: key => {
    const value = storage.getString(key);
    return Promise.resolve(value);
  },
  removeItem: key => {
    storage.delete(key);
    return Promise.resolve();
  },
};
const persistConfig = {
  key: 'root',
  storage: reduxStorage,
  whitelist: ['theme', 'token',], //apiTags
};
const persistedReducer = persistReducer(persistConfig, reducers);
const store = configureStore({
  reducer: persistedReducer,
  middleware: getDefaultMiddleware => {
    const middlewares = getDefaultMiddleware({
      serializableCheck: false,
      immutableCheck: false,
    }).concat(api.middleware).concat(apiTags.middleware);
    if (__DEV__ || !process.env.JEST_WORKER_ID) {
      const createDebugger = require('redux-flipper').default;
      middlewares.push(createDebugger());
    }
    return middlewares;
  },
});
const persistor = persistStore(store);
setupListeners(store.dispatch);
export { store, persistor };
