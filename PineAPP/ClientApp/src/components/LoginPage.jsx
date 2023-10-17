import {useRef, useState} from "react";
import {useNavigate} from "react-router-dom";

const RegisterPage = () => {
    const navigate = useNavigate();
    
    const url = "https://localhost:7074/";
    const refEmailInput = useRef(null);
    const refPasswordInput = useRef(null);

    const [authMessage, setAuthMessage] = useState('_');
    
    
    const getUserByEmail = async (email) => {
        try {
            const response = await fetch(url + `api/Users/GetUserByEmail/${email}`);
            const data = await response.json();
            return data.result;
        } catch (e) {
            // Fetch called with empty string
            return null;
        }
    }
    
    const authenticateUser = async (email, password) => {
        const user = await getUserByEmail(email);
        
        if (user === null){
            setAuthMessage("Incorrect email.");
        } else if(password === user.password){
            setAuthMessage("Login successful.");
            navigate("/browse", {state: user});
        } else {
            setAuthMessage("Incorrect password.");
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
            <button onClick={() => authenticateUser(refEmailInput.current.value, refPasswordInput.current.value)}>Login</button> <br/> 
            <h6>{ authMessage }</h6>
        </div>
    );
    
    
}
export default RegisterPage;