import React, { useState } from 'react';
import { useRef } from "react";
import { Button, Col, Container, Input, Label, Row } from "reactstrap";
import './css/Register.css';
import { useNavigate, Link } from 'react-router-dom';



const RegisterComponent = () => {


    const url = "https://localhost:7074/";
    const refEmailInput = useRef(null);
    const refPasswordInput1 = useRef(null);
    const refPasswordInput2 = useRef(null);
    const refUserNameInput = useRef(null);
    const navigate = useNavigate();

    const isUser = (userObject) => {
        return (
            Object.hasOwn(userObject, 'userId') &&
            Object.hasOwn(userObject, 'userName') &&
            Object.hasOwn(userObject, 'email') &&
            Object.hasOwn(userObject, 'password')
        );
    };

    // error messages
    const [errorUsername, setErrorUsername] = useState('');
    const [errorPassword, setErrorPassword] = useState('');
    const [errorEmail, setErrorEmail] = useState('');
    const [errorGeneral, setErrorGeneral] = useState('');


    if (sessionStorage.getItem('token')) {
        return (
            <Label>You are already logged in</Label>
        );
    }

    const handleErrors = (errors) => {
        if (!errors) {
            setErrorUsername('');
            setErrorPassword('');
            setErrorEmail('');
            return;
        }

        console.log(errors);
        setErrorUsername(errors.UserName);
        setErrorPassword(errors.Password);
        setErrorEmail(errors.Email);

    }


    const handleSubmit = async () => {

        if (refPasswordInput1.current.value !== refPasswordInput2.current.value) {
            setErrorPassword('Passwords must match.');
            setErrorGeneral('');
            return;
        }

        const createUserDto = {
            Email: refEmailInput.current.value,
            Password: refPasswordInput1.current.value,
            UserName: refUserNameInput.current.value,
        };

        try {
            const user = await postUser(createUserDto);
            if (isUser(user)) {
                console.log("User created!");
                sessionStorage.setItem('token', JSON.stringify(user));
                navigate('/browse');
            } else {
                handleErrors(user.errors);
                setErrorGeneral('');
            }

        } catch (err) {
            // Need to implement:
            console.log(err.message);
            handleErrors();
            setErrorGeneral('Incorrect input.');
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
                <h1 style={{ textAlign: "center" }}>Welcome to PineAPP</h1>
                <h2 style={{ marginBottom: 20 }}>An online community dedicated to learning and helping others learn</h2>

                <Label>Username:</Label>
                <Input className='input-field' placeholder='Username:' innerRef={refUserNameInput}></Input>
                <h6 className='input-error-message'>{errorUsername}</h6>

                <Label>Password:</Label>
                <Input className='input-field' placeholder='Password:' innerRef={refPasswordInput1} type='password'></Input>
                <Label>Repeat Password:</Label>
                <Input className='input-field' placeholder='Repeat password:' innerRef={refPasswordInput2} type='password'></Input>
                <h6 className='input-error-message'>{errorPassword}</h6>

                <Label>Email:</Label>
                <Input className='input-field' placeholder='Email:' innerRef={refEmailInput}></Input>
                <h6 className='input-error-message'>{errorEmail}</h6>

                <Button className='yellow-button' onClick={() => handleSubmit()}>Sign Up!</Button>

                <h6 className='input-error-message'>{errorGeneral}</h6>


                <Label>Already have an account?</Label><br/>
                <Label style={{marginRight: 10}}>Log in</Label>
                <Button tag={Link} to="/login" className='yellow-button'>Here</Button>
            </div>
        </Container>
    );

}

export default RegisterComponent;