import React from 'react';
import { render, fireEvent, waitFor, waitForElementToBeRemoved  } from '@testing-library/react';
import '@testing-library/jest-dom'
import Create from '../components/Create';
import { Provider } from "react-redux";
import { store } from "../mocks/mockStore";
import { BrowserRouter } from 'react-router-dom';

jest.mock('react-router-dom', () => ({
  ...jest.requireActual('react-router-dom'),
  useParams: () => ({
    id: '1'
  }),
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

describe('Create Component', () => {
  it('renders loading state correctly', async () => {

    const { getByPlaceholderText, getByText, queryByTestId } = render(
      <Provider store={store}>
          <BrowserRouter>
          <Create />
        </BrowserRouter>
      </Provider>
    );

    await waitForElementToBeRemoved(() => getByText('Loading...'));

    await waitFor(() => {
      expect(getByText('Cards in deck:')).toBeInTheDocument();
    });

    await waitFor(() => {
      expect(getByText('testF')).toBeInTheDocument();
    }); 
  });
});



describe('CardsDisplay Component', () => {

  const renderComponent = () => 
    render(
    <Provider store={store}>
      <BrowserRouter>
        <Create />
      </BrowserRouter>
    </Provider>
  );

  it('reads and renders cards', async () => {

    const { getByLabelText, getByText } = renderComponent();

    await waitFor(() => {
        expect(getByText("testF")).toBeInTheDocument();
    });

  });

  it('updates front and back side', async () => {

    const { getByDisplayValue, getByTestId } = renderComponent();
    const frontInput = getByDisplayValue("testF");
    const backInput = getByDisplayValue("testB");
    const saveButton = getByTestId('save-button-0');


    fireEvent.change(frontInput, { target: { value: 'testFrontChange' } });
    fireEvent.change(backInput, { target: { value: 'testBackChange' } });
    fireEvent.click(saveButton);

    expect(getByDisplayValue('testFrontChange')).toBeInTheDocument();
    expect(getByDisplayValue('testBackChange')).toBeInTheDocument();

  });

  it('adds a new card', async () => {

    const { getByAltText, getByDisplayValue } = renderComponent();
    const addButton = getByAltText('add').parentElement;

    fireEvent.click(addButton);

    await waitFor(() => {
      expect(getByDisplayValue('Front side')).toBeInTheDocument();
    });

  });

  it('adds a new card', async () => {

    const { getByAltText, getByDisplayValue } = renderComponent();
    const addButton = getByAltText('add').parentElement;

    fireEvent.click(addButton);

    await waitFor(() => {
      expect(getByDisplayValue('Front side')).toBeInTheDocument();
    });

  });

});