import React from 'react';
import {Container} from "reactstrap";
import DeckDisplay from "./shared/DeckDisplay";
import {
    useGetCommunityDecksQuery,
    useGetPersonalDecksQuery
} from "../api/decksApi";
import { useEffect, useState } from 'react';
import LoginComponent from "./LoginComponent"
import Loading from "./shared/Loading";

const Browse = () => {

    //Login functionality
    const [token, setToken] = useState(JSON.parse(sessionStorage.getItem('token')));
    const [userId, setUserId] = useState(0);
    const handleLogin = (newToken) => {
        setToken(newToken);
    }
    useEffect(() => {
        if(token) {
            setUserId(token.userId);
        }
    }, [token]);
    
    const personalData = useGetPersonalDecksQuery(userId);
    const communityData = useGetCommunityDecksQuery();

    useEffect(() => {
        // Trigger data fetching when the component mounts
        // Component being when we navigate back to /browse
        personalData.refetch();
        communityData.refetch();
    }, []);

    if(!token) {
        return <LoginComponent onLogin={handleLogin}></LoginComponent>
    }
    
    if (personalData.isLoading || communityData.isLoading) 
        return(<Loading/>);
    
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