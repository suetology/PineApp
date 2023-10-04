import React from 'react';
import {Button, Input, Label, Col, FormGroup, Form, Container, Row} from 'reactstrap';
import { useState } from 'react';
import { useAddDeckMutation} from '../api/decksApi';
import { useNavigate } from 'react-router-dom';

const CreateDeck = () => {

    const [name, setName] = useState("");

    const [description, setDescription] = useState("");

    const [creatorId, setCreatorId] = useState(0);

    const [isPersonal, setIsPersonal] = useState(false);

    const navigate = useNavigate();

    const handleNameChange = (e) => {
        setName(e.target.value);
    }

    const handleDescriptionChange = (e) => {
        setDescription(e.target.value);
    }

    const handleCreatorIdChange = (e) => {
        setCreatorId(e.target.value);
    }

    const handleIsPersonalChange = (e) => {
        setIsPersonal(e.target.value == "Personal");
    }

    const [addDeck] = useAddDeckMutation();
    
    const handleAddDeck = async (e) => {
        e.preventDefault();

        const intCreatorId = +creatorId;

        console.log(name, description, intCreatorId, isPersonal);
    
        const requestData =  {
            Name : name,
            IsPersonal : isPersonal,
            CreatorId : intCreatorId,
            Description : description,
         };

        try {
            const response = await addDeck((requestData), {
              headers: {
                'Content-Type': 'application/json', // Set the content type to JSON
              },
            });
            
            console.log("No error: " , response);

            navigate("/browse");
          } catch (error) {
            console.error("Error adding deck:", error);
            // Handle errors here
          }
    }

    return(
        <Row className="justify-content-center mt-5">
            <Col xs={10} xl={8} className="p-5 border border-light rounded shadow bg-white">
                <Form onSubmit={handleAddDeck}>
                    <div>
                        <h1>Create new deck</h1>
                        <label>Name</label>
                        <Input value = {name} onChange = {handleNameChange}></Input>
                        <label>Description</label>
                        <Input value = {description} onChange = {handleDescriptionChange} type="textarea"></Input>
                        <label>Enter your Creator Id</label>
                        <Input value = {creatorId} onChange = {handleCreatorIdChange}></Input>
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
                    </div>
                </Form>
            </Col>
        </Row>
    );
}

export default CreateDeck