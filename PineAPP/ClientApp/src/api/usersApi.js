import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";

const url = "https://localhost:7074/";

const usersApi = createApi({
    reducerPath: "usersApi",
    baseQuery: fetchBaseQuery({ baseUrl: url }),
    endpoints: (builder) => ({
        getAllUsers: builder.query({
            query: () => ({
                url: "api/Users",
                method: "GET",
                params: {}
            }),
            providesTags: ["Users"],
        }),
        getUserById: builder.query({
            query: () => ({
                url: "api/Users/GetUserById/",
                method: "GET",
                params: {}
            }),
        }),
        getAllDecks: builder.query({
            query: ({Email, Username, Password}) => ({
                url: "api/Users/Add",
                method: "POST",
                body: {
                    Email,
                    Username,
                    Password,
                }
            }),
            invalidatesTags: ["Users"],
        }),
    })
});

export { usersApi };
export const {
    useGetAllUsersQuery,
    useGetUserByIdQuery,
    userAddUserMutation,
} = usersApi







