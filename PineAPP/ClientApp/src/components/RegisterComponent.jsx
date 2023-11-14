import React from 'react';
import { useRef } from "react";
import { Button, Col, Container, Input, Label, Row } from "reactstrap";
import './css/Register.css';


const RegisterComponent = () => {

    const url = "https://localhost:7074/";
    const refEmailInput = useRef(null);
    const refPasswordInput1 = useRef(null);
    const refPasswordInput2 = useRef(null);
    const refUserNameInput = useRef(null);

    const handleSubmit = async () => {
        
        const createUserDto = {
            Email: refEmailInput.current.value,
            Password: refPasswordInput1.current.value,
            UserName: refUserNameInput.current.value,
        };
        const response = await postUser(createUserDto);
        console.log(response);

    }

    const postUser = async (createUserDto) => {
        const response = await fetch(url + "api/Users/Add",
            {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json', // You may need to include additional headers like authorization if required
            },
            body: JSON.stringify(createUserDto)
        });
        
        return await response.json();
    }


    return (
        <Container>
            <div>
                <h1>Welcome to PineAPP</h1>
                <h2>An online community dedicated to learning and helping others learn</h2>

                <Input className='input-field' placeholder='Username:' innerRef={refUserNameInput}></Input>
                <Input className='input-field' placeholder='Password:' innerRef={refPasswordInput1} type='password'></Input>
                <Input className='input-field' placeholder='Repeat password:' innerRef={refPasswordInput2} type='password'></Input>
                <Input className='input-field' placeholder='Email:' innerRef={refEmailInput}></Input>

                <Button className='yellow-button' onClick={() => handleSubmit()}>Sign Up!</Button>
            </div>
        </Container>
    );

}

export default RegisterComponent;