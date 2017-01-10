import React from 'react';
import * as Mdl from 'react-mdl';
import Header from './Header';
import s from './Layout.css';
import Navigation from './Navigation';
import Link from '../Link';

class Layout extends React.Component {

  componentDidMount() {
    window.componentHandler.upgradeElement(this.root);
  }

  componentWillUnmount() {
    window.componentHandler.downgradeElements(this.root);
  }

  render() {
    return (
      <Mdl.Layout fixedDrawer fixedHeader>
        <Header title="Fitness Hero" />
        <Mdl.Drawer title="Welcome Bob User">
          <Navigation />
        </Mdl.Drawer>
        <Mdl.Content >
          <main {...this.props} className={s.content} />
        </Mdl.Content>
      </Mdl.Layout>
    );
  }
}

export default Layout;
