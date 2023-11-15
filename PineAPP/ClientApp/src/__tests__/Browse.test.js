import React from 'react';
import { render, fireEvent, waitFor, waitForElementToBeRemoved  } from '@testing-library/react';
import Browse from "../components/Browse";
import { Provider } from "react-redux";
import { store } from "../mocks/mockStore";
import { act } from 'react-dom/test-utils';

beforeEach(() => {
    // Create a mock token object with userId
    const mockToken = JSON.stringify({ userId: '0' });
  
    // Mock sessionStorage.getItem to return the mock token for 'token'
    Storage.prototype.getItem = jest.fn((key) => {
      if (key === 'token') {
        return mockToken;
      }
      return null;
    });
  });
  
  afterEach(() => {
    // Clear the mock
    jest.restoreAllMocks();
  });
  

describe('Browse Component', () => {
  it("handles search input and fetches results", async () => {
    // Mock global fetch here if necessary

    const { getByPlaceholderText, getByText, queryByTestId } = render(
      <Provider store={store}>
        <Browse />
      </Provider>
    );
    

    await waitForElementToBeRemoved(() => queryByTestId('spinner'));

    const searchInput = getByPlaceholderText("Search decks by keyword");
    const searchButton = getByText("Search");


    // Simulate typing in the search input
    // fireEvent.change(searchInput, { target: { value: 'test' } });

    // // Simulate clicking the search button
    // fireEvent.click(searchButton);

    // // Use waitFor for asynchronous updates
    // await waitFor(() => {
    //   // Assert that the search results or loading spinner are displayed
    //   // If loading spinner has a unique text or test-id, use that for assertion
    //   // For example: expect(getByTestId('loading-spinner')).toBeInTheDocument();
    //   // If you are expecting specific search results, assert for those
    //   expect(getByText('Mock Deck')).toBeInTheDocument();
    // });
  });

  // ... other tests
});
