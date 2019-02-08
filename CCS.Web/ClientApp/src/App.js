import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import FetchData from './components/FetchData';
import Dashboard from './components/Dashboard/Dashboard';
import Measures from './components/Measures/Measures';

export default () => (
  <Layout>
    <Route exact path='/' component={Dashboard} />
    <Route path='/dashboard' component={Dashboard} />
    <Route path='/measures' component={Measures} />
    <Route path='/fetchdata/:startDateIndex?' component={FetchData} />
  </Layout>
);
