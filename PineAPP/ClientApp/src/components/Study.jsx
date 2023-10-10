import React, { useEffect, useState } from 'react';
import { Button, Col, Row, Input } from "reactstrap";
import { useParams } from 'react-router-dom';
import { useGetDeckByIdQuery } from '../api/decksApi';
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

    if (isLoading) return (<div>Loading...</div>);
    if (isCompleted) return (<Completion/>);
    
    const handleCardClick = () => {
        setFlipped(!isFlipped);
    };

    const handleEditClick = () => {
        setEditing(true);
    };

    const handleBackClick = () => {
        if (index !== 0) setIndex(index - 1);
    };

    const handleSave = () => {
        setEditing(false);
    };

    const handleChange = (type, newValue) => {
        const newCard = [...cards];
        newCard[index] = { ...newCard[index], [type]: newValue };
        setCards(newCard);
    };

    const handleCorrectClick = () => {
        setFlipped(false);
        if (index < cards.length - 1) setIndex(index + 1);
        else setCompleted(true);
    };

    const handleWrongClick = () => {
        setFlipped(false);
        if (index < cards.length - 1) setIndex(index + 1);
        else setCompleted(true);
    }
    
    let cardFrontContent = isEditing
        ? (
            <div className="card-face card-front">
                <Input className="fw-bold mb-2" value={cards[index].front} onChange={(e) => handleChange('front', e.target.value)} />
                <Button onClick={handleSave}>Save</Button>
            </div>
        )
        : (
            <div className="card-face card-front">
                <div className="card-content-container">
                    <h2 className="fw-bold mb-2">{cards[index].front}</h2>
                    <p style={{ fontSize: '10pt' }}>click to flip the card</p>
                </div>
            </div>
        );

    let cardBackContent = isEditing
        ? (
            <div className="card-face card-back">
                <Input value={cards[index].back} type="textarea" onChange={(e) => handleChange('back', e.target.value)} />
                <Input value={cards[index].examples} type="textarea" onChange={(e) => handleChange('examples', e.target.value)} />
                <Button onClick={handleSave}>Save</Button>
            </div>
        )
        : (
            <div className="card-face card-back">
                <h2 className="fw-bold mb-2">{cards[index].back}</h2>
                <p>{cards[index].examples}</p>
            </div>
        );

    let buttons = isFlipped && (
        <div className="p-1">
            <Button className="m-1 btn-success shadow" onClick={handleCorrectClick}>Correct</Button>
            <Button className="btn-danger shadow" onClick={handleWrongClick}>Wrong</Button>
        </div>
    );

    return (
        <Row className="h-75 justify-content-center align-items-center pt-2">
            <Col id="card-container" md={8} sm={10}>
                <div
                    id="card"
                    className={`h-100 ${isFlipped ? 'flipped' : ''}`}
                    onClick={handleCardClick}
                >
                    {cardFrontContent}
                    {cardBackContent}
                </div>

            </Col>
            <div className="d-flex justify-content-center align-items-center">
                {buttons}
            </div>
        </Row>
    );
}

export default Study;
