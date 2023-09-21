import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

const url = "https://localhost:7074/";

const decksApi = createApi({
    reducerPath: "decksApi",
    baseQuery: fetchBaseQuery({ baseUrl: url }),
    endpoints: (builder) => ({
        getAllDecks: builder.query({
            query: () => ({
                url: "api/Decks",
                method: "GET",
                params: {}    
            })
        }),
        getCommunityDecks: builder.query({
            query: () => ({
                url: "api/Decks/Community",
                method: "GET",
                params: {}
            })
        }),
        getPersonalDecks: builder.query({
            query: (creatorId) => ({
                url: `api/Decks/Personal/${creatorId}`,
                method: "GET",
                params: {}
            })
        }),
        getDeckById: builder.query({
            query: (deckId) => ({
                url: `api/Decks/${deckId}`,
                method: "GET",
                params: {}
            })
        }),
    })
});

export { decksApi };
export const { useGetAllDecksQuery, 
               useGetCommunityDecksQuery,
               useGetPersonalDecksQuery,
               useGetDeckByIdQuery } = decksApi;