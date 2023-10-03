import React from 'react';
import {Button, Input, Label, Col, FormGroup, Form} from 'reactstrap';
import { useState } from 'react';
import {useGetDeckByIdQuery, useAddDeckMutation} from '../api/decksApi';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import {useGetAllDecksQuery} from '../api/decksApi';

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
        <div>
            <Form onSubmit={handleAddDeck}>
            <div>
                <label>Name</label>
                <Input value = {name} onChange = {handleNameChange}></Input>
                <label>Description</label>
                <Input value = {description} onChange = {handleDescriptionChange}></Input>
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
        </div>
    );
}

export default CreateDeck

/*
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
            const response = await addDeck(JSON.stringify(requestData), {
              headers: {
                'Content-Type': 'application/json', // Set the content type to JSON
              },
            });
            
            console.log("No error: " , response);
          } catch (error) {
            console.error("Error adding deck:", error);
            // Handle errors here
          }
    }
*/


    //   const [addDecker] = useAddDeckMutation();
    
    //   const handleAddDeck = async (e) => {
    //       e.preventDefault();
  
    //       const intCreatorId = +creatorId;
  
    //       console.log(name, description, intCreatorId, isPersonal);
      
    //       const requestData =  {
    //           Name : name,
    //           IsPersonal : isPersonal,
    //           CreatorId : intCreatorId,
    //           Description : description,
    //        };
  
    //        try {
    //         const response = await addDecker(requestData, {
    //           headers: {
    //             'Content-Type': 'application/json', // Set the content type to JSON
    //           },
    //         });
              
    //           console.log("No error: " , response);
    //         } catch (error) {
    //           console.error("Error adding deck:", error);
    //           // Handle errors here
    //         }
    //   }

        /*
      const handleSubmit = async (e) => {
        e.preventDefault();

        const intCreatorId = +creatorId;

        try {
        const requestData =  {
            Name : name,
            IsPersonal : isPersonal,
            CreatorId : intCreatorId,
            Description : description,
        };
          console.log("It works")
          const response = await addDeck(requestData);
          console.log(response);
          // Handle a successful response, e.g., show a success message
            navigate('/browse');
        } catch (error) {
          //Handle any errors here
          console.error('Error adding deck fart:', error);
        }
      };

        async function addDeck(deckData) {
        try {
          const response = await axiosInstance.post('api/Decks/Add', deckData);
          return response.data;
        } catch (error) {
          // Handle error here
          console.error('Error adding deck:', error);
          throw error;
        }
      }
      */