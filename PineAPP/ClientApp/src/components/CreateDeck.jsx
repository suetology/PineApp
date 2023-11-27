import React from 'react';
import {Button, Input, Label, Col, FormGroup, Form, Container, Row} from 'reactstrap';
import { useState, useEffect } from 'react';
import { useAddDeckMutation} from '../api/decksApi';
import { useNavigate } from 'react-router-dom';
import {FileUpload} from "./FileUpload";
import LoginComponent from "./LoginComponent"
import {useDispatch, useSelector} from "react-redux";
import {setDecks} from "../redux/decksSlice";

const CreateDeck = () => {

    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [creatorId, setCreatorId] = useState(0);
    const [isPersonal, setIsPersonal] = useState(false);
    const navigate = useNavigate();
    const dispatch = useDispatch();

    // Retrieve userId
    useEffect(() => {
        setCreatorId((JSON.parse(sessionStorage.getItem('token')).userId));
    });

    const handleNameChange = (e) => {
        setName(e.target.value);
    }

    const handleDescriptionChange = (e) => {
        setDescription(e.target.value);
    }

    const handleIsPersonalChange = (e) => {
        setIsPersonal(e.target.value === "Personal");
    }

    const [addDeck] = useAddDeckMutation();
    
    const handleAddDeck = async (e) => {
        e.preventDefault();

        const intCreatorId = +creatorId;
        
        const requestData =  {
            Name : name,
            IsPersonal : isPersonal,
            CreatorId : intCreatorId,
            Description : description,
         };

        try {
            const response = await addDeck((requestData), {
              headers: {
                'Content-Type': 'application/json',
              },
            });
            
            console.log("No error: " , response);
            
            dispatch(setDecks({[response.data.id]: {...response.data, cards: []}}))

            navigate("/browse");
          } catch (error) {
            console.error("Error adding deck:", error);
          }
    }

    return(
        <Row className="justify-content-center mt-5">
            <Col xs={10} xl={8} className="p-5 border border-light rounded shadow bg-white">
                <Form onSubmit={handleAddDeck}>
                    <div>
                        <h1>Create new deck</h1>
                        <label htmlFor="deckName">Name</label>
                        <Input id="deckName" value = {name} onChange = {handleNameChange}></Input>
                        <label htmlFor="deckDescription">Description</label>
                        <Input id="deckDescription" value={description} onChange={handleDescriptionChange} type="textarea" />
                    </div>
                    <div>
                        <label>What type of deck are you creating?</label>
                        <FormGroup check>
                            <Input
                                name="radio1"
                                type="radio"
                                value= "Personal"
                                onChange={handleIsPersonalChange}
                                checked={isPersonal}
                            />
                            {' '}
                            <Label check>
                                Personal Deck
                            </Label>
                        </FormGroup>
                        <FormGroup check>
                            <Input
                                name="radio1"
                                type="radio"
                                value="Community"
                                onChange = {handleIsPersonalChange}
                                checked = {!isPersonal}
                            />
                            {' '}
                            <Label check>
                                Community Deck
                            </Label>
                        </FormGroup>
                    </div>
                    <div>
                        <Button className = "mt-3" type = "submit">Add Deck</Button>
                        <FileUpload />
                    </div>
                </Form>
            </Col>
        </Row>
    );
}

export default CreateDeck