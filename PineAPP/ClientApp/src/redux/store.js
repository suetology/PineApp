import { configureStore } from "@reduxjs/toolkit";
import { decksApi } from "../api/decksApi";
import { fileUploadApi } from "../api/fileUploadApi";

export const store = configureStore({
    reducer: {
        [decksApi.reducerPath]: decksApi.reducer,
        [fileUploadApi.reducerPath]: fileUploadApi.reducer
    },
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware()
            .concat(decksApi.middleware)
            .concat(fileUploadApi.middleware)
});