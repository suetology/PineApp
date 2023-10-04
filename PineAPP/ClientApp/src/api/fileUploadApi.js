import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

const url = "https://localhost:7074/";

const fileUploadApi = createApi({
    reducerPath: "fileUploadApi",
    baseQuery: fetchBaseQuery({ baseUrl: url }),
    endpoints: (builder) => ({
        uploadFile: builder.mutation({
            query: (file) => ({
                url: "api/FileUpload",
                method: "POST",
                body: file
            })
        })
    })
});

export { fileUploadApi };
export const { useUploadFileMutation } = fileUploadApi;