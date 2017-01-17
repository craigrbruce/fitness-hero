import React, { PropTypes } from 'react';

const title = 'Fitness Hero';

const data = [
  { name: 'client a' },
  { name: 'client b' },
  { name: 'client c' },
  { name: 'client d' },
  { name: 'client e' },
  { name: 'client f' },
  { name: 'client g' },
  { name: 'client h' },
  { name: 'client i' },
];

const TextCell = ({
  rowIndex,
  data,
  col,
  ...props
}) => (
  <Cell {...props}>
    {data[rowIndex][col]}
  </Cell>
);

class Home extends React.Component {
  static propTypes = {
  };

  componentDidMount() {
    document.title = title;
  }

  render() {
    return (
      <div style={{ width: '100%', height: '100%' }} />
    );
  }
}

export default Home;
