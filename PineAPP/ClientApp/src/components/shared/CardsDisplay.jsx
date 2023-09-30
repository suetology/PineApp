import React, {useState} from 'react';
import {Accordion, AccordionBody, AccordionHeader, AccordionItem, Col, Input} from "reactstrap";

const CardsDisplay = (props) => {

    const [open, setOpen] = useState('');
    const [values, setValue] = useState(props.cards);
    const toggle = (id) => {
        if (open === id) {
            setOpen();
        } else {
            setOpen(id);
        }
    };
    
    const handleFrontInputChange = (e, i) => {
        const updatedValues = [...values];
        updatedValues[i] = { ...updatedValues[i], front: e.target.value };
        setValue(updatedValues);
    }

    const handleBackInputChange = (e, i) => {
        const updatedValues = [...values];
        updatedValues[i] = { ...updatedValues[i], back: e.target.value };
        setValue(updatedValues);
    }

    const renderAccordionItems = () => {
        return values.map((card, i) => (
                <AccordionItem key={i}>
                    <AccordionHeader targetId={i}>{values[i].front}</AccordionHeader>
                    <AccordionBody accordionId={i}>
                        <Col>
                            <Input
                                value={values[i].front}
                                onChange={(e) => handleFrontInputChange(e, i)}
                            />
                            <Input 
                                value={values[i].back}
                                onChange={(e) => handleBackInputChange(e, i)}
                            />
                        </Col>
                        <img src="/trash.svg" className="btn" alt="delete"/>
                    </AccordionBody>
                </AccordionItem>
        ));
    };
    
    return (
        <Accordion open={open} toggle={toggle}>
            {renderAccordionItems()}
        </Accordion>
    );
}

export default CardsDisplay;