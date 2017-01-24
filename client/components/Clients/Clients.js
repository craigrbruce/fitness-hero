import React, { PropTypes } from 'react';
import { Column, Cell, Table } from 'fixed-data-table';
import 'fixed-data-table/dist/fixed-data-table.css';

const title = 'Fitness Hero';

class Home extends React.Component {
  static propTypes = {
  };

  constructor(props) {
    super(props);
    this.state = {
      tableData: [
        { name: 'One' },
        { name: 'Two' },
        { name: 'Three' },
        { name: 'Four' },
      ],
    };
  }

  componentDidMount() {
    document.title = title;
  }

  render() {
    return (
      <Table
        rowsCount={this.state.tableData.length}
        rowHeight={50}
        width={1000}
        height={500}
        headerHeight={50}
        {...this.props}
        >
        <Column
          width={100}
          header={<Cell>Name</Cell>}
          fixed
          width={100}
          cell={props => (
            <Cell {...props}>
              {this.state.tableData[props.rowIndex].name}
            </Cell>
          )}
          />
      </Table>
    );
  }
}

export default Home;
