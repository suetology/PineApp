import { configureStore } from '@reduxjs/toolkit';
import { decksApi } from '../api/decksApi';
import decksReducer from '../redux/decksSlice';


const mockDecks = [
  { id: 1, name: 'MockFirst', description: "a", isPersonal: false, creatorId: 0, cards: []},
  { id: 2, name: 'MockSecond', description: "a", isPersonal: true, creatorId: 0, cards: []},
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
      queryFn: (creatorId) => ({ data: mockDecks }),
    }),
    getDeckById: builder.query({
      queryFn: (deckId) => ({ data: mockDecks.filter(deck => deck.id === deckId)})
    }),
    addDeck : builder.mutation({
      queryFn : ({Name, IsPersonal, CreatorId, Description}) => ({
          data: {
              id: 0,
              Name,
              IsPersonal,
              CreatorId,
              Description,
          }
      }),
    }),
  }),
});