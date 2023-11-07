import React, { useState } from 'react';

const Tasks = ({ tasks, setTasks }) => {
    const [newTask, setNewTask] = useState('');

    const handleAddTask = () => {
        if (newTask.trim() !== '') {
            setTasks([...tasks, newTask]);
            setNewTask('');
        }
    };

    const handleRemoveTask = (taskToRemove) => {
        const updatedTasks = tasks.filter((task) => task !== taskToRemove);
        setTasks(updatedTasks);
    };

    const handleTaskChange = (e) => {
        setNewTask(e.target.value);
    };

    return (
        <div className="tasks">
            <input
                type="text"
                value={newTask}
                onChange={handleTaskChange}
                placeholder="Enter a new task"
                className="task-input"
            />
            <button onClick={handleAddTask} className="add-task-button">Add Task</button>
            <ul className="task-list">
                {tasks.map((task, index) => (
                    <li key={index} className="task-item">
                        {task}
                        <button onClick={() => handleRemoveTask(task)} className="remove-task-button">X</button>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default Tasks;
