import React, {useEffect, useState} from 'react';
import {Button, Col, Container, Row} from "reactstrap";
import {Link} from "react-router-dom";
import Browse from "../Browse";
import {useUpdateDeckByIdMutation} from "../../api/decksApi";

const Completion = ({ correct, wrong, deck }) => {

    const [updateDeck] = useUpdateDeckByIdMutation();
    
    useEffect(() => {
        const deckDTO = {
            Name: deck.name,
            IsPersonal: deck.isPersonal,
            CreatorId: deck.creatorId,
            Description: deck.description,
            Correct: correct,
            Wrong: wrong};

        updateDeck({deckId: deck.id, deck: deckDTO});
    }, []);

    return (
        <Container>
            <Row className="justify-content-center">
                <Col lg={8} className="text-center">
                    <img src="/check.svg" alt="success"  style={{ width: '50%'}}/>
                    <p>All cards reviewed</p>
                    <p>Correct answers: {correct}</p> 
                    <p>Wrong answers: {wrong}</p>  
                    <Link className="btn btn-light shadow border-dark" to="/Browse" style={{width: '150pt'}}>Return</Link>
                </Col>
            </Row>
        </Container>
    );
}

export default Completion;
