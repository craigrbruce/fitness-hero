import React, { PropTypes } from 'react';
import Layout from '../../components/Layout';

const title = 'Fitness Hero';

class Home extends React.Component {
  static propTypes = {
  };

  componentDidMount() {
    document.title = title;
  }

  render() {
    return (
      <Layout>
        This is the landing page. Terrific
      </Layout>
    );
  }
}

export default Home;
