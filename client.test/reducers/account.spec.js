import { expect } from 'chai';
import reducer, { initialState } from '../../client/reducers/account';
import {
  GET_ME_SUCCEEDED,
} from '../../client/actions/account';

describe('account reducer', () => {
  it('should return the default state for an unknown action', () => {
    const state = reducer(undefined, { type: 'nonesense' });
    expect(state).to.deep.equal(initialState);
  });

  describe('get me succeeded', () => {
    it('should update state with user', () => {
      const expectedState = {
        user: { name: 'some user' },
      };
      const startingState = { user: null };
      const action = {
        type: GET_ME_SUCCEEDED,
        user: { name: 'some user' },
      };
      const actualState = reducer(
        startingState,
        action
      );
      expect(actualState).to.deep.equal(expectedState);
    });
  });
});
