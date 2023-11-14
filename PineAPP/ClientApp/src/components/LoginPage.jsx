import { Col, Row } from "reactstrap";
import LoginComponent from "./LoginComponent";

const LoginPage = () => {

    return (
        <div className="register-page">
            <div className="background-image-login"></div>
            <Row xs={3}>
                <Col></Col>
                <div className="transparent-column">
                    <LoginComponent></LoginComponent>
                </div>
            </Row>
        </div>
    );
}

export default LoginPage;
