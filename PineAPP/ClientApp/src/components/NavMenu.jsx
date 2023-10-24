import React, { useState } from 'react';
import { Button, Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link, useNavigate } from 'react-router-dom';

const NavMenu = () => {
    const navigate = useNavigate();
    const [isCollapsed, setCollapsed] = useState(true);

    const handleLogOut = () => {
      sessionStorage.clear();
      navigate("/")
    }
    const handleLogIn = () => {
      navigate("/login")
    }
    
    return (
        <header>
          <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom bg-white">
            <NavbarBrand tag={Link} to="/">
              <img src="/favicon.ico" alt="PineApp" style={{height: '40px'}}/>
            </NavbarBrand>
            <NavbarToggler onClick={() => setCollapsed(!isCollapsed)} className="mr-2" />
            <Collapse isOpen={!isCollapsed} navbar>
              <ul className="navbar-nav">
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/browse">Browse</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/create">Create</NavLink>
                </NavItem>
              </ul>
              <ul className="navbar-nav ms-auto">
                <NavItem>
                  <Button onClick={() => (sessionStorage.getItem('token') ? true : handleLogIn() )}>Log In</Button>
                </NavItem>
                <NavItem>
                  <Button onClick={() => (sessionStorage.getItem('token') ? handleLogOut() : true )}>Log Out</Button>
                </NavItem>
              </ul>
            </Collapse>
          </Navbar>
        </header>
    );
}

export default NavMenu;
