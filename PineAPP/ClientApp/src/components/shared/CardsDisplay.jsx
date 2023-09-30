import React, {useState} from 'react';
import {Accordion, AccordionBody, AccordionHeader, AccordionItem, Col, Input} from "reactstrap";

const CardsDisplay = (props) => {
    //TODO accordion title disappears while editing

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

    const renderAccordionItems = () => {
        console.log(values);
        return values.map((card, i) => (
                <AccordionItem key={i}>
                    <AccordionHeader targetId={i}>{values[i].front}</AccordionHeader>
                    <AccordionBody accordionId={i}>
                        <Col>
                            <Input
                                value={values[i].front}
                                onChange={(e) => handleFrontInputChange(e, i)}
                            />
                            <Input value={values[i].back}/>
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