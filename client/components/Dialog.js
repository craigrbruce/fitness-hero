import React from 'react';
import * as Mui from 'material-ui';

const Dialog = ({
  title,
  handleSave,
  handleCancel,
  children,
}) => (
  <Mui.Dialog open>
    <Mui.DialogTitle>{title}</Mui.DialogTitle>
    <Mui.DialogContent>
      {
        children
      }
    </Mui.DialogContent>
    <Mui.DialogActions>
      <Mui.Button onClick={handleSave}>Save</Mui.Button>
      <Mui.Button onClick={handleCancel}>Cancel</Mui.Button>
    </Mui.DialogActions>
  </Mui.Dialog>
);

Dialog.propTypes = {
  title: React.PropTypes.string.isRequired,
  handleSave: React.PropTypes.func.isRequired,
  handleCancel: React.PropTypes.func.isRequired,
  children: React.PropTypes.any.isRequired,
};

export default Dialog;

