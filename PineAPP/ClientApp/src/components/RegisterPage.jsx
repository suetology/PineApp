import React, { useEffect, useState } from 'react';
import { useRef } from "react";
import { Button, Col, Container, Input, Label, Row } from "reactstrap";
import './css/Register.css';
import RegisterComponent from './RegisterComponent';

const RegisterPage = () => {

    return (
        <div className="register-page">
            <div className="background-image"></div>
            <Row xs={3}>
                <Col></Col>
                <div className="transparent-column">
                    <RegisterComponent></RegisterComponent>
                </div>
            </Row>
        </div>
    );

}

export default RegisterPage;