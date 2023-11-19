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
  });
});