import React, { useState } from 'react';
import { Button, Collapse, Label, Nav, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link, useNavigate } from 'react-router-dom';


const LoginStatusNavbar = () => {
    
    const navigate = useNavigate();
    const token = JSON.parse(sessionStorage.getItem('token'));

    const style1 = {
        marginRight: 10,
        color: "black",
        backgroundColor: "transparent",
        border: 0

    }

    const style_LogOut = {
        marginRight: 10,
        color: "black",
        backgroundColor: "#e0e0de",
        border: 0
    }
    
    const style_logIn = {
        marginRight: 10,
        color: "white",
        backgroundColor: "#f0b802",
        border: 0
    }
    

    const navbarIfLoggedIn = () => {
      return (
        <ul className="navbar-nav">
          <NavItem>
            <Button style={ style1 } tag={ Link } to="/account" >{ token.userName }</Button>
          </NavItem>
          <NavItem>
            <Button style={ style_LogOut } onClick={() => (token ? handleLogOut() : true)}>Log Out</Button>
          </NavItem>
        </ul>
      );
    }

    const navbarIfNotLoggegIn = () => {
      return (
        <ul className="navbar-nav">
          <NavItem>
            <Button style={ style1 } tag={Link} to="/register" >Sign Up</Button>
          </NavItem>
          <NavItem>
            <Button style={ style_logIn }onClick={() => (token ? true : handleLogIn() )}>Log In</Button>
          </NavItem>
        </ul>
      );
    }

    const handleLogOut = () => {
      sessionStorage.clear();
      navigate("/")
    }
    const handleLogIn = () => {
      navigate("/login")
    }

    return (
        token ? navbarIfLoggedIn() : navbarIfNotLoggegIn()
    )

}

export default LoginStatusNavbar;