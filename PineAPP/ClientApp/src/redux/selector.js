// export const selectAllDecks = (state) => state.decks;
//
// export const selectPersonalDecks = (state) => {
//     const allDecks = selectAllDecks(state);
//     return Object.values(allDecks).filter((deck) => deck.isPersonal);
// };
//
// export const selectCommunityDecks = (state) => {
//     const allDecks = selectAllDecks(state);
//     return Object.values(allDecks).filter((deck) => !deck.isPersonal);
// };