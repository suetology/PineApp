import { configureStore } from '@reduxjs/toolkit';
import { decksApi } from '../api/decksApi';
import decksReducer from '../redux/decksSlice';
import answersReducer from '../redux/slices'
import { cardsApi } from '../api/cardsApi';
import { fileUploadApi } from '../api/fileUploadApi';

const mockCards = [
  {cardId: 0, front: "testF", back: "testB", deckId: 1},
  {cardId: 1, front: "testF2", back: "testB2", deckId: 1}
];

const mockDecks = [
  { id: 1, name: 'MockFirst', description: "a", isPersonal: false, creatorId: 0, cards: mockCards},
  { id: 2, name: 'MockSecond', description: "a", isPersonal: true, creatorId: 0, cards: []},
];

export const store = configureStore({
  reducer: {
    decks: decksReducer,
    answers: answersReducer,
    [decksApi.reducerPath]: decksApi.reducer,
    [cardsApi.reducerPath]: cardsApi.reducer,
    [fileUploadApi.reducerPath]: fileUploadApi.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware()
      .concat(decksApi.middleware)
      .concat(cardsApi.middleware)
      .concat(fileUploadApi.middleware),
});

decksApi.injectEndpoints({
  overrideExisting: true,
  endpoints: (builder) => ({
    getAllDecksById: builder.query({
      queryFn: (creatorId) => ({ data: mockDecks }),
    }),
    getDeckById: builder.query({
      queryFn: (deckId) => ({ data: mockDecks[deckId-1]})
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

cardsApi.injectEndpoints({
  overrideExisting: true,
  endpoints: (builder) => ({
    addCard : builder.mutation({
      queryFn : ({Front, Back, DeckId}) => ({ data: {cardId: 9, front: Front, back: Back, deckId: DeckId} })
    }),
  }),
});

fileUploadApi.injectEndpoints({
  overrideExisting: true,
  endpoints: (builder) => ({
    uploadFile : builder.mutation({
      queryFn : (file) => ({ data: file })
    }),
  }),
});
