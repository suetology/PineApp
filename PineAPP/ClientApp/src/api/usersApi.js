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
            query: (userId) => ({
                url: `api/Users/GetUserById/${userId}`,
                method: "GET",
                params: {}
            }),
        }),
        getUserByEmail: builder.query({
            query: (email) => ({
                url: `api/Users/GetUserByEmail/${email}`,
                method: "GET",
                params: {}
            }),
        }),
        addUser: builder.query({
            query: ({Email, UserName, Password}) => ({
                url: "api/Users/Add",
                method: "POST",
                body: {
                    Email,
                    UserName,
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
    useGetUserByEmailQuery,
    userAddUserMutation,
} = usersApi;







