import React from 'react';

const SimplePomodoroTimer = ({ timerData }) => {
    const { minutes, seconds } = timerData;

    return (
        <div className="simple-timer">
            {minutes}:{seconds < 10 ? `0${seconds}` : seconds}
        </div>
    );
};

export default SimplePomodoroTimer;