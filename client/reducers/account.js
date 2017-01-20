import {
  SIGNIN_SUCCEEDED,
} from '../actions/account';

export const initialState = {
  user: null,
  token: null,
};

export default function (state = initialState, action) {
  switch (action.type) {
    case SIGNIN_SUCCEEDED:
      return {
        ...state,
        user: action.user,
        token: action.token,
      };
    default: return state;
  }
}
