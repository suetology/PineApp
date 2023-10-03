import React from 'react';
import {Container} from "reactstrap";
import DeckDisplay from "./shared/DeckDisplay";
import {
    useGetCommunityDecksQuery,
    useGetPersonalDecksQuery
} from "../api/decksApi";
import { useEffect } from 'react';


const Browse = () => {
    //temp
    const userId = 1;
    
    const personalData = useGetPersonalDecksQuery(userId);
    const communityData = useGetCommunityDecksQuery();

    useEffect(() => {
        // Trigger data fetching when the component mounts
        // Component being when we navigate back to /browse
        personalData.refetch();
        communityData.refetch();
    }, []);
    
    if (personalData.isLoading || communityData.isLoading) 
        return(<div>Loading...</div>);
    
    const personalDecks = personalData.data.result;
    const communityDecks = communityData.data.result;

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