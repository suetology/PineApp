import { configureStore } from "@reduxjs/toolkit";
import { decksApi } from "../api/decksApi";
import { fileUploadApi } from "../api/fileUploadApi";
import answersReducer from "./slices"

export const store = configureStore({
    reducer: {
        answers: answersReducer,
        [decksApi.reducerPath]: decksApi.reducer,
        [fileUploadApi.reducerPath]: fileUploadApi.reducer
    },
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware()
            .concat(decksApi.middleware)
            .concat(fileUploadApi.middleware)
});