import React from 'react';
import { connect } from 'react-redux';
import * as Mdl from 'react-mdl';
import s from './App.css';
import { getMe } from '../actions/account';

class App extends React.Component {
  componentDidMount() {
    this.props.getMe();
  }

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
              {
                this.props.user ? `Welcome ${this.props.user.email}` : ''
              }
            </Mdl.Button>
            <Mdl.Menu target="user-menu" align="right">
              <Mdl.MenuItem ><a href="account/logout">Sign out</a>
              </Mdl.MenuItem>
            </Mdl.Menu>
          </div>
        </Mdl.Header>
        <Mdl.Drawer
          title="Fitness Hero" className={s.drawer}
          style={{ borderRightWidth: 0 }}
          >
          <Mdl.Navigation className={s.navigation} >
            <a href="#/">Home</a>
            <a href="#/clients">Clients</a>
            <a href="#/appointments">Appointments</a>
          </Mdl.Navigation>
        </Mdl.Drawer >
        <Mdl.Content >
          {
            React.Children.map(
              this.props.children,
              (child) =>
                React.cloneElement(
                  child, {
                  }
                ))
          }
        </Mdl.Content>
      </Mdl.Layout >
    );
  }
}

App.propTypes = {
  route: React.PropTypes.any,
  signOut: React.PropTypes.func,
  children: React.PropTypes.element.isRequired,
  user: React.PropTypes.any,
  getMe: React.PropTypes.func,
};

const mapStateToProps = (state) => ({
  user: state.account.user,
});

const mapDispatchToProps = (dispatch) => ({
  getMe: () => dispatch(getMe()),
});

export default connect(mapStateToProps, mapDispatchToProps)(App);
