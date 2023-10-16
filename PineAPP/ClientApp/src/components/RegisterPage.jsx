import {useEffect, useRef, useState} from "react";
import {useGetUserByEmailQuery} from "../api/usersApi";
import {Input} from "reactstrap";
import {useNavigate} from "react-router-dom";

const RegisterPage = () => {
    const navigate = useNavigate();
    
    const serverUrl = "https://localhost:7074/";
    const refEmailInput = useRef(null);
    const refPasswordInput = useRef(null);

    const [authMessage, setAuthMessage] = useState('_');
    
    
    const getUserByEmail = async () => {
        try {
            const response = await fetch(serverUrl + `api/Users/GetUserByEmail/${refEmailInput.current.value}`);
            const data = await response.json();
            return data.result;
        } catch (e) {
            // Fetch called with empty string
            return null;
        }
    }
    
    const authenticateUser = async () => {
        const user = await getUserByEmail();
        
        if (user === null){
            setAuthMessage("Incorrect email.");
        } else if(refPasswordInput.current.value === user.password){
            setAuthMessage("Login successful.");
            navigate("/browse", {state: user});
        } else {
            setAuthMessage("Incorrect email or password.");
        }
    }
    
    return (
        <div id="Login">
            <p>Login:</p>
            <input
                ref={refEmailInput}
                type="text"
                placeholder={"Email:"}
                style={{
                marginBottom: "10px"
            }}
            ></input><br/>

            <input
                ref={refPasswordInput}
                type="password"
                placeholder={"Password:"}
                style={{
                    marginBottom: "10px"
                }}
            ></input><br/>
            <button onClick={authenticateUser}>Login</button> <br/> 
            <h1>{ authMessage }</h1>
        </div>
    );
    
    
}
export default RegisterPage;