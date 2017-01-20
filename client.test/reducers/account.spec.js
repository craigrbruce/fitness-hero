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
      const expectedState = {
        user: { name: 'some user' },
        token: 'some_token',
      };
      const startingState = { user: null, token: null };

      const action = {
        type: SIGNIN_SUCCEEDED,
        user: { name: 'some user' },
        token: 'some_token',
      };

      const actualState = reducer(
        startingState,
        action
      );

      expect(actualState).to.deep.equal(expectedState);
    });
  });
});
