import { useNavigate } from "react-router-dom";
import { Button, NavLink } from "reactstrap";

const NavLinkLogin = ({link: link, style: style, children}) => {
    const navigate = useNavigate();

    if (!style) {
        style = {
            color: "black",
            backgroundColor: "transparent",
            border: 0,
            cursor: 'pointer'
        }
    }

    const navigateIfLogin = (originalLink) => {
        const token = JSON.parse(sessionStorage.getItem('token'));
        if (token) {
            navigate(originalLink);
        } else {
            navigate("/login");
        }
    };
    
    return (
        <NavLink style={style}  onClick={() => navigateIfLogin(link)}>{children}</NavLink>
    );
}
export default NavLinkLogin;