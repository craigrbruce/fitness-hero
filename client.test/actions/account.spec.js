import {
  getMe,
  GET_ME_SUCCEEDED,
} from 'actions/account';
import * as common from 'actions/common';

describe('account actions', () => {
  let callStub;

  beforeEach(() => {
    callStub = sinon.stub(common, 'call');
  });

  afterEach(() => {
    callStub.restore();
  });

  describe('get me', () => {
    it('should invoke `call` with correct parameters', () => {
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
