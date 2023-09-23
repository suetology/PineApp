import React from 'react';
import { Container, Row, Col } from 'reactstrap';
import '../css/DeckDisplay.css'
import {Link} from "react-router-dom";

const DeckDisplay = (props) => {
    const itemsPerRow = 6;
    const rows = [];
    
    for (let i = 0; i < props.decks.length; i += itemsPerRow) {
        const row = props.decks.slice(i, i + itemsPerRow);

        const rowElements = row.map((deck, index) => (
            <Col key={index} md={12/itemsPerRow} xs={6}>
                <div className="image-container pl-2">
                    <Link to={`/create/${props.decks[i].id}`}><div className="text-overlay">{deck.name}</div></Link>
                    <img src="/deck.svg" className="img-fluid"  alt="deck:"/>
                </div>
            </Col>
        ));

        rows.push(
            <Row key={i} className="pb-4">
                {rowElements}
            </Row>
        );
    }

    return (
        <Container>
            {rows}
        </Container>
    );
};

export default DeckDisplay;