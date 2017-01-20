import React from 'react';
import * as Mdl from 'react-mdl';
import s from './App.css';

const LandingPage = ({ signIn, register }) => (
  <Mdl.Layout >
    <Mdl.Header title="Fitness Hero" className={s.header} >
      <Mdl.Button onClick={signIn}>Sign in</Mdl.Button>
      <Mdl.Button onClick={register}>Register</Mdl.Button>
    </ Mdl.Header>
    <Mdl.Content>
      Some awesome landing page content goes here.
    </Mdl.Content>
  </Mdl.Layout>
);

LandingPage.propTypes = {
  signIn: React.PropTypes.func.isRequired,
  register: React.PropTypes.func.isRequired,
};

export default LandingPage;
