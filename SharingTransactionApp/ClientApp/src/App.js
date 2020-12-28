import React, { Component } from 'react';
import { Redirect, Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { CreateNew } from './components/CreateNew';
import { History } from './components/History';
import registerServiceWorker, { unregister } from './registerServiceWorker';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
            <Route exact path='/' component={Home} />
            <Route path='/create' component={CreateNew} />
            <Route path='/history' component={History} />
            <Route path='/login' component={() => { window.location.href = 'api/login'; return null; }} />
            <Route path='/logout' component={() => { window.location.assign('api/logout'); return null; }} />
            
      </Layout>
    );
  }
}
