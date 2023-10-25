import { useEffect, useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, NavLink, Input, Label, Col, Container } from 'reactstrap';

const LoginComponent = ({ onLogin }) => {

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
            const data = await response.json();
            return data.result;
        } catch (e) {
            return null;
        }
    }

    const authenticateUser = async (email, password) => {
        console.log((new URL(window.location).pathname))
        const user = await getUserByEmail(email);

        if (user === null) {
            setAuthMessage("Incorrect email.");
            setAuthMessageColor("red");
        } else if (password === user.password) {
            setAuthMessage("Login successful.");
            setAuthMessageColor("green");
            sessionStorage.setItem('token', JSON.stringify(user));
            try {
                onLogin(user);
            } catch (e) {
                if ((new URL(window.location).pathname) == "/login") {
                    console.log("islogis");
                    navigate("/browse");
                }
            }


        } else {
            setAuthMessage("Incorrect password.");
        }
    }

    return (
        <Container fluid>
            <Col md="3">
                <div id="Login">
                    <h5>Login:</h5>
                    <Input
                        innerRef={refEmailInput}
                        type="text"
                        placeholder={"Email:"}
                        style={{
                            marginBottom: "10px"
                        }}
                    ></Input>

                    <Input
                        innerRef={refPasswordInput}
                        type="password"
                        placeholder={"Password:"}
                        style={{
                            marginBottom: "10px"
                        }}
                    ></Input>
                    <Button onClick={() => authenticateUser(refEmailInput.current.value, refPasswordInput.current.value)}>Login</Button> <br />
                    <h6 style={{ color: authMessageColor }}>{authMessage}</h6>
                </div>
            </Col>
        </Container>
    );


}
export default LoginComponent;