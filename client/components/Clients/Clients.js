import 'fixed-data-table/dist/fixed-data-table.css';
import React, { PropTypes } from 'react';
import { Table, Cell, Column } from 'fixed-data-table';

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
      <div style={{ width: '100%', height: '100%' }}>
        <Table
          height={5000}
          width={5000}
          headerHeight={50}
          rowHeight={50}
          rowsCount={data.length}
        >
          <Column
            width={1000}
            header={<Cell>Name</Cell>}
            cell={
              <TextCell data={data} width={50} />
            }
          />
        </Table>
      </div>
    );
  }
}

export default Home;
