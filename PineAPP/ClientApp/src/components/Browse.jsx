import React, { Component } from 'react';
import {Container} from "reactstrap";
import DeckDisplay from "./shared/DeckDisplay";

export class Browse extends Component {
    static displayName = Browse.name;
    
    
    render() {
        
        return(
            <Container className="m-5">
                <div>
                    <h5 className="mb-4">Personal decks</h5>
                    <DeckDisplay/>
                </div>
                <div>
                    <h5 className="mb-4">Community decks</h5>
                    <DeckDisplay/>
                </div>
            </Container>
            
        );
    }

}