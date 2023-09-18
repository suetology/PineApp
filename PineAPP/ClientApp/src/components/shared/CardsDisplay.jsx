import React, {useState} from 'react';
import {Accordion, AccordionBody, AccordionHeader, AccordionItem, Col, Input} from "reactstrap";

function CardsDisplay () {
    
    //temp list
    const cards = [
        'card1',
        'card2',
        'card3',
        'card4',
        'card5',
        'card6',
        'card7',
        'card8',
        'card9'
    ]

    const [open, setOpen] = useState('');
    const [values, setValue] = useState(cards);
    const toggle = (id) => {
        if (open === id) {
            setOpen();
        } else {
            setOpen(id);
        }
    };
    
    let rows = [];

    for (let i = 0; i < cards.length; i++) {
        rows.push(
            
                <AccordionItem>
                    <AccordionHeader targetId={i}>{values[i]} Front side</AccordionHeader>
                    <AccordionBody accordionId={i}>
                        <Col>
                            <Input
                                value={values[i]}
                                onChange={(e) => {
                                    const updatedValues = [...values];
                                    updatedValues[i] = e.target.value;
                                    setValue(updatedValues);
                                }}
                            />
                            <Input value="Back side"/>
                        </Col>
                        <img src="/trash.svg" className="btn" alt="delete"/>
                    </AccordionBody>
                </AccordionItem>
        );
    }
    
    return (
        <Accordion open={open} toggle={toggle}>
            {rows}
        </Accordion>
    );
}

export default CardsDisplay;