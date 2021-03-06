import React, { Component, useState  } from 'react';
import { Modal, Button } from "react-bootstrap";
import { useHistory, useNavigate  } from "react-router-dom";

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
      super(props);
      this.state = { forecasts: [], loading: true, show: false, firstName: '', lastName: '', successModal: false };
      this.create = this.create.bind(this);
      this.showModal = this.showModal.bind(this);
      this.hideModal = this.hideModal.bind(this);
      this.viewDetails = this.viewDetails.bind(this); 
    }
   

  componentDidMount() {
    this.populateWeatherData();
    }

    showModal = () => { 
        this.setState({ show: true });
    };

    hideModal = () => {
        this.setState({ show: false });
    };

    hideSuccessModal = () => {
        this.setState({ successModal: false }); 
        this.populateWeatherData();
    };

    showSuccessModal = () => {
        this.setState({ successModal: true });
    };

    onChange = e => this.setState({ [e.target.name]: e.target.value })

    create(event) {
        event.preventDefault(); 
        const postRequest = {
            "firstname": this.state.firstName,
            "lastname": this.state.lastName
        };
        const jsonData = JSON.stringify(postRequest);

        fetch("api/customers", {
            body: jsonData,
            method: 'POST', headers:
            {
                "Content-Type": "application/json"
            },
        })
            .then(response => {
                if (response.status == 200) {
                    this.showSuccessModal();
                    this.hideModal();
                    
                }
            });
    }
     

     viewDetails = (event) => {  
        event.preventDefault();
         const { param } = event.target.dataset;
       /*  const navigate = useHistory();*/
         window.location.href = window.location.origin + window.location.pathname + `customers?id=${param}`;
        // navigate(`/accounts/${param}`);
    }



   renderForecastsTable(forecasts) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>ID</th>
            <th>First Name</th>
            <th>Last Name</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map(forecast =>
            <tr key={forecast.firstName}>
              <td>{forecast.id}</td>
              <td>{forecast.firstName}</td>
                  <td>{forecast.lastName}</td>
                  <td style={{ width: "150px" }}>
                      <button data-param={forecast.id} className="btn btn-primary" type="button" onClick={this.viewDetails}>
                      View Accounts
                     </button>
                  </td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : this.renderForecastsTable(this.state.forecasts);

      return ( 
      <div>
        <h1 id="tabelLabel">Customers</h1>  

            <Modal
                show={this.state.show}
                onHide={this.hideModal}
                backdrop="static"
                keyboard={false}
            >
                <Modal.Header closeButton>
                    <Modal.Title>Create Customer</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <div> 
                        <div> 
                            <div>  
                                    <div className="form-group">
                                      <label>First Name</label>
                                      <input type="text" name="firstName" onChange={this.onChange} value={this.state.firstName} className="form-control" id="firstName"  />
                                    </div>
                                    <div className="form-group">
                                      <label>Last Name</label>
                                      <input type="text" name="lastName" onChange={this.onChange} value={this.state.lastName} className="form-control" id="lastname"  />
                                    </div> 
                                   
                            </div>
                            <span></span>
                        </div>
                    </div>
                </Modal.Body>
                <Modal.Footer>
                    
                    <Button variant="primary" type="submit" onClick={this.create}>Create</Button>
                </Modal.Footer>
            </Modal>

            <button className="btn btn-primary" type="button" style={{ marginLeft: "86%" }} onClick={this.showModal}>
                Create Customer
              </button>

              <Modal
                  show={this.state.successModal}
                  onHide={this.hideSuccessModal}
                  backdrop="static"
                  keyboard={false} >
                  <Modal.Header closeButton>
                      <Modal.Title>Information</Modal.Title>
                  </Modal.Header>
               
                  <Modal.Body>
                      <label>Customer Created Successfully</label>
                  </Modal.Body>
                  <Modal.Footer> 
                      <Button variant="primary" onClick={this.hideSuccessModal}>Ok</Button>
                  </Modal.Footer>
              </Modal>
        {contents}
      </div>
    );
  }

  async populateWeatherData() {
    const response = await fetch('api/customers');
    const data = await response.json();
      this.setState({ forecasts: data.data, loading: false });
    } 
}
