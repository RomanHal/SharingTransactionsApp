import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import axios from 'axios'
import './NavMenu.css';

export class NavMenu extends Component {
   displayName = NavMenu.name;
  constructor (props) {
      super(props);
      
    this.state = {
      userName: "Logged Out"
    };
    }
    componentDidMount() {
        axios.get('api/user/activeuser').then(response => { this.setState({ userName: response.data }) })
    }

  //    < NavLink tag = { Link } className = "text-dark" strict to = "/login" > Login</NavLink > 
  render () {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand tag={Link} to="/">SharingTransactionApp</NavbarBrand>
                    <NavbarBrand>User : {this.state.userName}</NavbarBrand>
                    
           
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/create">Create</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/history">History</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/login">Login</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link}  className="text-dark" strict to="/logout">Logout</NavLink>
                </NavItem>
              </ul>
          
                </Container>
            </Navbar>
      </header>
    );
  }
}
