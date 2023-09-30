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
    
    const handleNewCard = () => {
        //TODO currently temp card
        const updatedValues = [...values, {back: "aa", front: "aaa"}];
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
        <div>
            <Accordion open={open} toggle={toggle}>
                {renderAccordionItems()}
            </Accordion>
            
            <div className="border border-dark border-opacity-10 border-1 rounded bg-white justify-content-center d-flex"
                 style={{height: '50px', cursor: 'pointer'}}
                 onClick={handleNewCard}>
                <img src="/plus.svg" alt="add"/>
            </div>
        </div>
        
    );
}

export default CardsDisplay;