# RealEstateApi

### Overview

RealEstateAPI is a RESTful API designed to manage property listings, images, pricing, and user authentication for a real estate platform. The API allows users to create and update property details, list properties with filtering options, manage property images, and authenticate users with JWT (JSON Web Tokens). The API follows a hexagonal architecture, promoting separation of concerns and scalability.

## Features

- Property Management: Create, update, and manage properties, including adding images and changing prices.
- User Authentication: Register and log in users with JWT for secure API access.
- Filtering and Pagination: List properties with filters like price range and pagination support.
- Security Enhancements: Secure the API using HTTPS, JWT authentication, strong password hashing, and more.

## Technologies

- .NET 8.0
- SQL Server
- Entity Framework Core
- JWT Authentication
- NUnit & Moq (for unit testing)
- In-Memory Database (for testing)

## Hexagonal Architecture

The project is structured following the hexagonal architecture pattern (also known as Ports and Adapters), which ensures better modularity and separation of concerns:

- Domain Layer: Contains core entities like - Property, User, and interfaces for repositories.
- Application Layer: Implements business logic and service methods.
- Infrastructure Layer: Handles external services such as database access (via Entity Framework Core) and JWT token generation.
- API Layer: Exposes the API endpoints through controllers

# Setup Instructions

Prerequisites

- .NET 8.0 SDK
- SQL Server (or use Docker for SQL Server)

## Installation

1. Clone the repository

```bash
  git clone https://github.com/Carl0sL0pez03/RealEstateApi
  cd RealEstateApi
```

2. Install dependencies

```bash
  dotnet restore
```

3. Configure the database

```bash
  "ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=RealEstateDb;User Id=yourusername;Password=yourpassword;"
}
```

4. Perform the import of the database backup.

```bash
 Import the Sql mangament (Back up -> RealEstate.bak).
```

5. Set up the database

```bash
dotnet ef database update
```

6. Running the Application

```bash
dotnet run
```

7. Running Tests

```bash
dotnet test
```

## API Reference

### User Authentication

#### Log in and get a JWT token

```http
  POST /api/auth/login
```

| Parameter  | Type     | Description  |
| :--------- | :------- | :----------- |
| `username` | `string` | **Required** |
| `password` | `string` | **Required** |

#### Register a new user.

```http
  POST /api/auth/register
```

| Parameter  | Type     | Description  |
| :--------- | :------- | :----------- |
| `username` | `string` | **Required** |
| `password` | `string` | **Required** |

### Property Management

#### Create a new property

```http
  POST /api/properties
```

| Parameter      | Type      | Description   |
| :------------- | :-------- | :------------ |
| `Name`         | `string`  | **Required**. |
| `Address`      | `string`  | **Optional**. |
| `price`        | `decimal` | **Optional**. |
| `CodeInternal` | `string`  | **Optional**. |
| `Year`         | `int`     | **Optional**. |
| `idOwner`      | `int`     | **Required**. |

#### Update an existing property

```http
  PUT /api/properties/{id}
```

| Parameter      | Type      | Description   |
| :------------- | :-------- | :------------ |
| `idProperty`   | `string`  | **Required**. |
| `Name`         | `string`  | **Required**. |
| `Address`      | `string`  | **Optional**. |
| `price`        | `decimal` | **Optional**. |
| `CodeInternal` | `string`  | **Optional**. |
| `Year`         | `int`     | **Optional**. |
| `idOwner`      | `int`     | **Required**. |

#### List properties with optional filters and pagination

```http
  GET /api/properties
```

| Parameter    | Type     | Description   |
| :----------- | :------- | :------------ |
| `filterName` | `string` | **Required**. |
| `minPrice`   | `int`    | **Optional**. |
| `maxPrice`   | `int`    | **Required**. |

#### Add an image to a property.

```http
  POST /api/properties/{id}/add-image
```

| Parameter    | Type      | Description   |
| :----------- | :-------- | :------------ |
| `IdProperty` | `int`     | **Required**. |
| `File`       | `string`  | **Required**. |
| `Enabled`    | `boolean` | **Required**. |

#### Change the price of a property.

```http
  POST /api/properties/{id}/change-price
```

| Parameter | Type  | Description   |
| :-------- | :---- | :------------ |
| `Price`   | `int` | **Required**. |

## Security Measures

- JWT Authentication: All endpoints are protected using JWT, with token expiration and signature validation.
- HTTPS: The application enforces HTTPS to secure communication between the client and server.
- Password Hashing: User passwords are stored securely using hashed and salted values.
- SQL Injection Prevention: Queries are handled via Entity Framework Core, which uses parameterized queries to avoid SQL injection attacks.
- CSRF Protection: Consider adding CSRF tokens if using cookies for authentication.
