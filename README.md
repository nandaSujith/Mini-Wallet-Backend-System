# Mini Wallet Backend System

## Project Overview

MiniWallet is a backend wallet management system developed using **ASP.NET Core Web API** following **Clean Architecture principles**.

The system provides functionality for:

* User registration
* Wallet creation
* Wallet balance management
* Money transfer between wallets
* Transaction history tracking

The application uses **Entity Framework Core** with **SQL Server** for database operations.

---

# Technology Stack

* ASP.NET Core Web API
* C#
* Entity Framework Core
* SQL Server
* LINQ
* Swagger UI
* Clean Architecture
* Repository Pattern
* Unit of Work Pattern

---

# Solution Architecture

The project follows Clean Architecture:

```
MiniWallet

├── MiniWallet.API
│   └── Controllers
│   └── Swagger Configuration
│
├── MiniWallet.Application
│   └── Business Logic
│   └── DTOs
│   └── Interfaces
│
├── MiniWallet.Domain
│   └── Entities
│   └── Enums
│
└── MiniWallet.Infrastructure
    └── Database Context
    └── Repository Implementation
    └── SQL Configuration
```

---

# How to Run the Project Locally

## Prerequisites

Install the following:

* Visual Studio 2022
* .NET SDK
* SQL Server
* SQL Server Management Studio (SSMS)

---

## Steps to Run

1. Clone the repository:

```
git clone https://github.com/nandaSujith/Mini-Wallet-Backend-System
```

2. Open the solution:

```
MiniWallet.sln
```

using Visual Studio.

3. Restore NuGet packages:

```
Build → Restore NuGet Packages
```

4. Update the database connection string in:

```
MiniWallet.API/appsettings.json
```

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=MiniWalletDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

5. Set:

```
MiniWallet.API
```

as the startup project.

6. Run the application:

```
ctrl + F5
```

---

# Database Setup

The project uses SQL Server as the database.

The database script was created using SQL Server Management Studio (SSMS) and is included in the project repository.

Database script location:

Database/MiniWallet_DatabaseScript.sql
Steps to Create Database
Open SQL Server Management Studio (SSMS).
Open the file:
migration.sql

from the project folder.

Execute the SQL script.
The script will create the required database structure including:
Database
Tables
Primary Keys
Foreign Keys
Constraints
Relationships
After successful execution, update the connection string in:
MiniWallet.API/appsettings.json
Run the API project.

# Swagger API Documentation

Swagger UI is enabled for API testing.

After running the project, open:

```
[https://localhost:<port>/swagger/index.html](https://localhost:7257/swagger/index.html)
```



Swagger provides interactive documentation for all available APIs.

---

## API List
GET APIs

The available APIs can be tested through Swagger UI.

Example:

GET /api/users
GET /api/wallets/{id}
GET /api/transactions/{walletId}

## Get Wallet Balance

**GET**

```
/api/wallets/{walletId}/balance
```

Returns the current wallet balance.

---

## Transfer Money

**POST**

```
/api/transactions/transfer
```

Transfers money between two wallets.

---

## Get Transaction History

**GET**

```
/api/transactions/{walletId}
```

Returns wallet transaction details.

---

# Sample API Requests

## Register User

Request:

```json
{
  "name": "John Doe",
  "email": "john@gmail.com",
  "phone": "9876543210",
  "password": "Password@123",
  "initialBalance": 1000
}
```

---

## Transfer Money

Request:

```json
{
  "fromWalletId": 1,
  "toWalletId": 2,
  "amount": 500
}
```

---

# Assumptions

* Each user has only one wallet.
* Email and phone number are unique.
* Each transaction is stored permanently for auditing.
* Wallet balance cannot become negative.
* Decimal data type is used for monetary values.
* SQL Server is used as the database.
* Authentication is simplified for this assignment.
* Transactions are processed synchronously.
* Invalid transactions are rejected with proper error responses.

---

# Duplicate Transaction Handling

Duplicate transactions are handled using transaction validation.

Approach:

* Every transaction receives a unique transaction identifier.
* Duplicate transaction requests with the same identifier can be rejected.
* Transaction records are stored before completing the operation.
* Database constraints help prevent duplicate records.

---

# Concurrent Debit / Transfer Request Handling

To handle concurrent wallet operations:

* Database transactions are used to maintain consistency.
* Balance validation happens before debit operation.
* Entity Framework transaction handling ensures atomic updates.
* The system prevents partial updates where money is deducted from one wallet but not added to another.

Future improvements:

* Implement optimistic concurrency using RowVersion.
* Use distributed locking for high-volume transactions.

---

# Negative Balance Prevention

Negative wallet balances are prevented by:

* Checking available balance before debit.
* Rejecting transactions when balance is insufficient.
* Using database transaction rollback if any operation fails.

Example:

```
Current Balance: 500

Transfer Request: 1000

Result:
insufficient balance.
```

---

# Performance Optimizations Applied

The following optimizations were considered:

* Repository pattern reduces duplicate database logic.
* Async/Await used for database operations.
* Proper Entity Framework queries are used.
* Database indexes can be applied on:

  * Email
  * Phone Number
  * WalletId
  * Transaction Date

Additional improvements:

* Use AsNoTracking() for read-only queries.
* Implement pagination for transaction history.
* Add caching for frequently accessed wallet balances.
* Optimize complex queries using stored procedures.

---

# Production Scaling Approach

## Application Scaling

* Deploy using Docker containers.
* Host API in cloud platforms such as Azure/AWS.
* Use multiple API instances behind a load balancer.
* Implement auto-scaling based on traffic.

---

## Database Scaling

* Add proper indexing.
* Use database replication.
* Optimize queries.
* Implement backup and recovery strategies.
* Partition large transaction tables.

---

## Security Improvements

* Add JWT authentication.
* Implement role-based authorization.
* Store secrets securely.
* Enable HTTPS.
* Add API rate limiting.

---

## Monitoring and Reliability

* Add centralized logging.
* Implement health checks.
* Add monitoring tools.
* Configure automated backups.
* Setup alerts for failures.

---

# Improvements Possible With More Time

Future enhancements:

* JWT authentication and authorization.
* User login functionality.
* Unit testing.
* Integration testing.
* Global exception handling middleware.
* Transaction notifications.
* Audit logging.
* Advanced reporting dashboard.

---

# Conclusion

MiniWallet demonstrates a scalable wallet management API built using ASP.NET Core Web API and Clean Architecture principles with proper separation of concerns, database handling, and transaction safety.
