import React, {useState} from 'react';
import {Accordion, AccordionBody, AccordionHeader, AccordionItem, Button, Col, Input} from "reactstrap";
import {useAddCardMutation, useDeleteCardByIdMutation, useUpdateCardByIdMutation} from "../../api/cardsApi";
import {useParams} from "react-router-dom";
import {setDecks} from "../../redux/decksSlice";
import {useDispatch, useSelector} from "react-redux";


const CardsDisplay = (props) => {
    
    const decks = useSelector((state) => state.decks);
    const dispatch = useDispatch();
    const [addCard] = useAddCardMutation();
    const [deleteCard] = useDeleteCardByIdMutation();
    const [updateCard] = useUpdateCardByIdMutation();
    const [open, setOpen] = useState('');
    const [values, setValue] = useState(props.deck.cards);
    const deckId = props.deckId;
    
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
        const newCard = await addCard({ Back: "Back side", Front: "Front side", DeckId: deckId });
        const updatedValues = [...values, newCard.data]
        setValue(updatedValues);
        dispatch(setDecks({[deckId]: {...decks[deckId], cards: updatedValues}}));
    }

    const handleDelete = async (cardId) => {
        await deleteCard(cardId);

        setOpen();
        const updatedValues = values.filter((card) => card.id !== cardId);
        setValue(updatedValues);
        dispatch(setDecks({[deckId]: {...decks[deckId], cards: updatedValues}}));
    }

    const handleUpdateCard = async (cardId, i) => {
        await updateCard({cardId: cardId, Front: values[i].front, Back: values[i].back});
        
        const updatedCards = [...decks[deckId].cards]
        updatedCards[i] = values[i];
        
        dispatch(setDecks({[deckId]: {...decks[deckId], cards: updatedCards} }));
        setOpen();
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
                    <img onClick={() => handleUpdateCard(card.id, i)} style={{cursor: "pointer", height: "12pt"}} className="m-2" src="/check.svg" alt="save" data-testid={`save-button-${i}`}/>
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