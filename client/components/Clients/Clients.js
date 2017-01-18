import React, { PropTypes } from 'react';
import ResponsiveFixedDataTable from 'responsive-fixed-data-table';
import { Column, Cell } from 'fixed-data-table';
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
      <div style={{ width: '100%', height: '100%' }}>
        <ResponsiveFixedDataTable
          rowsCount={this.state.tableData.length}
          rowHeight={50}
          width={1000}
          height={500}
          headerHeight={50}
          >
          <Column
            width={100}
            header={<Cell>Name</Cell>}
            cell={props => (
              <Cell {...props}>
                {this.state.tableData[props.rowIndex].name}
              </Cell>
            )}
            />
        </ResponsiveFixedDataTable>
      </div>
    );
  }
}

export default Home;
