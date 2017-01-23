import React from 'react';
import * as Mdl from 'react-mdl';
import s from './App.css';

const LandingPage = () => (
  <Mdl.Layout >
    <Mdl.Header title="Fitness Hero" className={s.header} >
      <Mdl.Button href="account/login">Sign in</Mdl.Button>
      <Mdl.Button href="account/register">Register</Mdl.Button>
    </ Mdl.Header>
    <Mdl.Content>
      Some awesome landing page content goes here.
    </Mdl.Content>
  </Mdl.Layout>
);

LandingPage.propTypes = {
};

export default LandingPage;
