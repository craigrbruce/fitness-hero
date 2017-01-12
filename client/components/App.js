import React from 'react';
import * as Mdl from 'react-mdl';
import s from './App.css';

class App extends React.Component {
  componentDidMount() {
    window.componentHandler.upgradeElement(this.root);
  }

  componentWillUnmount() {
    window.componentHandler.downgradeElements(this.root);
  }

  render() {
    return (
      <Mdl.Layout fixedDrawer fixedHeader >
        <Mdl.Header title="Fitness Hero" className={s.header} />
        <Mdl.Drawer title="Welcome Bob User" className={s.drawer}>
          <Mdl.Navigation className={s.navigation} >
            <a href="/">Home</a>
            <a href="/clients">Clients</a>
            <a href="/appointments">Appointments</a>
          </Mdl.Navigation>
        </Mdl.Drawer>
        <Mdl.Content >
          {this.props.children}
        </Mdl.Content>
      </Mdl.Layout>
    );
  }
}

App.propTypes = {
  children: React.PropTypes.element.isRequired,
};

export default App;
