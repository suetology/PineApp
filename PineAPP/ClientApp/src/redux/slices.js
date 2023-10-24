import { createSlice } from "@reduxjs/toolkit"

const answersSlice = createSlice({
    name: 'answers',
    initialState : {
        correctAnswers: 0,
        wrongAnswers: 0,
    },
    reducers : {
        incrementCorrectAnswers: (state) => {
            state.correctAnswers += 1;
        },
        incrementWrongAnswers: (state) => {
            state.wrongAnswers += 1;
        }
    }
})


export const { 
    incrementCorrectAnswers, 
    incrementWrongAnswers,
    updateCorrectAnswers,
    updateWrongAnswers} = answersSlice.actions;
export default answersSlice.reducer;