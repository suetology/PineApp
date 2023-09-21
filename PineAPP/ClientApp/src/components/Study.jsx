import React, {Component, useState} from 'react';
import {Button, Col, Container, Input, Row} from "reactstrap";
import './css/Study.css';

const Study = () => {
    const [isFlipped, setFlipped] = useState(false);
    const [isEditing, setEditing] = useState(false);
    const [card, setCard] = useState({
        front: 'Front side',
        back: 'Back side',
        examples: 'Example'
    });

    const handleCardClick = () => {
        setFlipped(true);
    };

    const handleEditClick = () => {
        setEditing(true);
    };

    const handleFrontChange = (newValue) => {
        const newCard = {...this.state.card, front: newValue};
        setCard(newCard);
    }

    const handleBackChange = (newValue) => {
        const newCard = {...this.state.card, back: newValue};
        setCard(newCard);
    }

    const handleExamplesChange = (newValue) => {
        const newCard = {...this.state.card, examples: newValue};
        setCard(newCard);
    }

    const handleSave = () => {
        setEditing(false);
    }

    let editableContent = isEditing
        ? <div>
            <Input className="fw-bold mb-2" value={card.front} onChange={
                (e) => handleFrontChange(e.target.value)
            }/>
            <Input value={card.back} type="textarea" onChange={
                (e) => handleBackChange(e.target.value)
            }/>
            <Input value={card.examples} type="textarea" onChange={
                (e) => handleExamplesChange(e.target.value)
            }/>
            <Button onClick={handleSave}>Save</Button>
        </div>
        : <div>
            <h2 className="fw-bold mb-2">{card.front}</h2>
            <p>{card.back}</p>
            <p>{card.examples}</p>
        </div>;

    let content = isFlipped
        ? <div>
            {editableContent}
        </div>
        : <div>
            <h2 className="fw-bold mb-2">{card.front}</h2>
            <p>{card.back}</p>
        </div>;

    let buttons = isFlipped
        ? <div className="mt-5">
            <Button className="m-1 btn-success shadow">Correct</Button>
            <Button className="btn-danger shadow">Wrong</Button>
        </div>
        : null;
    
    return (
        <Row className="h-75 justify-content-center align-items-center pt-2">
            <Col
                id="card"
                className={"h-100 bg-white border rounded shadow p-4 text-center"}
                md={8}
                sm={10}
                onClick={handleCardClick}
            >
                <Row className="h-100">
                    <Col xs={1}>
                        <img className="btn" src="/arrow-90deg-left.svg" alt="back" />
                    </Col>
                    <Col xs={10} className="d-flex align-items-center justify-content-center">
                        <div>
                            {content}
                        </div>
                    </Col>
                    <Col xs={1}>
                        <img className="bg-white btn" src="/pencil.svg" alt="edit" onClick={handleEditClick}/>
                    </Col>
                </Row>
                {buttons}
            </Col>
        </Row>
    );
}

export default Study;
/*export class Study extends Component {
    static displayName = Study.name;
    
    constructor(props) {
        super(props);
        this.state = {
            isFlipped: false,
            isEditing: false,

            //temp values
            card: {
                front: 'Front side',
                back: 'Back side',
                examples: 'Example'
            }
        };
    }

    handleCardClick = () => {
        this.setState({ isFlipped: true });
    };
    
    handleEditClick = () => {
      this.setState({isEditing: true})  
    };
    
    handleFrontChange = (e) => {
        const newCard = {...this.state.card, front: e}
        this.setState({card: newCard})
    }

    handleBackChange = (e) => {
        const newCard = {...this.state.card, back: e}
        this.setState({card: newCard})
    }

    handleExamplesChange = (e) => {
        const newCard = {...this.state.card, examples: e}
        this.setState({card: newCard})
    }
    
    handleSave = () => {
        this.setState({isEditing: false})
    }

    render() {

        let editableContent = this.state.isEditing
            ? <div>
                <Input className="fw-bold mb-2" value={this.state.card.front} onChange={
                    (e) => this.handleFrontChange(e.target.value)
                }/>
                <Input value={this.state.card.back} type="textarea" onChange={
                    (e) => this.handleBackChange(e.target.value)
                }/>
                <Input value={this.state.card.examples} type="textarea" onChange={
                    (e) => this.handleExamplesChange(e.target.value)
                }/>
                <Button onClick={this.handleSave}>Save</Button>
            </div>
            : <div>
                <h2 className="fw-bold mb-2">{this.state.card.front}</h2>
                <p>{this.state.card.back}</p>
                <p>{this.state.card.examples}</p>
            </div>
        
        let content = this.state.isFlipped
            ? <div>
                {editableContent}
              </div>
            : <div>
                <h2 className="fw-bold mb-2">{this.state.card.front}</h2>
                <p>{this.state.card.back}</p>
              </div>
        
        let buttons = this.state.isFlipped
            ? <div className="mt-5">
                <Button className="m-1 btn-success shadow">Correct</Button>
                <Button className="btn-danger shadow">Wrong</Button>
              </div>
            : null

        return (
            <Row className="h-75 justify-content-center align-items-center pt-2">
                <Col
                    id="card"
                    className={"h-100 bg-white border rounded shadow p-4 text-center"}
                    md={8}
                    sm={10}
                    onClick={this.handleCardClick}
                >
                    <Row className="h-100">
                        <Col xs={1}>
                            <img className="btn" src="/arrow-90deg-left.svg" alt="back" />
                        </Col>
                        <Col xs={10} className="d-flex align-items-center justify-content-center">
                            <div>
                                {content}
                            </div>
                        </Col>
                        <Col xs={1}>
                            <img className="bg-white btn" src="/pencil.svg" alt="edit" onClick={this.handleEditClick}/>
                        </Col>
                    </Row>
                    {buttons}
                </Col>
            </Row>
        );
    }
}
*/
