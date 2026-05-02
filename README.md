# E-Commerce API

A modern, enterprise-grade **E-Commerce REST API** built with ASP.NET Core 8.0 following Clean Architecture principles. This API provides comprehensive functionality for managing products, shopping carts, orders, and payments with advanced features like caching, JWT authentication, and Stripe payment integration.

## Features

### Core Functionality

- **Product Management** - Browse, search, filter, and sort products by categories, brands, and types
- **Shopping Cart** - Add, update, and manage items in a shopping basket
- **Order Processing** - Create, retrieve, and manage customer orders
- **Payment Integration** - Secure payment processing via Stripe
- **User Authentication** - JWT-based authentication and authorization
- **Caching** - Redis caching for improved performance
- **Pagination** - Efficient data retrieval with pagination support
- **Specifications** - Advanced filtering and search capabilities

### Technical Features

- Clean Architecture with SOLID principles
- Repository Pattern for data access
- Unit of Work pattern for transaction management
- Dependency Injection
- Global exception handling middleware
- CORS support
- Swagger/OpenAPI documentation
- Environment-specific configurations

## Architecture

The project follows **Clean Architecture** with separation of concerns across multiple layers:

```
E-Commerce/
├── Core/
│   ├── Domain/              # Entities, DTOs, Contracts, and Business Rules
│   ├── Services/            # Business logic and service implementations
│   └── Services.Abstraction/# Service interfaces and abstractions
├── Infrastructure/
│   ├── Persistence/         # Database repositories and data access
│   └── Presentation/        # API Controllers
├── Shared/                  # Common DTOs, utilities, and constants
└── E-Commerce.API/          # Main API entry point and configuration
```

### Projects

| Project                | Purpose                                                       |
| ---------------------- | ------------------------------------------------------------- |
| `Domain`               | Core entities, business rules, and repository interfaces      |
| `Services`             | Business logic and service implementations                    |
| `Services.Abstraction` | Service contracts and abstractions                            |
| `Persistence`          | Database repositories and Entity Framework Core configuration |
| `Presentation`         | API controllers and request handling                          |
| `Shared`               | Common DTOs, enums, error models, and utilities               |
| `E-Commerce.API`       | Main ASP.NET Core application and dependency injection setup  |

## Technology Stack

### Framework & Runtime

- **ASP.NET Core 8.0**
- **.NET 8.0**

### Database

- **SQL Server** - Primary database for entities and identity
- **Entity Framework Core** - ORM for database operations

### Caching

- **Redis** - Distributed caching for performance optimization

### Authentication & Security

- **JWT (JSON Web Tokens)** - Token-based authentication
- **Identity Framework** - User management and claims-based authorization

### Payment Processing

- **Stripe** - Payment gateway integration for secure transactions

### API Documentation

- **Swagger/OpenAPI** - Interactive API documentation

### Architecture Patterns

- Clean Architecture
- Repository Pattern
- Unit of Work Pattern
- Dependency Injection

## Prerequisites

- **.NET 8.0 SDK** or later
- **SQL Server** (local or remote)
- **Redis Server** (for caching)
- **Visual Studio 2022** or **Visual Studio Code** with C# extensions

## Getting Started

### 1. Clone the Repository

```bash
git clone <repository-url>
cd E-Commerce
```

### 2. Install Dependencies

```bash
dotnet restore
```

### 3. Update Connection Strings

Edit `E-Commerce.API/appsettings.json` with your local database and Redis configuration:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=ECommerceAPI;Trusted_Connection=True;TrustServerCertificate=True",
    "IdentityConnection": "Server=YOUR_SERVER;Database=ECommerceAPI.Identity;Trusted_Connection=True;TrustServerCertificate=True",
    "RedisConnection": "your_redis_host:port"
  }
}
```

### 4. Apply Database Migrations

```bash
dotnet ef database update --project Infrastructure/Persistence/Persistence.csproj --startup-project E-Commerce.API
```

### 5. Run the Application

```bash
cd E-Commerce.API
dotnet run
```

The API will be available at `https://localhost:7162` (or the configured URLS in appsettings.json).

## Configuration

### appsettings.json

