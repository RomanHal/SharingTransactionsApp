import React, { Component } from 'react';
import './NavMenu.css';
import axios from 'axios'
import { saveAs } from 'file-saver'
export class HistoryDetails extends Component {

  constructor(props) {
      super(props);
      this.state = { details: [], loading: true, choosen: props.choosen, mode: props.mode, loading: true };
      this.showHistry = props.action
      this.getDetails = this.getDetails.bind(this);
      this.downloadBill = this.downloadBill.bind(this)
  }

    componentDidMount() {
        this.getDetails()
    }
    getDetails() {
        var url = 'api/Transaction/getDetails?id=' + this.state.choosen
        console.log(url)
        axios.get(url).then(response => {
            this.setState({ details: response.data, loading: false })
        }, () => { console.log(this.state.details) })
    }
    downloadBill() {
        console.log('downloadProcedure')
        axios.get('api/image?id=' + this.state.details.file).then(response => {
            console.log(response)
            var format = response.data.format
            console.log(format)
            var formatData = format == 0 ? 'application/pdf' : format == 2 ? 'image/png' :'image/jpg'
            console.log(this.state.choosen)
            var data = `data:${formatData};base64,${btoa(response.data.data)}`
            console.log(this.state.choosen)
            var formatString = format == 0 ? 'pdf' : format == 1 ? 'jpg' : 'png'
            var fileName = `bill.` + formatString

            saveAs(data, fileName);
        })

        
    }
 
    render() {
        console.log(this.state.details)
        var content = this.state.loading ? <p>Loading...</p>
        : <table className='table table-striped table-hover'>
            <tr><td><strong>Title</strong></td><td>{this.state.details.title}</td></tr>
            <tr><td><strong>Date</strong></td><td>{this.state.details.date}</td></tr>
            <tr><td><strong>Author</strong></td><td>{this.state.details.creator}</td></tr>
            <tr><td><strong>Cost</strong></td><td>{this.state.details.cash}</td></tr>
            <tr><td><strong>Bill</strong></td><td><button onClick={this.downloadBill} className="btn btn-info">GET</button></td></tr>

            <tr><td><strong>Collaborants</strong></td><td><strong>Accepted</strong></td></tr>
            {this.state.details.shareholders.map(p => <tr><td>{p.person}</td><td>{p.confirmation ? "Yes" : "No"}</td> </tr>)}
            <tr><td><strong>Description</strong></td><td>{this.state.details.description}</td></tr>

        </table>
    return (
        <div><br/>
            <button className="btn btn-primary" onClick={this.showHistry}>Return</button>
            {content}
           
      </div>
    );
  }

}
