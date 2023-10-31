import { createSlice } from '@reduxjs/toolkit';

const decksSlice = createSlice({
    name: 'decks',
    initialState: null,
    reducers: {
        setDecks: (state, action) => {
            const decksData = action.payload;
            return { ...state, ...decksData };
        },
        deleteDeckState: (state, action) => {
            const deckIdToDelete = action.payload;
            const { [deckIdToDelete]: deletedDeck, ...remainingDecks } = state;
            return remainingDecks;
        }
    }
});

export const { setDecks,
               deleteDeckState} = decksSlice.actions;

export default decksSlice.reducer;