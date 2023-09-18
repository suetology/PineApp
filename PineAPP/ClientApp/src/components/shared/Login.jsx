import React, { useState } from 'react';
import {Button, Modal, ModalHeader, ModalBody, ModalFooter, NavLink, Input, Label} from 'reactstrap';
import {Link} from "react-router-dom";
import Config from "bootstrap/js/src/util/config";

function Login(args) {
    const [modal, setModal] = useState(false);

    const toggle = () => setModal(!modal);
    
    const signin = () => {return 0;}

    return (
        <div>
            <NavLink tag={Link} className="text-dark fw-bold" onClick={toggle}>Sign up</NavLink>
            <Modal isOpen={modal} toggle={toggle} {...args}>
                <ModalHeader toggle={toggle}>Sign up</ModalHeader>
                <ModalBody>
                    <Label for="user">Username:</Label>
                    <Input id="user" placeholder="user"/>
                    <Label for="pass">Password:</Label>
                    <Input id="pass" type="password" placeholder="password123"/>
                </ModalBody>
                <ModalFooter>
                    <Button className="text-dark bg-white border-0">Log in</Button>
                    <Button onClick={signin}>Sign up</Button>
                </ModalFooter>
            </Modal>
        </div>
    );
}

export default Login;