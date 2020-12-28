import React, { Component } from 'react';
import './NavMenu.css';
import axios from 'axios'

export class HistoryTable extends Component {

  constructor(props) {
    super(props);
      this.state = { transactions: [], loading: true };
      //this.redirectTo = this.redirectTo.bind(this)
      this.action = props.action
      this.url = props.url
      this.getData = this.getData.bind(this)
    }

    componentDidMount() {
        this.getData();
    }
    
    getData() {
        axios.get(this.url).then(response => {
            this.setState({ transactions: response.data, loading: false }, () => { console.log(this.state) });
        })
    }

    static renderHistoryTable(elements, clickaction) {
        console.log(elements)
    return (
        <table className='table table-striped table-hover' aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Title</th>
                    <th>Cash[PLN]</th>
                    <th>Author</th>
                    <th>Confirmed</th>
                    <th>Details</th>
                </tr>
            </thead>
            <tbody >
                {elements.map((element) =>
                    <tr >
                        <td data-title="Date">{element.date}</td>
                        <td data-title="id">{element.title}</td>
                        <td data-title="id2">{element.cash}</td>
                        <td data-title="id3">{element.creator}</td>
                        <td data-title="id4">{element.confirmed ? 'Yes' : 'No'}</td>
                        <td><button className="btn" onClick={() => clickaction(element.id)}>---></button></td>
                        </tr>
                )}
        </tbody>
       </table>

    );
  }

  render() {
    var contents = this.state.loading
        ? <p><em>Loading...</em></p>
        : HistoryTable.renderHistoryTable(this.state.transactions, this.action);

    return (
        <div>
            {contents}
      </div>
    );
  }


  }

