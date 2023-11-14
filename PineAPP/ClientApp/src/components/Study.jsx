import React, { useEffect, useState, createContext } from 'react';
import { Button, Col, Row, Input } from "reactstrap";
import { useParams } from 'react-router-dom';
import { useGetDeckByIdQuery } from '../api/decksApi';
import './css/Study.css';
import Completion from "./shared/Completion";
import { incrementCorrectAnswers, incrementWrongAnswers } from '../redux/slices';
import { useDispatch, useSelector } from 'react-redux';

const Study = () => {


    const { id } = useParams();
    const [isFlipped, setFlipped] = useState(false);
    const [isEditing, setEditing] = useState(false);
    const [isCompleted, setCompleted] = useState(false);
    const [isLoading, setLoading] = useState(true);
    const [index, setIndex] = useState(0);
    const [cards, setCards] = useState({});
    
    const dispatch = useDispatch();

    const correctAnswers = useSelector(state => state.answers.correctAnswers);
    const wrongAnswers = useSelector(state => state.answers.wrongAnswers);

    const deckData = useGetDeckByIdQuery(id);
    useEffect(() => {
        if (!deckData.isLoading && deckData.isSuccess) {
            setCards(deckData.data.cards);
            setLoading(false);
        }
    }, [deckData]);

    if (isLoading) return (<div>Loading...</div>);

    const cardProgress = (
        <div className="card-progress-container">
            <div className="card-progress">
                {`${index + 1}/${cards.length}`}
            </div>
            <div className="deck-name">
                {deckData.data.name}
            </div>
        </div>
    );
    const handleCardClick = () => {
        setFlipped(!isFlipped);
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
        dispatch(incrementCorrectAnswers());
        setFlipped(false);
        setTimeout(() => {
            if (index < cards.length - 1) setIndex(index + 1);
            else setCompleted(true);
        }, 100);
    };

    const handleWrongClick = () => {
        dispatch(incrementWrongAnswers());
        setFlipped(false);
        setTimeout(() => {
            if (index < cards.length - 1) setIndex(index + 1);
            else setCompleted(true);
        }, 100);
    };
    
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

    if (isCompleted) return (<Completion correct={correctAnswers} wrong={wrongAnswers} deck={deckData.data}/>);  // counts pass to Completion
    
    let buttons = isFlipped && (
        <div className="p-1">
            <Button className="m-1 btn-success shadow" onClick={handleCorrectClick}>Correct</Button>
            <Button className="btn-danger shadow" onClick={handleWrongClick}>Wrong</Button>
        </div>
    );

    return (
        <Row className="h-75 justify-content-center align-items-center pt-2">
            {/* Progress Tracker */}
            <Col md={8} sm={10} className="d-flex justify-content-center">
                {cardProgress}
            </Col>
            <Col id="card-container" md={8} sm={10}>
                <div
                    id="card"
                    className={`h-100 ${isFlipped ? 'flipped' : ''}`}
                    onClick={handleCardClick}
                >
                {isFlipped ? cardBackContent : cardFrontContent }
                </div>
            </Col>
            <Col md={8} sm={10} className="d-flex justify-content-center">
                {buttons}
            </Col>
        </Row>
    );
}
export default Study;
