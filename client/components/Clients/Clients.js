import React, { PropTypes } from 'react';
import { Column, Cell, Table } from 'fixed-data-table';
import 'fixed-data-table/dist/fixed-data-table.css';
import Dialog from 'components/Dialog';
import * as s from './Clients.css';

const title = 'Fitness Hero';

class Home extends React.Component {
  static propTypes = {
  };

  constructor(props) {
    super(props);
    this.state = {
      openDialog: false,
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

  handleCloseDialog = () => this.setState({ openDialog: false });

  handleOpenDialog = () => this.setState({ openDialog: true });

  handleSave = () => /** dispatch save action **/ this.handleCloseDialog();

  render() {
    return (
      <div>
        <div className={s.toolbar}>
          <span>add new</span>
        </div>
        {
          this.state.openDialog ?
            <Dialog
              handleSave={this.handleSave}
              handleCancel={this.handleCloseDialog}
              title="Add Client"
              >
              <span>TODO .. create a client form component</span>

            </Dialog> : ''
        }
        <Table
          rowsCount={this.state.tableData.length}
          rowHeight={50}
          width={1000}
          height={700}
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
      </div>
    );
  }
}

export default Home;
