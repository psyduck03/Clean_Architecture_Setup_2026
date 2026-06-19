# Clean Architecture Setup 2025

This repository provides a modern and modular Clean Architecture structure that you can use as a starting point for your projects in 2025.

---

## Project Overview

### Architectural Structure
- **Architectural Pattern**: Clean Architecture
- **Design Patterns**:
  - Repository Pattern
  - UnitOfWork Pattern
  - Result Pattern
  - CQRS Pattern

### Libraries Used
- **EntityFrameworkCore**: For ORM (Object-Relational Mapping).
- **FluentValidation**: For validation processes.
- **MediatR**: For CQRS and messaging operations.
- **JWT Bearer**: For token-based authentication and authorization.
- **BCrypt.Net**: For secure password hashing.

### Authentication
JWT-based authentication is implemented:
1. Register a user via `POST /auth/register`.
2. Obtain a token via `POST /auth/login`.
3. Send it as a `Bearer` token to access protected endpoints (e.g. `GET /api/Employee`).

> Configure the `Jwt` section in `appsettings.json` (issuer, audience, secret key, expiry). Use a strong secret key in production.

### Additional Features (Planned)
- **AutoMapper**: For simplifying object-to-object mapping between layers. (Planned for future implementation)

---

## Getting Started

Follow these steps to set up and run the project:

### Prerequisites
- [.NET SDK 9.0](https://dotnet.microsoft.com/download) installed on your machine.
- A SQL Server instance for the database.

### Installation Steps and Configuration 

1. **Clone the Repository**:
```bash
git clone https://github.com/psyduck03/Clean_Architecture_Setup_2025.git
```
2. **Navigate to the Project Directory**:
```bash
cd Clean_Architecture_Setup_2025
```
3. **Configure the Database**:
   - Open the `appsettings.json` file in the `CleanArch25.API` project.
   - Update the connection string to match your database configuration:
```json
"ConnectionStrings": {
    "SqlServer": "Server=localhost,1433;Database=CleanArch25Db;User Id=sa;Password=YourPassword;TrustServerCertificate=True;"
}
```
1. **Restore Dependencies**:
```bash
dotnet restore
```
1. **Apply Migrations**:
   - From the solution root, create and apply the initial migration:
```bash
dotnet ef migrations add InitialCreate --project src/CleanArch25.Infrastructure --startup-project src/CleanArch25.API
dotnet ef database update --project src/CleanArch25.Infrastructure --startup-project src/CleanArch25.API
```
1. **Build the Project**:
```bash
dotnet build
```
1. **Run the Application**:
```bash
dotnet run
```
1. **Access the API**:
   - Open your web browser and navigate to `http://localhost:5149/scalar/v1` to access the API.

---

## Project Structure

The project is organized into the following layers:

1. **API Layer**: Handles HTTP requests and responses.
2. **Application Layer**: Contains business logic, CQRS handlers, and validation.
3. **Domain Layer**: Includes core entities, value objects, and domain logic.
4. **Infrastructure Layer**: Manages database access, repositories, and external services.

---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---
