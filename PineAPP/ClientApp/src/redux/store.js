import { configureStore } from "@reduxjs/toolkit";
import { decksApi } from "../api/decksApi";

export const store = configureStore({
    reducer: {
        [decksApi.reducerPath]: decksApi.reducer
    },
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware()
            .concat(decksApi.middleware)
});