# LibraryAndSalesPlatform

**LibraryAndSalesPlatform** is a comprehensive library sales solution designed to provide enterprise-grade RESTful services using ASP.NET WebAPI and C#. This project offers a robust framework for managing various library operations.

## Features

- **CRUD Operations**: Full CRUD operations for Users, Books, Shares, Shelves, Notes, FriendShips, OperationClaims, UserOperationClaims.
- **Authorization**: Authorization is required for add, update, and delete operations. Users must be logged in and have appropriate claims.
- **Database Integration**: Works seamlessly with a real MSSQL database.
- **EntityFramework**: Includes all necessary EntityFramework files.
- **Core Layer**: Integrated core layer for better modularity.
- **Dependency Injection**: Uses Autofac for IoC.
- **JWT Authentication**: JWT-based authentication for secure API access.
- **Logging**: Implemented logging with Serilog.
- **Object Mapping**: Utilizes AutoMapper for efficient object mapping.
- **Validation**: Login validation implemented using Fluent Validation.
- **Testing**: Comprehensive unit and integration tests with XUnit.

## Project Structure

- **IEntity, IDto, IEntityRepository, EfEntityRepositoryBase**: Core abstractions and base classes for repository patterns.
- **WebAPI**: Implements RESTful services.
- **JWT**: Secure token-based authentication.
- **IoC, Interceptors**: Configured dependency injection and method interceptors using Autofac.
- **SOLID Principles**: Ensures adherence to SOLID principles for maintainable and scalable code.
- **Clean Coding**: Focuses on clean, readable, and maintainable code.
- **DRY (Don't Repeat Yourself)**: Avoids redundancy and promotes reusability.

## Design Principles

When developing this project, the following principles were strictly adhered to:
- **SOLID Principles**: For maintainable and scalable code architecture.
- **Clean Coding**: Ensures readability and ease of maintenance.
- **DRY (Don't Repeat Yourself)**: Promotes code reuse and reduces redundancy.

## Getting Started

To get started with the project, follow these steps:

1. Clone the repository:
   ```sh
   git clone https://github.com/yourusername/LibraryAndSalesPlatform.git
