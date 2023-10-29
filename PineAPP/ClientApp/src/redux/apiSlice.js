import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';

const baseUrl = "https://localhost:7074/";

export const fetchApiData = createAsyncThunk('api/fetchApiData', async (url) => {
    const response = await fetch(baseUrl + url);
    if (!response.ok) {
        throw new Error('Network response error');
    }
    return response.json();
});

export const fetchCommunityDecks = createAsyncThunk('api/fetchCommunityDecks', async () => {
    const response = await fetch(baseUrl + 'api/Decks/Community'); // Replace 'CommunityDecks' with the actual endpoint
    if (!response.ok) {
        throw new Error('Network response error');
    }
    return response.json();
});

export const fetchPersonalDecks = createAsyncThunk('api/fetchPersonalDecks', async (creatorId) => {
    const response = await fetch(baseUrl + `api/Decks/Personal/${creatorId}`); // Replace 'PersonalDecks' with the actual endpoint
    if (!response.ok) {
        throw new Error('Network response error');
    }
    return response.json();
});

const apiSlice = createSlice({
    name: 'api',
    initialState: {
        data: null,
        communityDecks: null,  // Add this line
        personalDecks: null,   // Add this line
        status: 'idle',
        error: null,
    },
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(fetchApiData.pending, (state) => {
                state.status = 'loading';
            })
            .addCase(fetchApiData.fulfilled, (state, action) => {
                state.status = 'succeeded';
                state.data = action.payload;
            })
            .addCase(fetchApiData.rejected, (state, action) => {
                state.status = 'failed';
                state.error = action.error.message;
            })
            .addCase(fetchCommunityDecks.fulfilled, (state, action) => {
                state.communityDecks = action.payload;
            })
            .addCase(fetchPersonalDecks.fulfilled, (state, action) => {
                state.personalDecks = action.payload;
            });
    },
});

export default apiSlice.reducer;
