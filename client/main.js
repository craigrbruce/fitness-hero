import 'babel-polyfill';
import 'dialog-polyfill';
import injectTapEventPlugin from 'react-tap-event-plugin';
import React from 'react';
import { render } from 'react-dom';
import FastClick from 'fastclick';
import { hashHistory } from 'react-router';
import { syncHistoryWithStore } from 'react-router-redux';
import { AppContainer } from 'react-hot-loader';
import Auth from './lib/auth';

import configureStore from './store/configureStore';
import Root from './components/Root';

const store = configureStore();
const auth = new Auth(store);
const container = document.getElementById('container');
const history = syncHistoryWithStore(hashHistory, store);

injectTapEventPlugin();
render(
  <AppContainer>
    <Root store={store} history={history} auth={auth} />
  </AppContainer>,
  container
);


FastClick.attach(document.body);

if (module.hot) {
  module.hot.accept('./components/Root.js', () => {
    // eslint-disable-next-line global-require, import/newline-after-import
    const NewRoot = require('./components/Root').default;
    render(
      <AppContainer>
        <NewRoot store={store} history={history} auth={auth} />
      </AppContainer>,
      container
    );
  });
}
