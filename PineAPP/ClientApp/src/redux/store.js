import { configureStore } from "@reduxjs/toolkit";
import { decksApi } from "../api/decksApi";
import { fileUploadApi } from "../api/fileUploadApi";
import { usersApi } from "../api/usersApi";

export const store = configureStore({
    reducer: {
        [decksApi.reducerPath]: decksApi.reducer,
        [fileUploadApi.reducerPath]: fileUploadApi.reducer,
        [usersApi.reducerPath]: usersApi.reducer
    },
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware()
            .concat(decksApi.middleware)
            .concat(fileUploadApi.middleware)
            .concat(usersApi.middleware)
});