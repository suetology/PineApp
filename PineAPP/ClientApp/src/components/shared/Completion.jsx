import React, {useState} from 'react';
import {Button, Col, Container, Row} from "reactstrap";
import {Link} from "react-router-dom";
import Browse from "../Browse";

const Completion = () => {
    
    return (
        <Container>
            <Row className="justify-content-center">
                <Col lg={8} className="text-center">
                    <img src="/check.svg" alt="success"  style={{ width: '50%'}}/>
                    <p>All cards reviewed</p>
                    <Link className="btn btn-light shadow border-dark" to="/Browse" style={{width: '150pt'}}>Return</Link>
                </Col>
            </Row>
        </Container>
    );
}

export default Completion;