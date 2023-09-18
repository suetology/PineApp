import React from 'react';
import { Container, Row, Col } from 'reactstrap';
import '../css/DeckDisplay.css'

//temp list
var decks = [
    'deck1',
    'deck2',
    'deck3',
    'deck4',
    'deck5',
    'deck6',
    'deck7',
    'deck8',
    'deck9'
];

const DeckDisplay = () => {
    const itemsPerRow = 6;
    const rows = [];
    
    for (let i = 0; i < decks.length; i += itemsPerRow) {
        const row = decks.slice(i, i + itemsPerRow);

        const rowElements = row.map((deck, index) => (
            <Col key={index} md={12/itemsPerRow} xs={6}>
                <div className="image-container">
                    <a href="/create"><div className="text-overlay">{deck}</div></a>
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