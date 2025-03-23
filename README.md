# 📝 MyTodo API

A simple REST API for managing a To-Do list built with **ASP.NET Core**.

## 📦 Features
- Create, read, update, and delete (CRUD) to-do items.
- Supports filtering and sorting.
- Authentication with JWT (Username: `admin`, Password: `admin`).

## 🚀 Prerequisites
Ensure you have the following installed on your machine:

- **.NET SDK** (version 8.0 or later) – [Download](https://dotnet.microsoft.com/download)
- **Visual Studio** 
- **Git** – [Download](https://git-scm.com/downloads)
- **Postman** or **Swagger** – to test the API

## 📂 Project Structure
```
MyTodoApi/
├── MyTodoApi.sln                 # Solution file
├── MyTodoApi/                    # Main API project
│    ├── Controllers/             # API controllers
│    ├── Models/                  # Data models
│    ├── Services/                # Business logic
│    ├── Program.cs               # Entry point
│    └── appsettings.json         # Configuration
└── .gitignore                    # Ignored files
```

## ▶️ Running the Application

1. **Clone the Repository**

2. **Open in Visual Studio**

- Open `MyTodoApi.sln` in Visual Studio.
- Ensure dependencies are restored.
- Click the "Run" button or press `F5` to start the application.

The API should now be running at: [https://localhost:5001](https://localhost:5001)

## 📊 Testing the API

You can interact with the API using **Swagger** or **Postman**:

Swagger UI: [https://localhost:5001/swagger](https://localhost:5001/swagger)

### 📌 Endpoints Overview

1. **User Authentication**

- **Login**: `POST /api/auth/login`

  Request Body:
  ```json
  {
    "username": "admin",
    "password": "admin"
  }
  ```

  Response:
  ```json
  {
    "token": "your.jwt.token"
  }
  ```

2. **To-Do Items**

- **Get All To-Do Items**: `GET /api/todo`

  Supports optional filters and sorting:
  
  Filters:
  - `nameFilter`: Filter by name (e.g., `nameFilter=groceries`)
  - `priorityFilter`: Filter by priority (e.g., `priorityFilter=1`)
  - `statusFilter`: Filter by completion status (e.g., `statusFilter=true`)
  - `dueDateFilter`: Filter by due date (e.g., `dueDateFilter=2023-12-31`)

  Sorting:
  - `nameSort`: Sort by name (`asc` or `desc`)
  - `prioritySort`: Sort by priority (`asc` or `desc`)
  - `dueDateSort`: Sort by due date (`asc` or `desc`)

- **Get To-Do Item by ID**: `GET /api/todo/{id}`

- **Create a To-Do Item**: `POST /api/todo`

  No need to provide the `id` as it will be auto-generated.

  Request Body:
  ```json
  {
    "name": "Buy groceries",
    "priority": 1,
    "isCompleted": false,
    "dueDate": "2023-12-31"
  }
  ```

- **Update a To-Do Item**: `PUT /api/todo/{id}`

- **Delete a To-Do Item**: `DELETE /api/todo/{id}`

## 🔐 Authentication

1. Obtain a JWT token via the login endpoint:

```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "admin"
}
```

2. Include the token in the `Authorization` header for protected routes:

```
Authorization: Bearer YOUR_TOKEN_HERE
```

### 🔒 Protected Routes
All routes are protected except for:
- `GET /api/todo` (Get all to-do items)
- `GET /api/todo/{id}` (Get a specific to-do item)

These routes can be accessed without authentication for read-only purposes.

## 📌 Important Note
This application does not integrate with a database. All data is stored in memory and will be lost once the application is stopped.
