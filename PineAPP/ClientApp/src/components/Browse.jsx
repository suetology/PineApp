import React, {useEffect, useState} from 'react';
import {Container} from "reactstrap";
import DeckDisplay from "./shared/DeckDisplay";
import {useGetCommunityDecksQuery, useGetPersonalDecksQuery} from "../api/decksApi";
import Loading from "./shared/Loading";
import {useDispatch, useSelector} from "react-redux";
import {setDecks} from "../redux/decksSlice";
import {fetchApiData, fetchCommunityDecks, fetchPersonalDecks} from "../redux/apiSlice";
import {selectAllDecks, selectCommunityDecks, selectPersonalDecks} from "../redux/selector";


const Browse = () => {
    //temp
    const userId = 1;
    const [wasLoaded, setWasLoaded] = useState(false);
    const dispatch = useDispatch();
    const communityDecks = useSelector((state) => state.api.communityDecks);
    const personalDecks = useSelector((state) => state.api.personalDecks);
    const decks = useSelector((state) => state.decks);

    useEffect(() => {
        dispatch(fetchCommunityDecks());
        dispatch(fetchPersonalDecks(userId));
    }, []);


    if (!communityDecks || !personalDecks) {
        return <Loading />;
    }
    
    if (!wasLoaded) {
        const personalDecksData = personalDecks.result.reduce((acc, deck) => {
            acc[deck.id] = deck;
            return acc;
        }, {});
        const communityDecksData = communityDecks.result.reduce((acc, deck) => {
            acc[deck.id] = deck;
            return acc;
        }, {});

        dispatch(setDecks({ ...personalDecksData, ...communityDecksData }));
        setWasLoaded(true);
    }
    
    
    return(
        <Container className="m-5">
            <div>
                <h5 className="mb-4">Personal decks</h5>
                <DeckDisplay decks={personalDecks.result}/>
            </div>
            <div>
                <h5 className="mb-4">Community decks</h5>
                <DeckDisplay decks={(communityDecks.result)}/>
            </div>
        </Container>
        
    );  
}

export default Browse;