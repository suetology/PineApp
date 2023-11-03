import React, { useState } from 'react';
import { Button, Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link, useNavigate } from 'react-router-dom';
import NavLinkLogin from './shared/NavLinkLogin.jsx';
import LoginStatusNavbar from './shared/LoginStatusNavbar.jsx'

const NavMenu = () => {
    const [isCollapsed, setCollapsed] = useState(true);

    
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
                  <NavLinkLogin link={"/browse"}>Browse</NavLinkLogin>
                </NavItem>
                <NavItem>
                  <NavLinkLogin link={"/create"}>Create</NavLinkLogin>
                </NavItem>
              </ul >
              <ul className="navbar-nav ms-auto">
                <LoginStatusNavbar></LoginStatusNavbar>
              </ul>
            </Collapse>
          </Navbar>
        </header>
    );
}

export default NavMenu;
