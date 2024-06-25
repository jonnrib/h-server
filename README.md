# HanamiAPI

**HanamiAPI** is a robust and scalable API designed to manage user authentication, posts, and access control for a content management system. Built with ASP.NET Core, Entity Framework, and JWT for secure authentication, this project aims to provide a solid foundation for web applications requiring user management and content publishing features.

## Table of Contents

- [HanamiAPI](#hanamiapi)
  - [Table of Contents](#table-of-contents)
  - [Project Overview](#project-overview)
  - [Features](#features)
  - [Technology Stack](#technology-stack)
  - [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [NuGet Package Installation](#nuget-package-installation)
      - [Using the Visual Studio Interface](#using-the-visual-studio-interface)
      - [Using the Package Manager Console](#using-the-package-manager-console)
    - [Installation](#installation)
    - [Configuration](#configuration)
    - [Running the Application](#running-the-application)
  - [Recommendation for Committing Data in `app.db`](#recommendation-for-committing-data-in-appdb)
  - [API Endpoints](#api-endpoints)
    - [Create a Post](#create-a-post)
    - [List All Posts](#list-all-posts)
    - [Get a Specific Post](#get-a-specific-post)
    - [Update a Post](#update-a-post)
    - [Delete a Post](#delete-a-post)
  - [Database Migrations](#database-migrations)
  - [Contributing](#contributing)
  - [License](#license)

## Project Overview

Hanami API is a robust and scalable web service that allows users to manage posts. It is built with a clean architecture to ensure ease of maintenance and use.

## Features

- **CRUD operations for posts**
- **RESTful endpoints**
- **Database migration management**
- **Dependency injection**
- **Containerization with Docker**
- **User Authentication and Authorization**: Secure registration, login, and logout functionalities with JWT-based authentication.
- **Role-Based Access Control**: Manage different user roles and permissions to ensure secure access to resources.
- **Asynchronous Processing**: Improved performance and reliability with asynchronous operations.
- **Entity Framework Integration**: Utilize Entity Framework for efficient database management and migrations.

## Technology Stack

- **Backend**: ASP.NET Core, Entity Framework Core
- **Authentication**: JWT (JSON Web Tokens)
- **Database**: SQL Server (or any EF Core compatible database)
- **DevOps**: Docker for containerization, GitHub Actions for CI/CD
- **SQLite** (Database)
- **Swagger** (API Documentation)

## Getting Started

### Prerequisites

Before you begin, ensure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQLite](https://www.sqlite.org/download.html)
- [Docker (optional)](https://www.docker.com/)

### NuGet Package Installation

#### Using the Visual Studio Interface

1. Open the NuGet Package Manager in Visual Studio:

   - Right-click on your project in the Solution Explorer.
   - Select "Manage NuGet Packages...".

2. Install the following packages:
   - `Microsoft.EntityFrameworkCore.Sqlite`
   - `Microsoft.EntityFrameworkCore.SqlServer`
   - `Microsoft.EntityFrameworkCore.Tools`
   - `Swashbuckle.AspNetCore`

#### Using the Package Manager Console

1. Open the Package Manager Console:

   - Go to "Tools" > "NuGet Package Manager" > "Package Manager Console".

2. Execute the following commands:
   ```powershell
   Install-Package Microsoft.EntityFrameworkCore.Sqlite -Version 8.0.5
   Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 8.0.5
   Install-Package Microsoft.EntityFrameworkCore.Tools -Version 8.0.5
   Install-Package Swashbuckle.AspNetCore -Version 6.6.2
   ```

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/your-username/hanami-api.git
   cd hanami-api
   ```

2. Install the dependencies:

   ```bash
   dotnet restore
   ```

### Configuration

Ensure you have an `appsettings.json` file in the root of the project with the following content:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=app.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Running the Application

1. Apply the database migrations:

   ```bash
   dotnet ef database update
   ```

2. Run the application:

   ```bash
   dotnet run
   ```

3. Access the API documentation at `http://localhost:8080/swagger`.

## Recommendation for Committing Data in `app.db`

To ensure changes to the `app.db` file are not included in future commits, execute the following command:

```bash
git update-index --assume-unchanged app.db
```

This command makes Git ignore future changes to the `app.db` file in your local repository.

## API Endpoints

### Create a Post

- **URL:** `/api/posts`
- **Method:** `POST`
- **Request Body:**

  ```json
  {
    "title": "Post Title",
    "content": "Post content"
  }
  ```

- **Response:**

  ```json
  {
    "id": 1,
    "title": "Post Title",
    "content": "Post content",
    "createdAt": "2024-05-27T12:34:56Z"
  }
  ```

### List All Posts

- **URL:** `/api/posts`
- **Method:** `GET`
- **Response:**

  ```json
  [
    {
      "id": 1,
      "title": "Post Title",
      "content": "Post content",
      "createdAt": "2024-05-27T12:34:56Z"
    }
  ]
  ```

### Get a Specific Post

- **URL:** `/api/posts/{id}`
- **Method:** `GET`
- **Response:**

  ```json
  {
    "id": 1,
    "title": "Post Title",
    "content": "Post content",
    "createdAt": "2024-05-27T12:34:56Z"
  }
  ```

### Update a Post

- **URL:** `/api/posts/{id}`
- **Method:** `PUT`
- **Request Body:**

  ```json
  {
    "title": "Updated Title",
    "content": "Updated content"
  }
  ```

- **Response:**

  ```json
  {
    "id": 1,
    "title": "Updated Title",
    "content": "Updated content",
    "createdAt": "2024-05-27T12:34:56Z",
    "updatedAt": "2024-05-27T12:45:00Z"
  }
  ```

### Delete a Post

- **URL:** `/api/posts/{id}`
- **Method:** `DELETE`
- **Response:** `204 No Content`

## Database Migrations

To add a new migration:

```bash
dotnet ef migrations add MigrationName
```

To update the database:

```bash
dotnet ef database update
```

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/YourFeature`).
3. Make your changes.
4. Commit your changes (`git commit -m "Add some feature"`).
5. Push to the branch (`git push origin feature/YourFeature`).
6. Create a new Pull Request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.
