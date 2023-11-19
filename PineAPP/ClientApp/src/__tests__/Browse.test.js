import React from 'react';
import { render, fireEvent, waitFor, waitForElementToBeRemoved  } from '@testing-library/react';
import '@testing-library/jest-dom'
import Browse from "../components/Browse";
import { Provider } from "react-redux";
import { store } from "../mocks/mockStore";
import { BrowserRouter } from 'react-router-dom';
import { act } from 'react-dom/test-utils';

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
  




describe('Browse Component', () => {
  it("handles search input and fetches results", async () => {

    global.fetch = jest.fn().mockImplementation((url) => {
      if (url.includes('api/Decks/Search')) {
          return Promise.resolve({
              ok: true,
              json: () => Promise.resolve([{id: 1, name: 'MockFirst', isPersonal: false}]),
          });
      }
      return Promise.resolve({
          ok: false,
          status: 404,
      });
    });

    const { getByPlaceholderText, getByText, queryByTestId } = render(
    <Provider store={store}>
      <BrowserRouter>
        <Browse />
      </BrowserRouter>
    </Provider>
    );
    

    await waitForElementToBeRemoved(() => queryByTestId('spinner'));

    const searchInput = getByPlaceholderText("Search decks by keyword");
    const searchButton = getByText("Search");


    // Simulate typing in the search input
    fireEvent.change(searchInput, { target: { value: 'First' } });

    // Simulate clicking the search button
    fireEvent.click(searchButton);

    await waitFor(() => {
      expect(getByText('MockFirst')).toBeInTheDocument();
    });
  });

  it("handles search errors", async () => {

    global.fetch = jest.fn().mockImplementation((url) => {
      if (url.includes('api/Decks/Search')) {
        return Promise.resolve({
          ok: false,
          status: 404,
        });
      }
      
    });

    const { getByPlaceholderText, getByText, queryByTestId } = render(
    <Provider store={store}>
      <BrowserRouter>
        <Browse />
      </BrowserRouter>
    </Provider>
    );
    
    const searchInput = getByPlaceholderText("Search decks by keyword");
    const searchButton = getByText("Search");

    // Simulate typing in the search input
    fireEvent.change(searchInput, { target: { value: 'isToFail' } });

    // Simulate clicking the search button
    fireEvent.click(searchButton);

    await waitFor(() => {
      expect(getByText('No results found.')).toBeInTheDocument();
    });

  });
});
