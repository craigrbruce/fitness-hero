import React from 'react';
import { Route, IndexRoute } from 'react-router';
import App from './components/App';
import Clients from './components/Clients/Clients';

export default (
  <Route path="/" component={App}>
    <IndexRoute component={Clients} />
  </Route>
);
