import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { CustomerDetail } from './components/CustomerDetail';
import { AccountDetail } from './components/AccountDetail';


import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
      return (
      <Layout>
        <Route exact path='/' component={FetchData} />
        <Route path='/customers' component={CustomerDetail} />
        <Route exact path='/accounts' component={AccountDetail} />
        <Route path='/counter' component={Counter} /> 
      </Layout>
    );
  }
}
