import React from 'react';
import { render, fireEvent, waitFor, screen } from '@testing-library/react';
import '@testing-library/jest-dom'
import CreateDeck from '../components/CreateDeck';
import { Provider } from "react-redux";
import { store } from "../mocks/mockStore";
import { BrowserRouter } from 'react-router-dom';
import { useNavigate } from 'react-router-dom';

jest.mock('react-router-dom', () => ({
  ...jest.requireActual('react-router-dom'),
  useNavigate: jest.fn()
}));


beforeEach(() => {
  const mockToken = JSON.stringify({ userId: '0' });
  
  Storage.prototype.getItem = jest.fn((key) => {
    if (key === 'token') {
      return mockToken;
    }
    return null;
  });

});
  
afterEach(() => {
  jest.restoreAllMocks();
  jest.resetAllMocks();
});

const renderComponent = () =>
  render(
    <Provider store={store}>
      <BrowserRouter>
        <CreateDeck />
      </BrowserRouter>
    </Provider>
  );

describe('CreateDeck Component', () => {

  it('updates name and description', async () => {

    const {getByLabelText, getByDisplayValue } = renderComponent();

    const nameInput = getByLabelText("Name");
    const descriptionInput = getByLabelText("Description");


    fireEvent.change(nameInput, { target: { value: 'testName' } });
    fireEvent.change(descriptionInput, { target: { value: 'testDescription' } });

    expect(getByDisplayValue('testName')).toBeInTheDocument();
    expect(getByDisplayValue('testDescription')).toBeInTheDocument();
  });

  it('updates name and description', async () => {

    const { getByDisplayValue } = renderComponent();

    const personalRadio = getByDisplayValue("Personal");
    const communityRadio = getByDisplayValue("Community");

    fireEvent.click(personalRadio);
    expect(personalRadio.checked).toBe(true);

    fireEvent.click(communityRadio);
    expect(communityRadio.checked).toBe(true);

  });

  it('submits the form with correct data', async () => {
  
    const mockNavigate = jest.fn();
    useNavigate.mockReturnValue(mockNavigate);
  
  
    const { getByLabelText, getByText } = renderComponent();
    fireEvent.change(getByLabelText("Name"), { target: { value: 'Test Deck' } });
    fireEvent.change(getByLabelText("Description"), { target: { value: 'Description' } });
    fireEvent.click(getByText("Add Deck"));
  
    await waitFor(() => {
        expect(mockNavigate).toHaveBeenCalledWith('/browse');
    });

  });

  it('handles file selection', () => {
    const { getByTestId, getByText } = renderComponent();
    const input = getByTestId("file-input");
    const file = new File(['hello'], 'hello.png', { type: 'image/png' });
  
    fireEvent.change(input, { target: { files: [file] } });
    expect(input.files[0]).toBe(file);

    const uploadButton = getByText("Upload");

    fireEvent.click(uploadButton);

    expect(screen.queryByText('Error uploading file')).toBeNull();
  });

  it('handles wrong upload', () => {
    const { getByTestId, getByText } = renderComponent();
    const input = getByTestId("file-input");
    const uploadButton = getByText("Upload");

    fireEvent.change(input, { target: { files: [] } });

    fireEvent.click(uploadButton);

    expect(getByText("Error uploading file")).toBeInTheDocument();
  });

});