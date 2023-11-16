
jest.mock('node-fetch', () => require('fetch-mock-jest').sandbox());

global.TextEncoder = require('util').TextEncoder;
global.TextDecoder = require('util').TextDecoder;
