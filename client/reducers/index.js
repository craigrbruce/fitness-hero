import { routerReducer as routing } from 'react-router-redux';
import { combineReducers } from 'redux';

import account from './account';

const rootReducer = combineReducers({
  routing,
  account,
});

export default rootReducer;
