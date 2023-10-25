import React, {useEffect, useState} from 'react';
import {Container} from "reactstrap";
import DeckDisplay from "./shared/DeckDisplay";
import {useGetCommunityDecksQuery, useGetPersonalDecksQuery} from "../api/decksApi";
import Loading from "./shared/Loading";
import {useDispatch, useSelector} from "react-redux";
import {setDecks} from "../redux/decksSlice";


const Browse = () => {
    //temp
    const userId = 1;
    const [wasLoaded, setWasLoaded] = useState(false);
    const dispatch = useDispatch();

    const decks = useSelector((state) => state.decks);

    console.log(decks);

    const personalData = useGetPersonalDecksQuery(userId);
    const communityData = useGetCommunityDecksQuery();

    useEffect(() => {
        personalData.refetch();
        communityData.refetch();
        ///nu dara kart pabandyt su refetch
        console.log(personalData.isLoading);
        
    }, []);
    
    if (personalData.isLoading || communityData.isLoading){
        return(<Loading/>);
    }

    if (!personalData.isLoading && !communityData.isLoading && !wasLoaded) {
        const personalDecksData = personalData.data.result.reduce((acc, deck) => {
            acc[deck.id] = deck;
            return acc;
        }, {});

        const communityDecksData = communityData.data.result.reduce((acc, deck) => {
            acc[deck.id] = deck;
            return acc;
        }, {});

        setWasLoaded(true);

        dispatch(setDecks({ ...personalDecksData, ...communityDecksData }));
    }
    
    return(
        <Container className="m-5">
            <div>
                <h5 className="mb-4">Personal decks</h5>
                <DeckDisplay decks={personalData.data.result}/>
            </div>
            <div>
                <h5 className="mb-4">Community decks</h5>
                <DeckDisplay decks={communityData.data.result}/>
            </div>
        </Container>
        
    );  
}

export default Browse;