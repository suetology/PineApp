import React, { useState, useEffect  } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink, Modal, ModalHeader, ModalBody } from 'reactstrap';
import { Link } from 'react-router-dom';
import LoginStatusNavbar from './shared/LoginStatusNavbar';
import PomodoroTimer from './PomodoroTimer';
import SimplePomodoroTimer from './SimplePomodoroTimer';
import DURATIONS from './timerDurations';
import NavLinkLogin from "./shared/NavLinkLogin";

const NavMenu = () => {
    const [isCollapsed, setCollapsed] = useState(true);
    const [isTimerModalOpen, setTimerModalOpen] = useState(false);
    
    const [timerData, setTimerData] = useState({
        ...DURATIONS.Pomodoro,
        isActive: false
    });

    const toggleTimer = () => {
        setTimerData((prevState) => {
            if (prevState.isActive) {
                let newSeconds = prevState.seconds;
                let newMinutes = prevState.minutes;

                if (newSeconds === 0) {
                    if (newMinutes === 0) {
                        // todo should switch mode or stop
                        // todo timer logic here
                    } else {
                        newMinutes--;
                        newSeconds = 59;
                    }
                } else {
                    newSeconds--;
                }

                return {
                    ...prevState,
                    minutes: newMinutes,
                    seconds: newSeconds
                };
            } else {
                return { ...prevState, isActive: !prevState.isActive };
            }
        });
    };

    const resetTimer = (newData) => {
        console.log(`Resetting timer with data: ${JSON.stringify(newData)}`);
        setTimerData({
            ...newData,
            isActive: false
        });
    };

    useEffect(() => {
        let interval;

        if (timerData.isActive) {
            interval = setInterval(() => {
                toggleTimer();
            }, 1000);
        } else {
            clearInterval(interval);
        }
        return () => clearInterval(interval);
    }, [timerData.isActive]);
    
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
                  <NavLinkLogin link={"/browse"}>Browse</NavLinkLogin>
                </NavItem>
                <NavItem>
                  <NavLinkLogin link={"/create"}>Create</NavLinkLogin>
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