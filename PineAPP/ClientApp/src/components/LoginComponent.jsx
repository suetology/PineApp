import { useEffect, useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, NavLink, Input, Label, Col, Container } from 'reactstrap';
import { useGetUserByEmailQuery } from "../api/usersApi";

const LoginComponent = () => {

    const url = "https://localhost:7074/";
    const refEmailInput = useRef(null);
    const refPasswordInput = useRef(null);

    const navigate = useNavigate();

    const [authMessage, setAuthMessage] = useState('');
    const [authMessageColor, setAuthMessageColor] = useState("Red");

    // Navigate to "/browse if already logged in"
    useEffect(() => {
        if (
            sessionStorage.getItem('token')) {
            navigate("/browse");
        }
    }, []);


    const getUserByEmail = async (email) => {
        try {
            const response = await fetch(url + `api/Users/GetUserByEmail/${email}`);
            return await response.json();
        } catch (e) {
            return null;
        }
    }

    const authenticateUser = async (email, password, a) => {
        const user = await getUserByEmail(email);

        if (user === null) {
            setAuthMessage("Incorrect email.");
            setAuthMessageColor("red");
        } else if (password === user.password) {
            setAuthMessage("Login successful.");
            setAuthMessageColor("green");
            sessionStorage.setItem('token', JSON.stringify(user));
            // Navigate to "/browse" when login
            navigate("/browse");
        } else {
            setAuthMessage("Incorrect password.");
        }
    }

    return (
        <Container fluid>
            <h1 style={{textAlign: "center", margin: 20}}>Login</h1>
            <div id="Login">
                <Label>Email:</Label>
                <Input
                    innerRef={refEmailInput}
                    type="text"
                    placeholder={"Email:"}
                    style={{
                        marginBottom: "10px"
                    }}
                ></Input>

                <Label>Password:</Label>
                <Input
                    innerRef={refPasswordInput}
                    type="password"
                    placeholder={"Password:"}
                    style={{
                        marginBottom: "10px"
                    }}
                ></Input>
                <h6 style={{ color: authMessageColor }}>{authMessage}</h6>
                <Button 
                    className="yellow-button"
                    onClick={() => authenticateUser(refEmailInput.current.value, refPasswordInput.current.value, refPasswordInput)}>
                Login</Button> <br />
            </div>
        </Container>
    );


}
export default LoginComponent;