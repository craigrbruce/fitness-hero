import React from 'react';
import { connect } from 'react-redux';
import * as Mdl from 'react-mdl';
import s from './App.css';
import { signOut } from '../actions/account';

class App extends React.Component {
  render() {
    return (
      <Mdl.Layout fixedDrawer fixedHeader >
        <Mdl.Header title="Fitness Hero" className={s.header} >
          <div style={{ position: 'relative' }}>
            <Mdl.Button
              primary
              id="user-menu"
              style={{ color: 'white' }}
            >
              Welcome Frank Sidebottom
        </Mdl.Button>
            <Mdl.Menu target="user-menu" align="right">
              <Mdl.MenuItem
                onClick={this.props.signOut}
              >Sign out
              </Mdl.MenuItem>
            </Mdl.Menu>
          </div>
        </Mdl.Header>
        <Mdl.Drawer
          title="Fitness Hero" className={s.drawer}
        >
          <Mdl.Navigation className={s.navigation} >
            <a href="#/">Home</a>
            <a href="#/clients">Clients</a>
            <a href="#/appointments">Appointments</a>
          </Mdl.Navigation>
        </Mdl.Drawer >
        <Mdl.Content >
          {this.props.children}
        </Mdl.Content>
      </Mdl.Layout >
    );
  }
}

App.propTypes = {
  signOut: React.PropTypes.func,
  children: React.PropTypes.element.isRequired,
};

export default connect(null, { signOut })(App);
