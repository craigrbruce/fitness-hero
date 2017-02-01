import React from 'react';
import getMuiTheme from 'material-ui/styles/getMuiTheme';
import ArrowDropDown from 'material-ui/svg-icons/navigation/arrow-drop-down';
import { connect } from 'react-redux';
import * as Mui from 'material-ui';
import { getMe } from 'actions/account';

class App extends React.Component {
  static childContextTypes = {
    muiTheme: React.PropTypes.object,
  };

  constructor(props) {
    super(props);
    this.state = {
      open: false,
    };
  }

  getChildContext() {
    return {
      muiTheme: getMuiTheme(),
    };
  }

  componentDidMount() {
    this.props.getMe();
  }

  handleToggle = () => this.setState({ open: !this.state.open });

  closeDrawer = () => this.setState({ open: false });

  handleDrawerRequestChange = (open: boolean) => this.setState({ open });

  handleMenuTouchTap = (route: string) => {
    history.push(route);
    setTimeout(() => this.setState({ open: false }), 1000);
  };

  render() {
    return (
      <div className="application" >
        <Mui.AppBar
          onLeftIconButtonTouchTap={this.handleToggle}
          title="Fitness Hero"
          iconElementRight={
            <div className="app-bar-actions">
              <Mui.IconMenu
                targetOrigin={{ horizontal: 'right', vertical: 'top' }}
                anchorOrigin={{ horizontal: 'right', vertical: 'top' }}
                style={{ margin: 5, color: 'white' }}
                iconButtonElement={
                  <Mui.IconButton
                    style={{ color: 'white' }}
                    icon={<ArrowDropDown />}
                    labelPosition="before"
                    />
                }
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
          docked={false}
          width={320}
          onRequestChange={this.handleDrawerRequestChange}
          >
        </Mui.Drawer >
        <div className="main-content">
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
