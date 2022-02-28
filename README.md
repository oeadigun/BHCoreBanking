# BHCoreBanking 
The BH CoreBanking Service is a simple Single page application that allows its users to create customers, open accounts, and view transactions.

The project has 2 main parts:
 
#### SPA React front-end (https://localhost:44355/)

A single page application made with React, TypeScript and Webpack.

#### Api (https://localhost:44355/)

Based off the `dotnet new webapi` template. The API has 3 main controllers, Account, Customer and Transaction Controllers  
Account Controller - Responsible for account opening, and account verification  
Customer Controller - Responsible for customer creation and verification  
Transaction Controller - Responsible for view transaction status


### Project Setup Requirements
Dotnet Core 5.0 SDK  
Node.js v12.16.1 or later  
Visual Studio 16.11.3 or later   

### Running the project
- Clone the repository
- Restore nuget packages
- Run Npm to install javascript packages
- Build Solution 
- Run with IIS Express or Locally hosted IIS Instance

### Project Goals
The goal is to ensure we can open new accounts as well as do some form of initial deposit into the newly created accounts. This will be done by using the UI to create new customers
and then creating accounts for the newly created customer.

There is a default account that is currently hard-coded to provide some initial funds, say from a suspense or vault, but can be replaced.

### Solution Considerations
- Unit Tests were written for the back-end code with code coverage above `80%` and minimum cyclomatic complexity
- Microsoft recommended coding conventions were used (https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Project structure was divided into separate logical units into `Core`, `Data`, `Services` and `API`

### Credits 
* [Dotnet core templates](https://github.com/aspnet/JavaScriptServices)  



