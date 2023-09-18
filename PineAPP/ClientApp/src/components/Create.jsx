import React, {Component, useState} from 'react';
import {Button, Col, Container, Input, Row} from "reactstrap";
import InputBox from "./shared/InputBox";
import CardsDisplay from "./shared/CardsDisplay";

export class Create extends Component {
    static displayName = Create.name;
    
    render() {

        return(
            <div>
            <Row className="m-5">
                <Col sm={4}>
                    <InputBox initialValue="New name" type="text" className="h3"/>
                    <InputBox initialValue="description" type="textarea" className="border border-dark bg-light p-2 pr-5 pb-5"/>
                    <img src="/trash.svg" alt="delete"/>
                </Col>
                <Col sm={4}>
                    <Container className="border rounded border-dark bg-light p-2 mb-2">
                        <Row>
                            <Col className="text-center">
                                <h5 >3</h5>
                                <p>Correct</p>
                            </Col>
                            <Col className="text-center">
                                <h5>3</h5>
                                <p>Not Studied</p>
                            </Col>
                            <Col className="text-center">
                                <h5>3</h5>
                                <p>Wrong</p>
                            </Col>
                        </Row>
                    </Container>
                    <a href="/study" className="btn text-white w-100 " style={{backgroundColor:'#aed683', borderWidth: '0'}}>Study</a>
                </Col>
            </Row>
            
            <Col className="m-5">
                <p className="mb-2">Cards in deck:</p>
                <CardsDisplay/>
            </Col>
            </div>
        );
    }

}