import React, {useEffect, useState, useReducer} from 'react';
import {Button, Col, Container, Input, Row} from "reactstrap";
import CardsDisplay from "./shared/CardsDisplay";
import {useParams} from 'react-router-dom';
import {useDeleteDeckByIdMutation, useUpdateDeckByIdMutation, useGetAllDecksByIdQuery} from '../api/decksApi'
import { useNavigate } from 'react-router-dom';
import {useDispatch, useSelector} from "react-redux";
import {setDecks, deleteDeckState} from "../redux/decksSlice";

const Create = () => {
    const [userId, setUserId] = useState(JSON.parse(sessionStorage.getItem('token')).userId); //temp
    const { id } = useParams();
    const navigate = useNavigate();
    const [deleteDeck] = useDeleteDeckByIdMutation();
    const [updateDeck] = useUpdateDeckByIdMutation();
    const [isEditing, setEditing] = useState(false);
    const [wasLoaded, setWasLoaded] = useState(false);

    const dispatch = useDispatch();
    const decks = useSelector((state) => state.decks);
    const [deck, setDeck] = useState(null);
    
    const deckData = useGetAllDecksByIdQuery(userId);
    
    const handleDelete = async(deckId) => {
        try{
            const response = await deleteDeck(deckId);
            console.log("Successfully deleted deck: ", response);
            
            dispatch(deleteDeckState(deckId));
            
            navigate("/browse");
        } catch (error)
        {
            console.error('Error deleting deck:', error);
        }
    }

    const handleUpdateDeck = async () => {
        setEditing(false);
        const deckDTO = {
            Name: deck.name,
            IsPersonal: deck.isPersonal,
            CreatorId: deck.creatorId,
            Description: deck.description,
            Correct: deck.correct,
            Wrong: deck.wrong};

        await updateDeck({deckId: id, deck: deckDTO});

        dispatch(setDecks({[id]: {...decks[id], name: deck.name, description: deck.description}}));
    }


    
    if (deckData.isLoading)
        return(<div>Loading...</div>);
    

    if (!decks) {
        const formatted = deckData.data.reduce((acc, deck) => {
            acc[deck.id] = deck;
            return acc;
        }, {});
        console.log(formatted);
        dispatch(setDecks(formatted));

        setDeck(formatted[id]);

        return(<div>Loading...</div>); 
    }
   
    if (!deck){
        return(<div>Loading...</div>); 
    }
    
    const name =
        isEditing
            ? <div>
                <Input type="text"
                       value={deck.name}
                       onChange={(e) => setDeck({...deck, name: e.target.value})}/>
            </div>
            : <p className="h3"
                 style={{cursor: 'pointer'}}
                 onClick={() => setEditing(true)}>
                {deck.name}
            </p>;

    const description =
        isEditing
            ? <div>
                <Input type="textarea"
                       value={deck.description}
                       onChange={(e) => setDeck({...deck, description: e.target.value})}/>
            </div>
            : <p className="border border-dark bg-light p-2 pr-5 pb-5"
                 style={{cursor: 'pointer'}}
                 onClick={() => setEditing(true)}>
                {deck.description}
            </p>;

    return(
        <div>
            <Row className="m-5">
                <Col sm={4}>
                    <div>{name}</div>
                    <div>{description}</div>
                    <img onClick={() => {handleDelete(deck.id)}} style={{cursor: "pointer", height: "12pt"}} className="m-2" src="/trash.svg" alt="delete"/>
                    <img onClick={() => setEditing(true)} style={{cursor: "pointer", height: "12pt"}} className="m-2" src="/pencil.svg" alt="edit"/>
                    <img onClick={() => handleUpdateDeck()} style={{cursor: "pointer", height: "12pt"}} className="m-2" src="/check.svg" alt="save"/>
                </Col>
                <Col sm={4}>
                    <Container className="border rounded border-dark bg-light p-2 mb-2">
                        <Row>
                            <Col className="text-center">
                                <h5 >{deck.correct}</h5>
                                <p>Correct</p>
                            </Col>
                            <Col className="text-center">
                                <h5>{deck.wrong}</h5>
                                <p>Wrong</p>
                            </Col>
                        </Row>
                    </Container>
                    <a href={`/study/${id}`} className="btn text-white w-100 " style={{backgroundColor:'#aed683', borderWidth: '0'}}>Study</a>
                </Col>
            </Row>

            <Col className="m-5">
                <p className="mb-2">Cards in deck:</p>
                <CardsDisplay deck={deck} deckId={id}/>
            </Col>
        </div>
    );
}

export default Create;