/* eslint-disable */
var jsdom = require('jsdom').jsdom;
var chai = require('chai');
var sinon = require('sinon');
var chaiAsPromised = require('chai-as-promised');
var sinonChai = require('sinon-chai');
var sinonStubPromise = require('sinon-stub-promise');

global.document = jsdom('');
global.window = document.defaultView;
Object.keys(document.defaultView).forEach((property) => {
  if (typeof global[property] === 'undefined') {
    global[property] = document.defaultView[property];
  }
});

global.navigator = {
  userAgent: 'node.js',
};

global.sinon = sinon;
global.expect = chai.expect;

chai.use(sinonChai);
chai.use(chaiAsPromised);
sinonStubPromise(sinon);

global.localStorage = {
  getItem: sinon.spy(),
  setItem: sinon.spy(),
};
