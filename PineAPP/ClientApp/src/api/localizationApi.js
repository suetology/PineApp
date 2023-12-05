import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";

const url = "http://localhost:5067/";

const localizationApi = createApi({
    reducerPath: "localizationApi",
    baseQuery: fetchBaseQuery({ baseUrl: url }),
    endpoints: (builder) => ({
        getStrings: builder.query({
            query: (culture) => ({
                url: `Localization/${culture}`,
                method: "GET",
                params: {}
            })
        })
    })
});