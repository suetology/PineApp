import React, { Component } from 'react';
import {Col, Container, Row} from "reactstrap";

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
        <Container fluid>
            <Row>
                <Col md="4" className="bg-white rounded shadow-lg">
                    <div className="p-5">
                        <h2 className="fw-bold">PineAPP flashcards</h2>
                        <p>Enhance your learning journey with our dynamic and user-friendly platform.
                            Whether you're a student or professional, our flash cards are your key to efficient and enjoyable learning.
                            Start exploring and unlock your full potential today!</p>
                    </div>
                </Col>
                <Col md="8">
                    <img src="/home-bg.jpg" alt="Image" className="img-fluid image-container rounded" />
                </Col>
            </Row>
        </Container>
    );
  }
}
