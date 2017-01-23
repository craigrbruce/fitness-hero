import React from 'react';
import { connect } from 'react-redux';
import * as Mdl from 'react-mdl';
import s from './App.css';
import LandingPage from './LandingPage';

class App extends React.Component {
  componentDidMount() {
    this.props.route.auth.getUser();
  }

  render() {
    return (
      !window.userName ?
        <LandingPage /> :
        <Mdl.Layout fixedDrawer fixedHeader >
          <Mdl.Header title="Fitness Hero" className={s.header} >
            <div style={{ position: 'relative' }}>
              <Mdl.Button
                primary
                id="user-menu"
                style={{ color: 'white' }}
              >
                {
                  `Welcome ${window.userName}`
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
                      auth: this.props.route.auth,
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
  token: React.PropTypes.string,
};

const mapStateToProps = (state) => ({
  user: state.account.user,
  token: state.account.token,
});

export default connect(mapStateToProps)(App);
