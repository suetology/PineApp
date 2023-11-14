import React, { useState } from 'react';
import { useRef } from "react";
import { Button, Col, Container, Input, Label, Row } from "reactstrap";
import './css/Register.css';
import { useNavigate } from 'react-router-dom';



const RegisterComponent = () => {


    const url = "https://localhost:7074/";
    const refEmailInput = useRef(null);
    const refPasswordInput1 = useRef(null);
    const refPasswordInput2 = useRef(null);
    const refUserNameInput = useRef(null);
    const navigate = useNavigate();

    const isUser = (userObject) => {
        Object.hasOwn(userObject, 'userId') &&
            Object.hasOwn(userObject, 'userName') &&
            Object.hasOwn(userObject, 'email') &&
            Object.hasOwn(userObject, 'password')
    };

    // error messages
    const [errorUsername, setErrorUsername] = useState('');
    const [errorPassword, setErrorPassword] = useState('');
    const [errorEmail, setErrorEmail] = useState('');

    
    if (sessionStorage.getItem('token')) {
        return (
            <Label>You are already logged in</Label>
        );
    }

    const handleErrors = (errors) => {
        console.log(errors);
        setErrorUsername(errors.UserName);
        setErrorPassword(errors.Password);
        setErrorEmail(errors.Email);

    }


    const handleSubmit = async () => {

        const createUserDto = {
            Email: refEmailInput.current.value,
            Password: refPasswordInput1.current.value,
            UserName: refUserNameInput.current.value,
        };
        try {
            const response = await postUser(createUserDto);
            // console.log(response);
            if (isUser(response)) {
                console.log("User created!");
                navigate('/browse');
            } else {
                handleErrors(response.errors);
            }

        } catch (err) {
            console.log(err.message);
        }
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
                <h6 className='input-error-message'>{errorUsername}</h6>

                <Input className='input-field' placeholder='Password:' innerRef={refPasswordInput1} type='password'></Input>
                <Input className='input-field' placeholder='Repeat password:' innerRef={refPasswordInput2} type='password'></Input>
                <h6 className='input-error-message'>{errorPassword}</h6>

                <Input className='input-field' placeholder='Email:' innerRef={refEmailInput}></Input>
                <h6 className='input-error-message'>{errorEmail}</h6>

                <Button className='yellow-button' onClick={() => handleSubmit()}>Sign Up!</Button>
            </div>
        </Container>
    );

}

export default RegisterComponent;