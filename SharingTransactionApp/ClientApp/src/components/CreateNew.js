import React, { Component } from 'react';
import { useHistory } from 'react-router-dom';
import axios from 'axios'
import App from '../App';

export class CreateNew extends Component {

  constructor(props) {
      super(props);
      this.state = {
          collaborators: [],
          avalibleUsers: [],
          changingCollabs: false,
          title: "",
          describtion: "",
          cost: 0,
          file: '',
          format: '',
          created:false
      };
      this.uploadFile = this.uploadFile.bind(this)
      this.getUsers = this.getUsers.bind(this)
      this.selectCollaborators = this.selectCollaborators.bind(this)
      this.changeButtonHandler = this.changeButtonHandler.bind(this)
      this.addCollaborator = this.addCollaborator.bind(this)
      this.deleteCollaborator = this.deleteCollaborator.bind(this)
      this.createAndSend = this.createAndSend.bind(this)
      this.getCost = this.getCost.bind(this)
      
    }
    componentDidMount() {
        this.getUsers()
    }
    getUsers() {
        axios.get('api/user').then(response => this.setState({ avalibleUsers: response.data }))
    }
    addCollaborator(user) {
        this.setState({ collaborators: [...this.state.collaborators, user] })
    }
    deleteCollaborator(user) {
        this.setState({ collaborators: this.state.collaborators.filter(u=>u!=user) })
    }
    selectCollaborators() {
        var data = this.state.avalibleUsers.map(user =>
            <tr>
                <td>{user}</td>
                <td>
                    <button className="btn btn-success" onClick={() => this.addCollaborator(user)}>Add</button>
                    <button className="btn btn-danger" onClick={() => this.deleteCollaborator(user)}> Delete</button>
                </td>
            </tr>)
        return data
    }
    changeButtonHandler() {
        this.setState({ changingCollabs: !this.state.changingCollabs })
    }
    createAndSend() {

        console.log(this.state.title)
        console.log(this.state.cost)
        console.log(this.state.describtion)
        console.log(this.state.collaborators)
        var data = {
            "title": this.state.title,
            "cost": this.state.cost,
            "description": this.state.describtion,
            "shareholders": this.state.collaborators,
            "format": this.state.format,
            "file": this.state.file
        }

        axios.post("api/creation", data).then(response => {
            var history = useHistory()
            history.push('/')
        }).catch(error => { return })
        this.setState({ created: true })

    }
    getCost(e) {
        var string = e.replace(",", ".")
        var cost = parseFloat(string)
        this.setState({cost:cost})
    }
    uploadFile(e) {
        
        let reader = new FileReader();
        let file = e.target.files[0];
        let format= file.type.split('/')[1]
        console.log(file); 
        console.log(format);
        this.setState({ format: format })
        reader.onload = () => {
            this.setState({
                file: reader.result
            });
        }
        reader.readAsBinaryString(file);
    }

    render() {
        var content = this.state.created ? <div><h1>Created</h1><h4>Check History</h4></div>
            : <div>
                <table className='table'>
                    <thead></thead>
                    <tbody>
                        <tr><td><strong>Title</strong></td><td><input type="text" value={this.state.title} onChange={c => this.setState({ title: c.target.value })} /></td></tr>
                        <tr><td><strong>Cost</strong></td><td><input type="text" onChange={c => this.getCost(c.target.value)} /></td></tr>
                        <tr><td><strong>Bill</strong></td><td><input type="file" accept="image/png, image/jpeg,application/pdf" onChange={this.uploadFile} /></td></tr>

                        <tr><td><strong>Collaborators</strong></td>
                            <td>
                                {this.state.collaborators.map(user => <strong> {user}<br /> </strong>)}
                                <button className="btn btn-primary" onClick={this.changeButtonHandler}>Change/Close</button>
                            </td>
                        </tr>
                        {this.state.changingCollabs ? this.selectCollaborators() : ''}
                        <tr><td><strong>Description</strong></td><td><textarea type="text" value={this.state.describtion} onChange={c => this.setState({ describtion: c.target.value })} /></td></tr>
                    </tbody>
                </table>

                <button className="btn btn-primary" onClick={this.createAndSend}>Create</button>


            </div>

        return (
    <div>
        {content}
    </div>
    );
  }


}
