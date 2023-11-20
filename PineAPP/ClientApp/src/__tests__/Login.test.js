import React from 'react';
import { BrowserRouter } from 'react-router-dom';
import LoginPage from '../components/LoginPage';
import { fireEvent, getByTestId, render, screen, waitFor } from '@testing-library/react';
import '@testing-library/jest-dom'

global.fetch = jest.fn(() => Promise.resolve({ json: () => Promise.resolve({ ok: true }) }));

// Mock useNavigate
jest.mock('react-router-dom', () => ({
    ...jest.requireActual('react-router-dom'),
    useNavigate: jest.fn(),
}));

describe('LoginComponent', () => {

    it('renders without crashing', () => {
        const { getByPlaceholderText } = render(
            <BrowserRouter>
                <LoginPage />
            </BrowserRouter>
        );
        const emailInput = getByPlaceholderText('Email:');
        const passwordInput = getByPlaceholderText('Password:');

        expect(emailInput).toBeInTheDocument();
        expect(passwordInput).toBeInTheDocument();
    });

    it('calls navigate if already logged in', () => {
        const mockNavigate = jest.fn();
        require('react-router-dom').useNavigate.mockReturnValue(mockNavigate);

        sessionStorage.setItem('token', 'someToken');

        render(
            <BrowserRouter>
                <LoginPage />
            </BrowserRouter>
        );

        expect(mockNavigate).toHaveBeenCalledWith('/browse');
    });

    it('authenticates users', async () => {
        const mockNavigate = jest.fn();
        require('react-router-dom').useNavigate.mockReturnValue(mockNavigate);

        const { getByPlaceholderText, getByText } = render(
            <BrowserRouter>
                <LoginPage />
            </BrowserRouter>
        );

        const emailInput = getByPlaceholderText('Email:');
        const passwordInput = getByPlaceholderText('Password:');
        const loginButton = screen.getByTestId('login-button');

        // Mock API response for getUserByEmail
        global.fetch.mockResolvedValueOnce({
            json: jest.fn().mockResolvedValueOnce({ email: 'test@example.com', password: 'password' }),
        });

        fireEvent.change(emailInput, { target: { value: 'test@example.com' } });
        fireEvent.change(passwordInput, { target: { value: 'password' } });

        fireEvent.click(loginButton);

        await waitFor(() => {
            expect(sessionStorage.getItem('token')).not.toBeNull();
            expect(mockNavigate).toHaveBeenCalledWith('/browse');
        });
    });
});
