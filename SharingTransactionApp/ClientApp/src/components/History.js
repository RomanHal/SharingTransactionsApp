import React, { Component } from 'react';
import axios from 'axios'
import { HistoryDetails } from './HistoryDetails';
import { HistoryTable } from './HistoryTable';
import { Route } from 'react-router';
import './NavMenu.css';
export class History extends Component {
    static displayName = History.name;

  constructor(props) {
      super(props);
      this.state = { forecasts: [], showDetails: false, choosen: '',state:'h' };
      this.showHistry = this.showHistry.bind(this)
      this.showToConfirm = this.showToConfirm.bind(this)
      this.showCreatedMyself = this.showCreatedMyself.bind(this)
      this.showDetail = this.showDetail.bind(this)
      this.confirm = this.confirm.bind(this)
  }
    showHistry = (data) => {
        this.setState({ showDetails: false, state:'h' })
    }
    showToConfirm = (data) => {
        this.setState({ showDetails: false, state: 'c' })
    }
    showCreatedMyself = (data) => {
        this.setState({ showDetails: false, state: 'm' })
    }
    showDetail = (data) => {
        this.setState({ showDetails: true, choosen: data });
    }
    confirm = (data) => {
        console.log(this.state.choosen)
        var data = { "data" : this.state.choosen }
        axios.post("api/confirmation", data).then(response => { }).catch(error => { })
        this.showHistry()
    }
  componentDidMount() {

  }


 
    render() {
    return (
        <div>
            <button type="button" onClick={()=>this.showHistry()} className="btn btn-primary">History</button> {" "}
            <button type="button" onClick={()=>this.showCreatedMyself()} className="btn btn-primary">Created</button> {" "}
            <button type="button" onClick={() => this.showToConfirm()} className="btn btn-primary">ToAccept</button> {" "}
            {this.state.showDetails ? <HistoryDetails action={this.showHistry} choosen={this.state.choosen}></HistoryDetails>:''}
            {(!this.state.showDetails && this.state.state == 'm') ? <HistoryTable url='api/creation' action={this.showDetail}></HistoryTable>:''}
            {(!this.state.showDetails && this.state.state == 'c') ? <HistoryTable url='api/confirmation' action={this.showDetail}></HistoryTable> : ''}
            {(this.state.showDetails && this.state.state == 'c') ?
                <Route to='/'>
                    <button type="button" onClick={() => this.confirm()} className="btn btn-primary">Confirm</button>
                </Route>
                : ''}
            {(!this.state.showDetails && this.state.state == 'h') ? <HistoryTable url='api/transaction' action={this.showDetail}></HistoryTable>:''}

      </div>
    );
  }

}
