import { expect } from 'chai';
import sinon from 'sinon';
import {
  getMe,
  GET_ME_SUCCEEDED,
} from '../../client/actions/account';
import * as common from '../../client/actions/common';

describe('account actions', () => {
  let callStub;

  beforeEach(() => {
    callStub = sinon.stub(common, 'call');
  });

  afterEach(() => {
    callStub.restore();
  });

  describe('get me', () => {
    it('should return correct action', () => {
      getMe();
      expect(callStub.calledWith(
        'get',
        GET_ME_SUCCEEDED,
        'user',
        null,
        'api/v1/me',
      )).to.be.true;
    });
  });
});
