import React, { useState, useEffect } from 'react';
import './css/PomodoroTimer.css';
import Tasks from './Tasks';
import DURATIONS from './timerDurations';

const PomodoroTimer = ({ timerData, toggleTimer, resetTimer }) => {
    const [mode, setMode] = useState('Pomodoro');
    const [tasks, setTasks] = useState([]);
    const { minutes, seconds, isActive } = timerData;
    const [newTask, setNewTask] = useState('');

    useEffect(() => {
        let interval;

        if (isActive) {
            if (minutes === 0 && seconds === 0) {
                let nextMode = 'Pomodoro';
                switch (mode) {
                    case 'Pomodoro':
                        nextMode = 'Short Break';
                        break;
                    case 'Short Break':
                        nextMode = 'Long Break';
                        break;
                    case 'Long Break':
                        nextMode = 'Pomodoro';
                        break;
                }
                setMode(nextMode);
                resetTimer(DURATIONS[nextMode]);
                // TODO: Implement notifications
            } else {
                interval = setInterval(() => {
                    toggleTimer();
                }, 1000);
            }
        } else {
            clearInterval(interval);
        }

        return () => clearInterval(interval);
    }, [isActive, minutes, seconds, mode, resetTimer, toggleTimer]);

    const changeMode = (newMode) => {
        const modeKey = newMode.replace(' ', '');
        const newDuration = DURATIONS[modeKey];

        if (newDuration) {
            setMode(newMode);
            resetTimer({
                minutes: newDuration.minutes,
                seconds: newDuration.seconds,
                isActive: false
            });
        } else {
            console.error(`Invalid mode: ${newMode}`);
        }
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
                    {
                        isActive
                            ? (mode === 'Pomodoro' ? 'Time to focus!' : 'Time for a break!')
                            : `${minutes}:${seconds < 10 ? `0${seconds}` : seconds}`
                    }
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