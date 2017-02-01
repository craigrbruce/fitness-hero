import React from 'react';
import * as Mui from 'material-ui';
import s from './App.css';

const LandingPage = () => (
  <Mui.Layout >
    <Mui.Header title="Fitness Hero" className={s.header} >
      <Mui.Button href="account/login">Sign in</Mui.Button>
      <Mui.Button href="account/register">Register</Mui.Button>
    </ Mui.Header>
    <Mui.Content>
      Some awesome landing page content goes here.
    </Mui.Content>
  </Mui.Layout>
);

LandingPage.propTypes = {
};

export default LandingPage;
