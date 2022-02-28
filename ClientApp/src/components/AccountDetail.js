import React, { Component, useState  } from 'react';
import { Modal, Button } from "react-bootstrap";
import urlparse from "url-parse";
import { useHistory, useNavigate  } from "react-router-dom";

export class AccountDetail extends Component {
  static displayName = AccountDetail.name;

  constructor(props) {
      super(props);
      this.state = {
          forecasts: [], loading: true, show: false, balance: {}, firstName: '', lastName: '', successModal: false };
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
         window.location.href = window.location.origin + `/accounts?id=${param}`;
        
    }



   renderForecastsTable(forecasts) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>ID</th>
            <th>Transaction Reference</th>
            <th>Amount</th>
                    <th>Date</th>
                    <th>Status</th> 
          </tr>
        </thead>
        <tbody>
          {forecasts.map(forecast =>
            <tr key={forecast.id}>
              <td>{forecast.id}</td>
                  <td>{forecast.transactionReference}</td>
                  <td>{forecast.amount}</td>
                  <td>{forecast.transactionDate}</td>
                  <td>{forecast.status}</td>
                  
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
        <h1 id="tabelLabel">Transactions</h1>  
 
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
                          <div className="form-group">
                              <label>Balance ({this.state.balance.currencyCode })</label>
                              <input type="text" name="balance" disabled={true} value={this.state.balance.amount} className="form-control" id="balance" />
                          </div>
                         

                      </div>
                      <span></span>
                  </div>
              </div> 
        {contents}
      </div>
    );
  }

    async populateWeatherData() { 
    const urlData = urlparse(window.location.href, true)
    const response = await fetch(`api/transactions/account/${urlData.query.id}`);
    const data = await response.json();

    const customerResponse = await fetch(`api/customers/${urlData.query.customerID}`);
    const customerData = await customerResponse.json();

    const accountResponse = await fetch(`api/accounts/${urlData.query.id}`);
    const accountData = await accountResponse.json();

        this.setState({
            forecasts: data.data,
            loading: false,
            firstName: customerData.data.firstName,
            balance: accountData.data.account.balance,
            lastName: customerData.data.lastName
        });
    }
}
