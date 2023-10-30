import { configureStore } from "@reduxjs/toolkit";
import { decksApi } from "../api/decksApi";
import { fileUploadApi } from "../api/fileUploadApi";
import { usersApi } from "../api/usersApi";
import answersReducer from "./slices"
import deckSlice from "./decksSlice";
import {cardsApi} from "../api/cardsApi";


export const store = configureStore({
    reducer: {
        decks: deckSlice,
        answers: answersReducer,
        [decksApi.reducerPath]: decksApi.reducer,
        [cardsApi.reducerPath]: cardsApi.reducer,
        [fileUploadApi.reducerPath]: fileUploadApi.reducer,
        [usersApi.reducerPath]: usersApi.reducer
    },
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware()
            .concat(decksApi.middleware)
            .concat(cardsApi.middleware)
            .concat(fileUploadApi.middleware)
            .concat(usersApi.middleware)
});