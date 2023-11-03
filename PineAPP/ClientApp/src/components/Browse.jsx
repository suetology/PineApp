import {Container} from "reactstrap";
import React, {useEffect, useState} from 'react';
import DeckDisplay from "./shared/DeckDisplay";
import Loading from "./shared/Loading";
import {useGetAllDecksByIdQuery, useGetCommunityDecksQuery, useGetPersonalDecksQuery} from "../api/decksApi";
import {useDispatch, useSelector} from "react-redux";
import {setDecks} from "../redux/decksSlice";
import LoginComponent from "./LoginComponent"

const url = "https://localhost:7074/";


const Browse = () => {

    
    const [loadingSearch, setLoadingSearch] = useState(false);
    const [searchError, setSearchError] = useState(null);

    const [searchKeyword, setSearchKeyword] = useState('');
    const [searchResults, setSearchResults] = useState([]);

    //Login functionality
    const dispatch = useDispatch();
    const decks = useSelector((state) => state.decks);
    const userId = JSON.parse(sessionStorage.getItem('token')).userId;
    
    const deckData = useGetAllDecksByIdQuery(userId);
    
    useEffect(() => {
        // Trigger data fetching when the component mounts or when searchKeyword changes
        if (searchKeyword) {
            fetchSearchResults();
        }
    }, [searchKeyword]);
    

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
    
    const personalDecksFiltered = personalDecks.filter(deck => deck.name.includes(searchKeyword));
    const communityDecksFiltered = communityDecks.filter(deck => deck.name.includes(searchKeyword));

    let decksToDisplay = [];
    if (searchResults.length > 0) {
        decksToDisplay = searchResults;
    } else {
        decksToDisplay = personalDecksFiltered.concat(communityDecksFiltered);
    }
    
    const fetchSearchResults = async () => {
        try {
            setLoadingSearch(true);
            const response = await fetch(url + `api/Decks/Search/${searchKeyword}`);

            if (response.ok) {
                const data = await response.json();
                setSearchResults(data.result);
                setSearchError(null);
            } else {
                if (response.status === 404) {
                    setSearchResults([]); // Clear search results when no results found
                    setSearchError('No results found.');
                } else {
                    setSearchError('An error occurred while searching.');
                }
            }
        } catch (error) {
            console.error('Search error:', error);
            setSearchResults([]); // Clear search results on error
            setSearchError('An error occurred while searching.');
        } finally {
            setLoadingSearch(false);
        }
    };
    
    return (
        <div>
            <input
                type="text"
                placeholder="Search decks by keyword"
                value={searchKeyword}
                onChange={(e) => setSearchKeyword(e.target.value)}
            />
            <button onClick={fetchSearchResults} disabled={loadingSearch}>
                {loadingSearch ? 'Searching...' : 'Search'}
            </button>

            {searchError && <p>{searchError}</p>}
            {loadingSearch && <Loading />}
            {searchResults.length > 0 && (
                <div>
                    {searchResults.map((deck) => (
                        <div key={deck.id}>
                        </div>
                    ))}
                </div>
            )}

            <Container className="m-5">
                <div>
                    <h5 className="mb-4">Personal decks</h5>
                    <DeckDisplay decks={personalDecksFiltered} />
                </div>
                <div>
                    <h5 className="mb-4">Community decks</h5>
                    <DeckDisplay decks={communityDecksFiltered} />
                </div>
            </Container>
        </div>
    );
}

export default Browse;