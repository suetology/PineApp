import React, {useEffect, useState} from 'react';
import {Container} from "reactstrap";
import DeckDisplay from "./shared/DeckDisplay";
import {useGetAllDecksByIdQuery, useGetCommunityDecksQuery, useGetPersonalDecksQuery} from "../api/decksApi";
import Loading from "./shared/Loading";
import {useDispatch, useSelector} from "react-redux";
import {setDecks} from "../redux/decksSlice";
import LoginComponent from "./LoginComponent"

const Browse = () => {
    
    const dispatch = useDispatch();
    const decks = useSelector((state) => state.decks);
    const [token, setToken] = useState(JSON.parse(sessionStorage.getItem('token')));
    const [userId, setUserId] = useState(0);

    const deckData = useGetAllDecksByIdQuery(userId)

    useEffect(() => {
        deckData.refetch();
    }, [userId]);
    
    const handleLogin = (newToken) => {
        setToken(newToken);
    }
    
    useEffect(() => {
        if(token) {
            setUserId(token.userId);
        }
    }, [token]);

    if(!token) {
        return <LoginComponent onLogin={handleLogin}></LoginComponent>
    }

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