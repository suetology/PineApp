import { configureStore } from "@reduxjs/toolkit";
import { decksApi } from "../api/decksApi";
import { cardsApi } from "../api/cardsApi";
import { fileUploadApi } from "../api/fileUploadApi";
import answersReducer from "./slices"
import deckSlice from "./decksSlice";

export const store = configureStore({
    reducer: {
        decks: deckSlice,
        answers: answersReducer,
        [decksApi.reducerPath]: decksApi.reducer,
        [cardsApi.reducerPath]: cardsApi.reducer,
        [fileUploadApi.reducerPath]: fileUploadApi.reducer
    },
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware()
            .concat(decksApi.middleware)
            .concat(fileUploadApi.middleware)
            .concat(cardsApi.middleware)
});