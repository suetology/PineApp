import { configureStore } from '@reduxjs/toolkit';
import { decksApi } from '../api/decksApi';
import decksReducer from '../redux/decksSlice';
// import other reducers as necessary

export const store = configureStore({
  reducer: {
    decks: decksReducer,
    [decksApi.reducerPath]: decksApi.reducer,
    // add other reducers here
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(decksApi.middleware),
});
