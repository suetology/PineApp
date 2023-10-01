import React, {Component, useEffect, useState} from 'react';
import {Button, Col, Container, Input, Row} from "reactstrap";
import {useParams} from 'react-router-dom';
import {useGetDeckByIdQuery} from '../api/decksApi'
import './css/Study.css';
import Completion from "./shared/Completion";

const Study = () => {
    const { id } = useParams();
    
    const [isFlipped, setFlipped] = useState(false);
    const [isEditing, setEditing] = useState(false);
    const [isCompleted, setCompleted] = useState(false);
    const [isLoading, setLoading] = useState(true);
    const [index, setIndex] = useState(0);
    const [cards, setCards] = useState({});
    
    const deckData = useGetDeckByIdQuery(id);

    useEffect(() => {
        if (!deckData.isLoading && deckData.isSuccess) {
            setCards(deckData.data.result.cards);
            setLoading(false);
        }
    }, [deckData]);
    
    if (isLoading)
        return (<div>Loading...</div>)
    
    if (isCompleted)
        return (<Completion/>)

    const handleCardClick = () => {
        setFlipped(true);
    };

    const handleEditClick = () => {
        setEditing(true);
    };

    const handleBackClick = () => {
        if (index !== 0)
            setIndex(index-1);
    }

    const handleFrontChange = (newValue) => {
        const newCard = [...cards];
        newCard[index] = {...newCard[index], front: newValue}
        setCards(newCard);
    }

    const handleBackChange = (newValue) => {
        const newCard = [...cards];
        newCard[index] = {...newCard[index], back: newValue}
        setCards(newCard);
    }

    const handleExamplesChange = (newValue) => {
        const newCard = [...cards];
        newCard[index] = {...newCard[index], examples: newValue}
        setCards(newCard);
    }

    const handleSave = () => {
        setEditing(false);
    }

    const handleCorrectClick = () => {
        setFlipped(false);
        if (index < cards.length - 1)
            setIndex(index + 1);
        else 
            setCompleted(true);
    };

    const handleWrongClick = () => {
        setFlipped(false);
        if (index < cards.length - 1)
            setIndex(index + 1);
        else
            setCompleted(true);
    }

    let editableContent = isEditing
        ? <div>
            <Input className="fw-bold mb-2" value={cards[index].front} onChange={
                (e) => handleFrontChange(e.target.value)
            }/>
            <Input value={cards[index].back} type="textarea" onChange={
                (e) => handleBackChange(e.target.value)
            }/>
            <Input value={cards[index].examples} type="textarea" onChange={
                (e) => handleExamplesChange(e.target.value)
            }/>
            <Button onClick={handleSave}>Save</Button>
        </div>
        : <div>
            <h2 className="fw-bold mb-2">{cards[index].front}</h2>
            <p>{cards[index].back}</p>
            <p>{cards[index].examples}</p>
        </div>;

    let content = isFlipped
        ? <div>
            {editableContent}
        </div>
        : <div>
            <h2 className="fw-bold mb-2">{cards[index].front}</h2>
            <p style={{fontSize: '10pt'}}>click to flip card</p>
        </div>;


    let buttons = isFlipped
        ? <div className="p-1">
            <Button className="m-1 btn-success shadow" onClick={handleCorrectClick}>Correct</Button>
            <Button className="btn-danger shadow" onClick={handleWrongClick}>Wrong</Button>
        </div>
        : null;
    
    return (
        <Row className="h-75 justify-content-center align-items-center pt-2">
            <Col
                id="card"
                className={"h-100 bg-white border rounded shadow p-4 text-center"}
                md={8}
                sm={10}
                onClick={handleCardClick}
            >
                <Row className="h-100">
                    <Col xs={1}>
                        <img className="btn" src="/arrow-90deg-left.svg" alt="back" onClick={handleBackClick}/>
                    </Col>
                    <Col xs={10} className="d-flex align-items-center justify-content-center">
                        <div>
                            {content}
                        </div>
                    </Col>
                    <Col xs={1}>
                        <img className="bg-white btn" src="/pencil.svg" alt="edit" onClick={handleEditClick}/>
                    </Col>
                </Row>
            </Col>
            <div className="d-flex justify-content-center align-items-center">
                {buttons}
            </div>
        </Row>
    );
}

export default Study;