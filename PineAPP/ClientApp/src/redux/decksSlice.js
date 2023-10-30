import { createSlice } from '@reduxjs/toolkit';

const decksSlice = createSlice({
    name: 'decks',
    initialState: null,
    reducers: {
        setDecks: (state, action) => {
            const decksData = action.payload;
            return { ...state, ...decksData };
        }
    }
});

export const { setDecks } = decksSlice.actions;

export default decksSlice.reducer;