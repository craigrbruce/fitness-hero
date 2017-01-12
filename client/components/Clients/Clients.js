import React, { PropTypes } from 'react';

const title = 'Fitness Hero';

class Home extends React.Component {
  static propTypes = {
  };

  componentDidMount() {
    document.title = title;
  }

  render() {
    return (
      <div>Some clients go here </div>
    );
  }
}

export default Home;
