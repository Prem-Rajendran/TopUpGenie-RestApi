**# TopUpGenie**
**TopUpGenie** is a user-friendly application designed to streamline mobile top-up transactions, allowing users to efficiently manage their top-up beneficiaries and execute credit transactions for UAE phone numbers. The app supports a variety of top-up options and includes robust security features to ensure that users can safely and easily add credit to their mobile phones.

**Key Features:**
Beneficiary Management: Users can add, view, and manage up to 5 active top-up beneficiaries.
Flexible Top-Up Options: Supports a range of top-up amounts (AED 5 to AED 100).
Monthly Limits: Enforces monthly top-up limits based on user verification status, ensuring compliance with security guidelines.
Transaction Tracking: Provides detailed transaction records with status updates for successful and failed transactions.
Secure Payment Processing: Includes mechanisms to ensure users' balances are accurately debited before top-ups are processed, leveraging external HTTP services for real-time balance and transaction handling.
TopUpGenie is built using ASP.NET Core, following the principles of Clean Architecture, with a focus on maintainability, scalability, and testability. The project is structured to accommodate future enhancements and integrations with external services.

**Tech Stack:**
**Backend:** ASP.NET Core .Net 7
**Database:** SQLite (In-Memory for unit testing), EF Core
**Design Patterns:** Repository Pattern, Unit of Work, 
**Security:** JWT Token based User authentication and authorization mechanisms, with role access control
**Testing:** xUnit for unit and integration tests

<img width="1800" alt="Screenshot 2024-08-08 at 4 31 38 PM" src="https://github.com/user-attachments/assets/6e62129d-653d-47d6-a1e0-97051ade7da1">

