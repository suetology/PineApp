import React, { useState, useEffect  } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink, Modal, ModalHeader, ModalBody } from 'reactstrap';
import { Link } from 'react-router-dom';
import LoginStatusNavbar from './shared/LoginStatusNavbar';
import PomodoroTimer from './PomodoroTimer';
import SimplePomodoroTimer from './SimplePomodoroTimer';

const NavMenu = () => {
    const [isCollapsed, setCollapsed] = useState(true);
    const [isTimerModalOpen, setTimerModalOpen] = useState(false);
    
    const [timerData, setTimerData] = useState({
        isActive: false,
        minutes: 25,
        seconds: 0,
    });
    
    const toggleTimer = () => {
        setTimerData((prevState) => ({
            ...prevState,
            isActive: !prevState.isActive,
        }));
    };

    const resetTimer = (newData) => {
        setTimerData((prevData) => ({
            ...prevData,
            ...newData,
        }));
    };

    useEffect(() => {
        let interval;

        if (timerData.isActive && timerData.minutes === 0 && timerData.seconds === 0) {
            // TODO Timer has reached 0, add what happens when it's done
        } else if (timerData.isActive) {
            interval = setInterval(() => {
                if (timerData.seconds === 0) {
                    if (timerData.minutes === 0) {
                        
                        clearInterval(interval);
                        
                    } else {
                        resetTimer({ minutes: timerData.minutes - 1, seconds: 59 });
                    }
                } else {
                    resetTimer({ seconds: timerData.seconds - 1 });
                }
            }, 1000);
        } else {
            clearInterval(interval);
        }

        return () => clearInterval(interval);
    }, [timerData]);

    const toggleTimerModal = () => {
        setTimerModalOpen(!isTimerModalOpen);
    };


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
              </ul >
            </Collapse>
              <ul className="navbar-nav ms-auto">
                    <NavItem>
                        <NavLink
                            className="text-dark"
                            style={{ cursor: 'pointer' }}
                            onClick={toggleTimerModal}
                        >
                            <img src="/timer.png" alt="Pomodoro Icon" style={{ height: '40px' }} />
                            <SimplePomodoroTimer timerData={timerData} />
                        </NavLink>
                    </NavItem>
                <LoginStatusNavbar></LoginStatusNavbar>
              </ul>
            <Modal isOpen={isTimerModalOpen} toggle={toggleTimerModal}>
                    <ModalHeader toggle={toggleTimerModal}>Pomodoro Timer</ModalHeader>
                    <ModalBody>
                        <PomodoroTimer timerData={timerData} toggleTimer={toggleTimer} resetTimer={resetTimer} />
                    </ModalBody>
                </Modal>
          </Navbar>
        </header>
    );
};

export default NavMenu;