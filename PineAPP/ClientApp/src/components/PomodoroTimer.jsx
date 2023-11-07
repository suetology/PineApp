import React, { useState, useEffect } from 'react';
import './css/PomodoroTimer.css';
import Tasks from './Tasks';

const PomodoroTimer = ({ timerData, toggleTimer, resetTimer }) => {
    const [mode, setMode] = useState('Pomodoro');
    const [tasks, setTasks] = useState([]);
    const { minutes, seconds, isActive } = timerData;
    const [newTask, setNewTask] = useState('');

    useEffect(() => {
        let interval;

        if (isActive && minutes === 0 && seconds === 0) {
            if (mode === 'Pomodoro') {
                setMode('Short Break');
                resetTimer({ minutes: 5, seconds: 0 });
            } else {
                setMode('Pomodoro');
                resetTimer({ minutes: 25, seconds: 0 });
            }
            
            // TODO notifications
            
        } else if (isActive) {
            interval = setInterval(() => {
            }, 1000);
        } else {
            clearInterval(interval);
        }

        return () => clearInterval(interval);
    }, [isActive, minutes, seconds, mode]);

    const changeMode = (newMode) => {
        setMode(newMode);
    };

    return (
        <div className="pomodoro-container">
            <div className="mode-selector">
                <button onClick={() => changeMode('Pomodoro')} className="mode-button">Pomodoro</button>
                <button onClick={() => changeMode('Short Break')} className="mode-button">Short Break</button>
                <button onClick={() => changeMode('Long Break')} className="mode-button">Long Break</button>
            </div>
            <div className="pomodoro-header">
                <div className="timer">
                    {isActive ? 'Time to focus!' : `${minutes}:${seconds < 10 ? `0${seconds}` : seconds}`}
                </div>
                <div className="buttons">
                    <button onClick={toggleTimer} className="start-button">
                        {isActive ? 'Pause' : 'Start'}
                    </button>
                    <button onClick={() => resetTimer({ minutes: 25, seconds: 0, isActive: false })} className="reset-button">
                        Reset
                    </button>
                </div>
            </div>
                <Tasks tasks={tasks} setTasks={setTasks} />
        </div>
    );
};

export default PomodoroTimer;