# TopUpGenie
**TopUpGenie** is a user-friendly application designed to streamline mobile top-up transactions, allowing users to efficiently manage their top-up beneficiaries and execute credit transactions for UAE phone numbers. The app supports a variety of top-up options and includes robust security features to ensure that users can safely and easily add credit to their mobile phones.

**Key Features:**
- Beneficiary Management: Users can add, view, and manage up to 5 active top-up beneficiaries.
- Flexible Top-Up Options: Supports a range of top-up amounts (AED 5 to AED 100).
- Monthly Limits: Enforces monthly top-up limits based on user verification status, ensuring compliance with security guidelines.
- Transaction Tracking: Provides detailed transaction records with status updates for successful and failed transactions.
- Secure Payment Processing: Includes mechanisms to ensure users' balances are accurately debited before top-ups are processed, leveraging external HTTP services for real-time balance and transaction handling.
- TopUpGenie is built using ASP.NET Core, following the principles of Clean Architecture, with a focus on maintainability, scalability, and testability. The project is structured to accommodate future enhancements and integrations with external services.

**Tech Stack:**
**Backend:** ASP.NET Core .Net 7
**Database:** SQLite (In-Memory for unit testing), EF Core
**Design Patterns:** Repository Pattern, Unit of Work, 
**Security:** JWT Token based User authentication and authorization mechanisms, with role access control
**Testing:** xUnit for unit and integration tests

<img width="1800" alt="Screenshot 2024-08-08 at 4 31 38 PM" src="https://github.com/user-attachments/assets/6e62129d-653d-47d6-a1e0-97051ade7da1">

![image](https://github.com/user-attachments/assets/20e865fa-d249-4256-a20b-50df1779f6a0)


## How to Use
To clone and set up **TopUpGenie** on your local machine, follow these steps:

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/TopUpGenie.git
cd TopUpGenie-RestApi
```

### 2. Database Setup
- Ensure you have the .NET SDK installed.
- Navigate to the TopUpGenie.DataAccess directory.
- Run the following commands to apply migrations and update the database:

```bash
dotnet ef migrations add InitialCreate --project ../TopUpGenie.DataAccess --startup-project ../TopUpGenie.RestApi
dotnet ef database update --project ../TopUpGenie.DataAccess --startup-project ../TopUpGenie.RestApi
```

### 3. Run the Application
- Open the solution in Visual Studio or your preferred IDE.
- Set the TopUpGenie.RestApi project as the startup project.
- Build and run the application.

### 4. Testing
- Unit tests can be run using:

```bash
dotnet test
```


## Code Coverage
### 1. Data Layer Integration Testing - Code Coverage 

![image](https://github.com/user-attachments/assets/17565509-e668-4b71-b93e-490f2cda0449)


### 2. Service Layer Integration Testing - Code Coverage

![image](https://github.com/user-attachments/assets/585891ec-c3cf-4734-af03-359a0452c4fd)

### 3. Service Layer Unit Testing - Code Coverage

![image](https://github.com/user-attachments/assets/9cfa39f4-f6ed-4f60-88a4-9d6931a84141)


