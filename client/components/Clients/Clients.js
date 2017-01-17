import React, { PropTypes } from 'react';

const title = 'Fitness Hero';

class Home extends React.Component {
  static propTypes = {
  };

  constructor(props) {
    super(props);
    this.state = {
      data: [
        { name: 'client a' },
        { name: 'client b' },
        { name: 'client c' },
        { name: 'client d' },
        { name: 'client e' },
        { name: 'client f' },
        { name: 'client g' },
        { name: 'client h' },
        { name: 'client i' },
      ],
    };
  }

  componentDidMount() {
    document.title = title;
  }

  render() {
    return (
      <div style={{ width: '100%', height: '100%' }}>
        some sort of client grid here
      </div>
    );
  }
}

export default Home;
