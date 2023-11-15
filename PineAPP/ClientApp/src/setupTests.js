import { server } from './mocks/mockServer';

global.TextEncoder = require('util').TextEncoder;
global.TextDecoder = require('util').TextDecoder;

beforeAll(() => server.listen());
afterEach(() => server.resetHandlers());
afterAll(() => server.close());