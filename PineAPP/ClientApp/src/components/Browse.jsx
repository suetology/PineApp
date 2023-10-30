import React, {useEffect, useState} from 'react';
import {Container} from "reactstrap";
import DeckDisplay from "./shared/DeckDisplay";
import {useGetAllDecksByIdQuery, useGetCommunityDecksQuery, useGetPersonalDecksQuery} from "../api/decksApi";
import Loading from "./shared/Loading";
import {useDispatch, useSelector} from "react-redux";
import {setDecks} from "../redux/decksSlice";

const Browse = () => {
    //temp
    const userId = 1;
    const dispatch = useDispatch();
    const decks = useSelector((state) => state.decks);

    const deckData = useGetAllDecksByIdQuery(userId)


    if (deckData.isLoading) {
        return <Loading />;
    }
    
    if (!decks) {
        const formatted = deckData.data.result.reduce((acc, deck) => {
            acc[deck.id] = deck;
            return acc;
        }, {});
        dispatch(setDecks(formatted));
        return <Loading/>
    }
    
    const communityDecks = Object.values(decks).filter(deck => !deck.isPersonal);
    const personalDecks = Object.values(decks).filter(deck => deck.isPersonal);

    
    return(
        <Container className="m-5">
            <div>
                <h5 className="mb-4">Personal decks</h5>
                <DeckDisplay decks={personalDecks}/>
            </div>
            <div>
                <h5 className="mb-4">Community decks</h5>
                <DeckDisplay decks={communityDecks}/>
            </div>
        </Container>
        
    );  
}

export default Browse;