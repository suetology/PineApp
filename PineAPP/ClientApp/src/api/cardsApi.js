import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

const url = "https://localhost:7074/";

const cardsApi = createApi({
    reducerPath: "cardsApi",
    baseQuery: fetchBaseQuery({ baseUrl: url }),
    endpoints: (builder) => ({
        addCard : builder.mutation({
            query : ({Front, Back, DeckId}) => ({
                url: "api/Card",
                method : "POST",
                body: {
                    Front,
                    Back,
                    DeckId
                }
            }),
        }),
        updateCardById : builder.mutation({
            query : ({cardId, Front, Back}) => ({
                url : `api/Card/${cardId}`,
                method : "PUT",
                body : {
                    Front,
                    Back
                }
            })
        }),
        deleteCardById : builder.mutation ({
            query : (cardId) => ({
                url : `api/Card/${cardId}`,
                method : "DELETE",
                params : {}
            })
        })
    })
});

export { cardsApi };
export const {
    useAddCardMutation,
    useDeleteCardByIdMutation,
    useUpdateCardByIdMutation} = cardsApi;