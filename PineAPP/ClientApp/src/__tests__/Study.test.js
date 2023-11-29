import React from 'react';
import { render, fireEvent, waitFor  } from '@testing-library/react';
import '@testing-library/jest-dom'
import { Provider } from "react-redux";
import { store } from "../mocks/mockStore";
import { BrowserRouter } from 'react-router-dom';
import Study from '../components/Study';

jest.mock('react-router-dom', () => ({
  ...jest.requireActual('react-router-dom'),
  useParams: () => ({
    id: '1'
  }),
}));

  
afterEach(() => {
  jest.restoreAllMocks();
  jest.resetAllMocks();
});



describe('Study Component', () => {

  const renderComponent = () => 
    render(
    <Provider store={store}>
      <BrowserRouter>
        <Study />
      </BrowserRouter>
    </Provider>
  );

  it('reads and renders cards', async () => {

    const { getByText } = renderComponent();

    await waitFor(() => {
        expect(getByText("Loading...")).toBeInTheDocument();
    });

    await waitFor(() => {
        expect(getByText("testF")).toBeInTheDocument();
    });

  });

  it('completes successfully', async () => {

    const { getByText, getByTestId } = renderComponent();
    const card = getByTestId("card");

    fireEvent.click(card);
    await waitFor(() => {
        expect(getByText("testB")).toBeInTheDocument();
    });

    fireEvent.click(getByText("Correct"));
    await waitFor(() => {
        expect(getByText("testF2")).toBeInTheDocument();
    });

    fireEvent.click(card);
    await waitFor(() => {
        expect(getByText("testB2")).toBeInTheDocument();
    });

    fireEvent.click(getByText("Wrong"));
    await waitFor(() => {
        expect(getByText("All cards reviewed")).toBeInTheDocument();
    });
    
  });

  

});