import React, { Component } from 'react';
import axios from 'axios'


export class Home extends Component {

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
        axios.get('api/balance').then(response => {
            this.setState({ transactions: response.data, loading: false }, () => { console.log(this.state) });
        })
    }

    static renderBalances(elements, clickaction) {
        console.log(elements)
        return (
            <table className='table table-striped table-hover' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Person</th>
                        <th>Balance[PLN]</th>
                    </tr>
                </thead>
                <tbody >
                    {elements.map((element) =>
                        <tr >
                            <td data-title="id">{element.name}</td>
                            <td data-title="id2">{element.cash}</td>
                        </tr>
                    )}
                </tbody>
            </table>

        );
    }

    render() {
        var contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Home.renderBalances(this.state.transactions, this.action);

        return (
            <div>
                {contents}
            </div>
        );
    }


}
