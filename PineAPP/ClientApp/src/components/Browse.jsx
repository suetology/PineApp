import React from 'react';
import {Container} from "reactstrap";
import DeckDisplay from "./shared/DeckDisplay";
import {useGetCommunityDecksQuery, useGetPersonalDecksQuery} from "../api/decksApi";
import Loading from "./shared/Loading";
import {useDispatch, useSelector} from "react-redux";
import {setDecks} from "../redux/decksSlice";


const Browse = () => {
    //temp
    const userId = 1;

    const dispatch = useDispatch();
    // const personalDecks = useSelector(selectPersonalDecks);
    // const communityDecks = useSelector(selectCommunityDecks);

    const decks = useSelector( state => state.decks);


    const personalData = useGetPersonalDecksQuery(userId);
    const communityData = useGetCommunityDecksQuery();

    if (!personalData.isLoading && !communityData.isLoading) {
        const personalDecksData = personalData.data.result.reduce((acc, deck) => {
            acc[deck.id] = deck;
            return acc;
        }, {});

        const communityDecksData = communityData.data.result.reduce((acc, deck) => {
            acc[deck.id] = deck;
            return acc;
        }, {});

        //dispatch(setDecks({ ...personalDecksData, ...communityDecksData }));
        console.log(setDecks({ ...personalDecksData, ...communityDecksData }));
    }
    
    
    if (personalData.isLoading || communityData.isLoading) 
        return(<Loading/>);
    
    
    return(
        <Container className="m-5">
            <div>
                <h5 className="mb-4">Personal decks</h5>
                <DeckDisplay decks={{}}/>
            </div>
            <div>
                <h5 className="mb-4">Community decks</h5>
                <DeckDisplay decks={{}}/>
            </div>
        </Container>
        
    );  
}

export default Browse;