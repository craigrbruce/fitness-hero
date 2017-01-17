import chai, { expect } from 'chai';
import sinonChai from 'sinon-chai';
import sinonStubPromise from 'sinon-stub-promise';
import sinon from 'sinon';
import call from '../../client/actions/common';
import * as api from '../../client/lib/api';

chai.use(sinonChai);
sinonStubPromise(sinon);

describe('call', () => {
  let dispatchStub;
  let getStub;
  let httpVerb;
  let successAction;
  let successProperty;
  let failedAction;
  let url;
  let params;

  beforeEach(() => {
    dispatchStub = sinon.stub();
    getStub = sinon.stub(api, 'get');
    httpVerb = 'get';
    successAction = 'success_action';
    successProperty = 'successProperty';
    failedAction = 'failed_action';
    url = 'some/url';
    params = '?some=params';
  });

  afterEach(() => {
    getStub.restore();
  });

  context('when succeeded', () => {
    beforeEach(() => {
      getStub.returnsPromise().resolves({ data: { id: 1 } });
    });

    it('should dispatch specified success action', () => {
      call(
        httpVerb,
        successAction,
        successProperty,
        failedAction,
        url,
        params
      )(dispatchStub);

      const dispatchArgs: any = dispatchStub.lastCall.args[0];

      expect(getStub.calledWith(url)).to.be.true;
      expect(dispatchArgs).to.deep.equal({
        type: successAction,
        [successProperty]: { id: 1 },
      });
    });
  });

  context('when failed', () => {
    beforeEach(() => {
      getStub.returnsPromise().rejects({ statusText: 'error' });
    });

    it('should dispatch specified failure action', () => {
      call(
        httpVerb,
        successAction,
        successProperty,
        failedAction,
        url,
        params
      )(dispatchStub);

      expect(getStub.calledWith(url)).to.be.true;
      const dispatchArgs: any = dispatchStub.lastCall.args[0];

      expect(dispatchArgs).to.deep.equal({
        type: failedAction,
        error: 'error',
      });
    });
  });
});

