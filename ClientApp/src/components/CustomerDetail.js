import React, { Component, useState  } from 'react';
import { Modal, Button } from "react-bootstrap";
import urlparse from "url-parse";
import { useHistory, useNavigate  } from "react-router-dom";

export class CustomerDetail extends Component {
  static displayName = CustomerDetail.name;

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
        const urlData = urlparse(window.location.href, true) 
        const postRequest = {
            "initialCreditDeposit": this.state.amount,
            "currencyCode": 'USD',
            "customerID": urlData.query.id
        };
        const jsonData = JSON.stringify(postRequest);

        fetch("api/accounts", {
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
         const urlData = urlparse(window.location.href, true)
         window.location.href = window.location.origin + `/accounts?id=${param}&customerID=${urlData.query.id}`;
        
    }



   renderForecastsTable(forecasts) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>ID</th>
            <th>Account Number</th>
                    <th>Currency</th>
                    <th>Balance</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map(forecast =>
            <tr key={forecast.id}>
              <td>{forecast.id}</td>
                  <td>{forecast.accountNumber}</td>
                  <td>{forecast.balance.currencyCode}</td>
                  <td>{forecast.balance.amount}</td>
                  <td style={{ width: "150px" }}>
                      <button data-param={forecast.id} className="btn btn-primary" type="button" onClick={this.viewDetails}>
                      View Transactions
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
        <h1 id="tabelLabel">Accounts</h1>  

            <Modal
                show={this.state.show}
                onHide={this.hideModal}
                backdrop="static"
                keyboard={false}
            >
                <Modal.Header closeButton>
                    <Modal.Title>Create Account</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <div> 
                        <div> 
                            <div>  
                                    <div className="form-group">
                                      <label>Amount</label>
                                      <input type="number" name="amount" onChange={this.onChange} value={this.state.amount} className="form-control" id="amount"  />
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
              <div>
                  <div>
                      <div >
                          <div className="form-group">
                              <label>First Name</label>
                              <input type="text" name="firstName" disabled={true}   value={this.state.firstName} className="form-control" id="firstName" />
                             </div>
                          <div className="form-group">
                              <label>Last Name</label>
                              <input type="text" name="lastName" disabled={true}  value={this.state.lastName} className="form-control" id="lastname" />
                          </div>

                      </div>
                      <span></span>
                  </div>
              </div>
              <button className="btn btn-primary" type="button" style={{ marginLeft: "86%", marginTop: "10px" }} onClick={this.showModal}>
                Create Account
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
                      <label>Account Created Successfully</label>
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
    const urlData = urlparse(window.location.href, true)
    const response = await fetch(`api/accounts/customer/${urlData.query.id}`);
    const data = await response.json();

    const customerResponse = await fetch(`api/customers/${urlData.query.id}`);
    const customerData = await customerResponse.json();
        this.setState({ forecasts: data.data, loading: false, firstName: customerData.data.firstName, lastName: customerData.data.lastName });
    } 
}
