import { setupServer} from "msw/node";
import { rest } from 'msw';

const handlers = [
    rest.get('https://localhost:7074/api/Decks/All/:creatorId', (req, res, ctx) => {
    const { creatorId } = req.params;

    const mockData = [
      { id: 1, name: 'Deck 1', creatorId: creatorId },
      { id: 2, name: 'Deck 2', creatorId: creatorId },
    ];
 
    return res(
      ctx.status(200), 
      ctx.json(mockData)
    );
  }),
];


export const server = setupServer(...handlers);
