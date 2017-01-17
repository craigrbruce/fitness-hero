import { expect } from 'chai';
import sinon from 'sinon';
import request from 'axios';
import {
  bulkDeleteResource,
  save,
  get,
} from '../../client/lib/api';

describe('get', () => {
  let getStub;

  beforeEach(() => {
    getStub = sinon.stub(request, 'get');
  });

  afterEach(() => {
    getStub.restore();
  });

  context('when params are provided', () => {
    beforeEach(() => {
      get('foo/bar/url', { foo: 'bar' });
    });

    it('should call the api with the provided params', () => {
      const args = getStub.lastCall.args;
      const url = args[0];
      const options = args[1];

      expect(url).to.equal('foo/bar/url');
      expect(options.params).to.deep.equal({ foo: 'bar' });
    });
  });

  context('when array of ids is provided', () => {
    beforeEach(() => {
      get('foo/bar/url', [1, 2, 3, 4]);
    });

    it('should call the api with the correct query string', () => {
      const args = getStub.lastCall.args;
      const url = args[0];

      expect(url).to.equal('foo/bar/url?ids=1&ids=2&ids=3&ids=4');
    });
  });

  context('when params are not provided', () => {
    beforeEach(() => {
      request.get('foo/bar/url/1');
    });

    it('should call the api with the provided url', () => {
      const args = getStub.lastCall.args;
      const url = args[0];

      expect(url).to.equal('foo/bar/url/1');
    });
  });
});

describe('save', () => {
  let postStub;
  let putStub;

  beforeEach(() => {
    postStub = sinon.stub(request, 'post');
    putStub = sinon.stub(request, 'put');
  });

  afterEach(() => {
    postStub.restore();
    putStub.restore();
  });

  context('when id is 0', () => {
    beforeEach(() => {
      save('foo/bar/url', { id: 0 });
    });

    it('should use the POST http verb and not apply the id to the url', () => {
      const url = postStub.lastCall.args[0];
      expect(url).to.equal('foo/bar/url');
      expect(postStub.called).to.be.true;
      expect(putStub.called).to.be.false;
    });
  });

  context('when id is not 0', () => {
    beforeEach(() => {
      save('foo/bar/url', { id: 1 });
    });

    it('should use the PUT http verb and apply the id the the url', () => {
      const url = putStub.lastCall.args[0];
      expect(url).to.equal('foo/bar/url/1');
      expect(postStub.called).to.be.false;
      expect(putStub.called).to.be.true;
    });
  });
});

describe('bulk delete', () => {
  let deleteStub;

  beforeEach(() => {
    deleteStub = sinon.stub(request, 'delete');
  });

  afterEach(() => {
    deleteStub.restore();
  });

  context('when more than one id is supplied', () => {
    it('should call api with correct query string', () => {
      bulkDeleteResource('foo/bar/thing', [1, 2, 3, 4]);
      const args = deleteStub.lastCall.args;
      const url = args[0];
      expect(url).to.equal('foo/bar/thing?ids=1&ids=2&ids=3&ids=4');

    });
  });

  context('when one id is supplied', () => {
    it('should call the delete by id endpoint', () => {
      bulkDeleteResource('foo/bar/thing', [1]);
      const args = deleteStub.lastCall.args;
      const url = args[0];
      expect(url).to.equal('foo/bar/thing/1');
    });
  });
});
