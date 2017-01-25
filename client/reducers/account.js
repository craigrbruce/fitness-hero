import {
  GET_ME_SUCCEEDED,
} from '../actions/account';

export const initialState = {
  user: null,
};

export default function (state = initialState, action) {
  switch (action.type) {
    case GET_ME_SUCCEEDED:
      return {
        ...state,
        user: action.user,
      };
    default: return state;
  }
}
