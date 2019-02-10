import React from 'react';
import { Route } from 'react-router-dom'
import Layout from './components/Layout';
import FetchData from './components/FetchData';
import Dashboard from './components/Dashboard/Dashboard';
import Measures from './components/Measures/Measures';
import CssBaseline from '@material-ui/core/CssBaseline';

export default () => (
  <Layout>
    <CssBaseline />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons" />
    <Route exact path='/' component={Dashboard} />
    <Route path='/dashboard' component={Dashboard} />
    <Route path='/measures' component={Measures} />
    <Route path='/fetchdata/:startDateIndex?' component={FetchData} />
  </Layout>
);
