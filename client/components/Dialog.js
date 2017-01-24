import React from 'react';
import * as Mdl from 'react-mdl';

const Dialog = ({
  title,
  handleSave,
  handleCancel,
  children,
}) => (
  <Mdl.Dialog open>
    <Mdl.DialogTitle>{title}</Mdl.DialogTitle>
    <Mdl.DialogContent>
      {
        children
      }
    </Mdl.DialogContent>
    <Mdl.DialogActions>
      <Mdl.Button onClick={handleSave}>Save</Mdl.Button>
      <Mdl.Button onClick={handleCancel}>Cancel</Mdl.Button>
    </Mdl.DialogActions>
  </Mdl.Dialog>
);

Dialog.propTypes = {
  title: React.PropTypes.string.isRequired,
  handleSave: React.PropTypes.func.isRequired,
  handleCancel: React.PropTypes.func.isRequired,
  children: React.PropTypes.any.isRequired,
};

export default Dialog;

