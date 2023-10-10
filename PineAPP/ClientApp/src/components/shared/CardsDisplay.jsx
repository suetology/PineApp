import React, {useState} from 'react';
import {Accordion, AccordionBody, AccordionHeader, AccordionItem, Button, Col, Input} from "reactstrap";
import {useAddCardMutation, useDeleteCardByIdMutation, useUpdateCardByIdMutation} from "../../api/decksApi";
import { useNavigate } from 'react-router-dom';
import { useEffect } from 'react';
const CardsDisplay = (props) => {

    const [addCard] = useAddCardMutation();
    const [deleteCard] = useDeleteCardByIdMutation();
    const [updateCard] = useUpdateCardByIdMutation();
    const [open, setOpen] = useState('');
    const [values, setValue] = useState(props.deck.cards);
    const navigate = useNavigate();

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

    const handleNewCard = async () => {
        await addCard({ Back: "Back side", Front: "Front side", DeckId: props.deck.id });

        //TODO refetch kartais veikia kartais ne
        const response = await props.refetchData();
        setValue(response.cards);
        window.location.reload(); //auto refreshas bet yra tas buffering kai spaudi siaip refresha
    }

    const handleDelete = async (id) => {
        await deleteCard(id);

        setOpen();
        const response = await props.refetchData();
        setValue(response.cards);
        window.location.reload();
    }

    const handleUpdateCard = async (id, i) => {
        await updateCard({cardId: id, Front: values[i].front, Back: values[i].back});
        
        setOpen();
        const response = await props.refetchData();
        setValue(response.cards);
        window.location.reload();
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
                    <img onClick={() => handleDelete(card.id)} src="/trash.svg" className="btn" alt="delete" style={{cursor: "pointer"}}/>
                    <img onClick={() => handleUpdateCard(card.id, i)} style={{cursor: "pointer", height: "12pt"}} className="m-2" src="/check.svg" alt="save"/>
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