Key configuration sections:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "SQL Server connection string",
    "IdentityConnection": "SQL Server identity database connection",
    "RedisConnection": "Redis connection string"
  },
  "JwtOptions": {
    "Issuer": "Token issuer URL",
    "Audience": "Intended audience",
    "SecretKey": "Secret key for token signing",
    "ExpirationInDays": 30
  },
  "StripeSettings": {
    "SecretKey": "Stripe secret API key",
    "EndPointSecret": "Stripe webhook endpoint secret"
  },
  "URLS": {
    "BaseUrl": "API base URL",
    "FrontUrl": "Frontend application URL"
  }
}
```

### Logging

Configure logging levels in the `Logging` section of appsettings.json:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## API Endpoints

### Products

| Method | Endpoint               | Description                          |
| ------ | ---------------------- | ------------------------------------ |
| GET    | `/api/products`        | Get all products (paginated, cached) |
| GET    | `/api/products/{id}`   | Get product by ID                    |
| GET    | `/api/products/brands` | Get all brands                       |
| GET    | `/api/products/types`  | Get all product types                |

### Authentication

| Method | Endpoint             | Description       |
| ------ | -------------------- | ----------------- |
| POST   | `/api/auth/register` | Register new user |
| POST   | `/api/auth/login`    | User login        |

### Basket

| Method | Endpoint           | Description             |
| ------ | ------------------ | ----------------------- |
| GET    | `/api/basket`      | Get user's basket       |
| POST   | `/api/basket`      | Add/Update basket items |
| DELETE | `/api/basket/{id}` | Remove item from basket |

### Orders

| Method | Endpoint           | Description       |
| ------ | ------------------ | ----------------- |
| GET    | `/api/orders`      | Get user's orders |
| GET    | `/api/orders/{id}` | Get order details |
| POST   | `/api/orders`      | Create new order  |

### Payments

| Method | Endpoint                | Description            |
| ------ | ----------------------- | ---------------------- |
| POST   | `/api/payments/intent`  | Create payment intent  |
| POST   | `/api/payments/webhook` | Handle Stripe webhooks |

## Authentication

The API uses **JWT (JSON Web Tokens)** for authentication:

1. Register or login to obtain a token
2. Include the token in the `Authorization` header:

```
Authorization: Bearer <your_jwt_token>
```

## Database Schema

### Main Entities

**Products Module**

- Product
- Brand
- ProductType
- ProductImage

**Basket Module**

- Basket
- BasketItem

**Order Module**

- Order
- OrderItem
- DeliveryMethod

**Identity Module**

- User
- Role

## Caching Strategy

The API implements Redis caching for improved performance:

- **Product listings** - Cached with RedisCache attribute
- **Cache expiration** - Configurable per endpoint
- **Cache invalidation** - Automatic on data modifications

Example:

```csharp
[RedisCache]
[HttpGet]
public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProductsAsync(...)
```

## Build & Publish

### Development Build

```bash
dotnet build
```

### Release Build

```bash
dotnet build -c Release
```

### Publish

```bash
dotnet publish -c Release -o ./publish
```

## Testing

To run tests (if test projects are available):

```bash
dotnet test
```

## Project Structure Details

### Core Layer

- **Domain**: Business entities, value objects, and repository interfaces
- **Services**: Business logic, DTOs, and service contracts
- **Services.Abstraction**: Service interfaces for dependency injection

### Infrastructure Layer

- **Persistence**: Entity Framework Core DbContext, repositories, specifications, and database configurations
- **Presentation**: API controllers handling HTTP requests

### Shared Layer

- Common DTOs for all modules
- Enums and error models
- Pagination utilities
- JWT configuration

## Middleware Pipeline

The application uses the following middleware:

1. **Global Exception Handling** - Catches and formats all exceptions
2. **HTTPS Redirection** - Forces HTTPS in production
3. **Static Files** - Serves static content
4. **CORS** - Cross-Origin Resource Sharing
5. **Authentication** - JWT validation
6. **Authorization** - Role-based access control
7. **Swagger** - API documentation (Development only)

## Database Seeding

The application automatically seeds the database with initial data on startup:

```csharp
await app.SeedDatabaseAsync();
```

## Environment-Specific Settings

Development configuration includes Swagger documentation for API exploration.

Switch between Development and Production:

```bash
export ASPNETCORE_ENVIRONMENT=Production  # Linux/Mac
set ASPNETCORE_ENVIRONMENT=Production     # Windows
```

## Troubleshooting

### Database Connection Issues

- Verify SQL Server is running
- Check connection strings in appsettings.json
- Ensure database credentials are correct

### Redis Connection Issues

- Verify Redis server is running
- Check RedisConnection string in appsettings.json
- Ensure Redis is accessible on the configured host/port

### Migration Issues

```bash
# Check migration status
dotnet ef migrations list

# Remove last migration
dotnet ef migrations remove

# Create new migration
dotnet ef migrations add MigrationName
```

## Contributing

1. Create a feature branch: `git checkout -b feature/feature-name`
2. Commit changes: `git commit -am 'Add feature'`
3. Push to branch: `git push origin feature/feature-name`
4. Submit a Pull Request

---

**Last Updated**: 2026  
**Version**: 1.0.0
