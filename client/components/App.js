import React from 'react';
import getMuiTheme from 'material-ui/styles/getMuiTheme';
import Dashboard from 'material-ui/svg-icons/action/dashboard';
import Event from 'material-ui/svg-icons/action/event';
import Settings from 'material-ui/svg-icons/action/settings';
import People from 'material-ui/svg-icons/social/people';
import Person from 'material-ui/svg-icons/social/person';
import PersonOutline from 'material-ui/svg-icons/social/person-outline';
import { connect } from 'react-redux';
import * as Mui from 'material-ui';
import { getMe } from 'actions/account';

const DRAWER_WIDTH = 320;

export class App extends React.Component {
  static childContextTypes = {
    muiTheme: React.PropTypes.object,
  };

  constructor(props) {
    super(props);
    this.state = {
      open: false,
      docked: false,
    };
  }

  getChildContext() {
    return {
      muiTheme: getMuiTheme(),
    };
  }

  componentWillMount() {
    const mql = window.matchMedia('(min-width: 800px)');
    mql.addListener(this.mediaQueryChanged);
    this.setState({ mql });
  }

  componentDidMount() {
    this.props.getMe();
    this.mediaQueryChanged();
  }

  componentWillUnmount() {
    this.state.mql.removeListener(this.mediaQueryChanged);
  }

  mediaQueryChanged = () => {
    const docked = this.state.mql.matches;
    const open = this.state.mql.matches;
    this.setState({ docked, open });
  }

  handleToggle = () => this.setState({ open: !this.state.open });

  closeDrawer = () => this.setState({ open: false });

  handleDrawerRequestChange = (open) => this.setState({ open });

  handleMenuTouchTap = (route) => {
    history.push(route);
    if (this.state.docked === false) {
      setTimeout(() => this.setState({ open: false }), 1000);
    }
  };

  render() {
    return (
      <div className="application" >
        <Mui.AppBar
          showMenuIconButton={this.state.docked === false}
          style={{ marginLeft: this.state.docked ? DRAWER_WIDTH : 0 }}
          onLeftIconButtonTouchTap={this.handleToggle}
          title="Fitness Hero"
          iconElementRight={
            <div className="app-bar-actions">
              <Mui.IconMenu
                targetOrigin={{ horizontal: 'right', vertical: 'top' }}
                anchorOrigin={{ horizontal: 'right', vertical: 'top' }}
                iconButtonElement={<Mui.IconButton labelPosition="before"><PersonOutline /></Mui.IconButton>}
                >
                <Mui.MenuItem
                  primaryText="Sign out"
                  href="account/logout"
                  />
              </Mui.IconMenu>
            </div>
          }
          />
        <Mui.Drawer
          open={this.state.open}
          docked={this.state.docked}
          width={DRAWER_WIDTH}
          onRequestChange={this.handleDrawerRequestChange}
          >
          <Mui.Menu>
            <Mui.MenuItem href="#/" primaryText="Dashboard" leftIcon={<Dashboard />} />
            <Mui.MenuItem href="#/clients" primaryText="Clients" leftIcon={<People />} />
            <Mui.MenuItem primaryText="Schedule" leftIcon={<Event />} />
            <Mui.Divider />
            <Mui.MenuItem primaryText="Settings" leftIcon={<Settings />} />
            <Mui.MenuItem primaryText="Acccount" leftIcon={<Person />} />
          </Mui.Menu>
        </Mui.Drawer >
        <div className="main-content" style={{ marginLeft: this.state.docked ? DRAWER_WIDTH : 0 }}>
          {
            React.Children.map(
              this.props.children,
              (child) =>
                React.cloneElement(
                  child, {
                  }
                ))
          }
        </div>
      </div >
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
