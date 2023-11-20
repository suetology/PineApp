import React from 'react';
import { render, screen, fireEvent, cleanup } from '@testing-library/react';
import RegisterComponent from './../components/RegisterComponent';
import RegisterPage from './../components/RegisterPage';
import { BrowserRouter, useNavigate } from 'react-router-dom';
import { act } from 'react-dom/test-utils';
import '@testing-library/jest-dom'

// Mock fetch 
global.fetch = jest.fn(() => Promise.resolve({ json: () => Promise.resolve({ ok: true }) }));

// Mock useNavigate
jest.mock('react-router-dom', () => ({
    ...jest.requireActual('react-router-dom'),
    useNavigate: jest.fn(),
}));

beforeEach(() => {
    cleanup();
    jest.clearAllMocks();
    sessionStorage.clear();
});

// Tests
describe('Register', () => {
    it('renders without crashing', () => {
        render(
            <BrowserRouter>
                <RegisterPage />
            </BrowserRouter>
        );
        expect(screen.getByText('Welcome to PineAPP')).toBeInTheDocument();
    });

    it('handles form submission correctly', async () => {
        render(
            <BrowserRouter>
                <RegisterPage />
            </BrowserRouter>
        );

        // Mock user input
        const usernameInput = screen.getByPlaceholderText('Username:');
        const passwordInput1 = screen.getByPlaceholderText('Password:');
        const passwordInput2 = screen.getByPlaceholderText('Repeat password:');
        const emailInput = screen.getByPlaceholderText('Email:');

        fireEvent.change(usernameInput, { target: { value: 'testUser' } });
        fireEvent.change(passwordInput1, { target: { value: 'testPassword' } });
        fireEvent.change(passwordInput2, { target: { value: 'testPassword' } });
        fireEvent.change(emailInput, { target: { value: 'test@example.com' } });

        // Mock fetch as ok
        fetch.mockResolvedValueOnce({ json: () => Promise.resolve({ ok: true }) });

        // Trigger form submission
        fireEvent.click(screen.getByText('Sign Up!'));

        expect(await screen.findByText('You are already logged in')).toBeInTheDocument();

        await screen.findByText('You are already logged in');
        // Assertions
        expect(fetch).toHaveBeenCalledWith('https://localhost:7074/api/Users/Add', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                Email: 'test@example.com',
                Password: 'testPassword',
                UserName: 'testUser',
            }),
        });

        expect(useNavigate).toHaveBeenCalledTimes(2);
        expect(sessionStorage.getItem('token')).not.toBeNull();

        // You can add more assertions based on your component's behavior
    });

    it('handles empty input', async () => {
        fetch.mockResolvedValueOnce({
            json: () => Promise.resolve(
                {
                    status: 400,
                    errors: {
                        Email: ["The Email field is required."],
                        Password: ["The Password field is required."],
                        UserName: ["The UserName field is required."],
                    }
                }

            ),
        });

        render(
            <BrowserRouter>
                <RegisterComponent />
            </BrowserRouter>
        );

        act(() => fireEvent.click(screen.getByText('Sign Up!')));

        await screen.findByText("The Email field is required.");
        await screen.findByText("The UserName field is required.");
        await screen.findByText("The Password field is required.");

        const h6Elements = screen.getAllByRole('heading', { level: 6 });

        expect(h6Elements).toHaveLength(4);
        expect(screen.queryByText("The Email field is required.")).toBeInTheDocument();
        expect(screen.queryByText("The UserName field is required.")).toBeInTheDocument();
        expect(screen.queryByText("The Password field is required.")).toBeInTheDocument();
    });

    it('handles incorrect input', async () => {
        fetch.mockResolvedValueOnce({
            json: () => {throw new Error("JSON.parse: unexpected character at line 1 column 1 of the JSON data");}
        });

        render(
            <BrowserRouter>
                <RegisterComponent />
            </BrowserRouter>
        );

        act(() => fireEvent.click(screen.getByText('Sign Up!')));

        await screen.findByText("Incorrect input.");

        expect(screen.queryByText("Incorrect input.")).toBeInTheDocument();
    });
});
