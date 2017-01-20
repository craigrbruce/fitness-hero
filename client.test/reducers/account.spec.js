import { expect } from 'chai';
import reducer, { initialState } from '../../client/reducers/account';
import {
  SIGNIN_SUCCEEDED,
} from '../../client/actions/account';

describe('account reducer', () => {
  it('should return the default state for an unknown action', () => {
    const state = reducer(undefined, { type: 'nonesense' });
    expect(state).to.deep.equal(initialState);
  });

  describe('sign in succeeded', () => {
    it('should update state with user', () => {
      const expectedState = { user: { name: 'some user' } };

      const action = {
        type: SIGNIN_SUCCEEDED,
        user: { name: 'some user' },
      };

      const actualState = reducer(
        { user: undefined },
        action
      );

      expect(actualState).to.deep.equal(expectedState);
    });
  });
});
