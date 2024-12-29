# MAUVE

MAUVE is a modern e-commerce platform built with .NET Core and Angular, specializing in contemporary streetwear and fashion.

## Tech Stack

### Backend
- .NET 7.0
- Entity Framework Core
- SQL Server
- JWT Authentication
- Clean Architecture

### Frontend
- Angular 16+
- NgRx for state management
- TailwindCSS
- Angular Material

## Project Structure

```
MauveSupply/
├── Mauve.API             # Web API
├── Mauve.Core           # Domain Models & Interfaces
├── Mauve.Infrastructure # Data Access & External Services
└── Mauve.Application    # Business Logic & DTOs
```

## Features

- Product catalog with variant management
- User authentication and authorization
- Shopping cart functionality
- Order processing and tracking
- Supplier integration
- Inventory management
- Secure payment processing
- User wishlist

## Getting Started

### Prerequisites
- .NET 7.0 SDK
- Node.js & npm
- SQL Server
- Visual Studio Code or Visual Studio 2022

### Development Setup

1. Clone the repository
```bash
git clone https://github.com/marvindeckmyn/mauve-backend.git
cd mauve-backend
```

2. Install dependencies
```bash
# Backend
dotnet restore

# Frontend (coming soon)
# cd ClientApp
# npm install
```

3. Update database connection string in `appsettings.json`

4. Run migrations
```bash
cd Mauve.API
dotnet ef database update
```

5. Run the application
```bash
dotnet run
```

## Architecture

The solution follows Clean Architecture principles:

- **Mauve.Core**: Contains all domain models and interfaces
- **Mauve.Application**: Contains business logic and service interfaces
- **Mauve.Infrastructure**: Implements data access and external service integration
- **Mauve.API**: Handles HTTP requests and contains controllers

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details

## Contact

Marvin Deckmyn - [GitHub](https://github.com/marvindeckmyn)