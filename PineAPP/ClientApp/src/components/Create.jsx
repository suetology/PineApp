import React, {useEffect, useState} from 'react';
import {Button, Col, Container, Input, Row} from "reactstrap";
import CardsDisplay from "./shared/CardsDisplay";
import {useParams} from 'react-router-dom';
import {useGetDeckByIdQuery, useDeleteDeckByIdMutation, useUpdateDeckByIdMutation} from '../api/decksApi'
import { useNavigate } from 'react-router-dom';
const Create = () => {
    const { id } = useParams();

    const navigate = useNavigate();
    const [deleteDeck] = useDeleteDeckByIdMutation();
    const [updateDeck] = useUpdateDeckByIdMutation();
    const [isEditing, setEditing] = useState(false);
    const [deck, setDeck] = useState(null);

    const handleDelete = async(deckId) => {
        try{
            const response = await deleteDeck(deckId);
            console.log("Successfully deleted deck: ", response);

            navigate("/browse");
        } catch (error)
        {
            console.error('Error deleting deck:', error);
        }
    }

    const refetchData = async () => {
        await deckData.refetch();
        setDeck(deckData.data?.result);
        return deck;
    }

    const handleUpdateDeck = () => {
        setEditing(false);
        const deckDTO = {
            Name: deck.name,
            IsPersonal: deck.isPersonal,
            CreatorId: deck.creatorId,
            Description: deck.description};

        updateDeck({deckId: deck.id, deck: deckDTO});
        
        //temp fix
        window.location.reload();
    }

    const deckData = useGetDeckByIdQuery(id);

    if (deckData.isLoading)
        return(<div>Loading...</div>);

    const deckResult = deckData.data?.result;

    if (!deck) {
        setDeck(deckResult);
        return(<div>Loading...</div>);
    }


    if (!deckResult) {
        return <div>Deck not found</div>;
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
                                <h5 >3</h5>
                                <p>Correct</p>
                            </Col>
                            <Col className="text-center">
                                <h5>3</h5>
                                <p>Not Studied</p>
                            </Col>
                            <Col className="text-center">
                                <h5>3</h5>
                                <p>Wrong</p>
                            </Col>
                        </Row>
                    </Container>
                    <a href={`/study/${id}`} className="btn text-white w-100 " style={{backgroundColor:'#aed683', borderWidth: '0'}}>Study</a>
                </Col>
            </Row>

            <Col className="m-5">
                <p className="mb-2">Cards in deck:</p>
                <CardsDisplay deck={deck} refetchData={refetchData}/>
            </Col>
        </div>
    );
}

export default Create;