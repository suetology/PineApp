import { configureStore } from '@reduxjs/toolkit';
import { decksApi } from '../api/decksApi';
import decksReducer from '../redux/decksSlice';


const mockDecks = [
  { id: 1, name: 'Mock Deck 1', isPersonal: false},
  { id: 2, name: 'Mock Deck 2', isPersonal: true},
];

export const store = configureStore({
  reducer: {
    decks: decksReducer,
    [decksApi.reducerPath]: decksApi.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(decksApi.middleware),
});

decksApi.injectEndpoints({
  overrideExisting: true,
  endpoints: (builder) => ({
    getAllDecksById: builder.query({
      queryFn: (creatorId) => ({ data: mockDecks.filter(deck => deck.creatorId === creatorId) }),
    }),
    // ...other endpoints
  }),
});