import React, { Component, PropTypes } from 'react';
import { Provider } from 'react-redux';
import { Router } from 'react-router';
// import DevTools from './DevTools';
import routes from '../routes';

export default class Root extends Component {
  render() {
    const { store, history, auth } = this.props;
    return (
      <Provider store={store}>
        <div>
          <Router
            history={history}
            routes={React.Children.map(routes, (route) => React.cloneElement(route, { auth }))}
            />
          { /* <DevTools /> */}
        </div>
      </Provider>
    );
  }
}

Root.propTypes = {
  store: PropTypes.object.isRequired,
  history: PropTypes.object.isRequired,
  auth: PropTypes.object.isRequired,
};
