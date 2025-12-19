# Asteroids Game & Backend API

A full-stack project featuring a classic Asteroids game with a persistent backend for user accounts, a shop system, and data analysis.

## 🚀 Features

- **Classic Asteroids Gameplay**: Fly, shoot, and survive waves of asteroids.
- **User System**: Secure registration and login (JWT Authentication).
- **Economy**: Earn credits by passing waves and spend them in the shop.
- **Shop System**: Buy weapons and power-ups (persisted in database).
- **Data Analysis**: Backend endpoints to analyze game item statistics.
- **RESTful API**: Built with ASP.NET Core 9.0.

## 🛠️ Tech Stack

### Backend
- **Framework**: ASP.NET Core 9.0 (Web API)
- **Database**: SQLite (Entity Framework Core)
- **Authentication**: JWT (JSON Web Tokens) & BCrypt for password hashing
- **Documentation**: Swagger UI

### Frontend
- **Core**: HTML5 Canvas, Vanilla JavaScript
- **Styling**: CSS3
- **Communication**: Fetch API

## 📋 Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- A modern web browser
- (Optional) VS Code with "Live Server" extension

## 🔧 Getting Started

### 1. Setup the Backend

1.  Navigate to the backend folder:
    ```bash
    cd asteroidsbackend
    ```
2.  Restore dependencies and run the server:
    ```bash
    dotnet run
    ```
    *The server will start on `http://localhost:5000`.*
    *The SQLite database (`asteroids.db`) will be created automatically on the first run.*

### 2. Setup the Frontend

1.  Go to https://github.com/ecleesam/ecleesam.github.io
1.  Open the `FrontEnd` folder.
2.  Open `index.html` in your browser.
    *   **Recommended**: Use a local development server (like VS Code's "Live Server") to avoid CORS issues with file protocols.
    *   If using VS Code Live Server, right-click `index.html` and select "Open with Live Server".

## 🎮 How to Play

1.  **Register/Login**: Use the panel on the left to create an account.
2.  **Controls**:
    - `W` / `↑`: Thrust
    - `A` / `←`: Rotate Left
    - `D` / `→`: Rotate Right
    - `Space`: Fire
    - `P`: Pause
3.  **Shop**:
    - Earn credits by clearing waves (100 credits per wave).
    - Click "Load Shop" to see items.
    - Buy items to upgrade your ship (logic for equipping items is handled in backend inventory).

## 📚 API Documentation

When the backend is running, you can explore the full API documentation via Swagger UI:
- URL: [http://localhost:5000/swagger](http://localhost:5000/swagger)

### Key Endpoints
- `POST /api/auth/register`: Create a new user.
- `POST /api/auth/login`: Authenticate and get a JWT token.
- `GET /api/items`: List all available shop items.
- `POST /api/shop/buy/{itemId}`: Purchase an item.
- `GET /api/analysis/top-weapons`: View game statistics.

## 📂 Project Structure

```
Asteroids-backend/
├── asteroidsbackend/       # ASP.NET Core Web API
│   ├── Controllers/        # API Endpoints
│   ├── Data/               # Database Context & Repositories
│   ├── Models/             # Data Entities
│   ├── Services/           # Business Logic
│   └── Program.cs          # App Configuration
├── FrontEnd/               # Game Client
│   ├── index.html          # Entry point
│   ├── script.js           # Game Loop & Logic
│   └── api.js              # API Communication
└── AsteroidsBackend.Tests/ # Unit Tests
```

## 🧪 Running Tests

To run the backend unit tests:

```bash
dotnet test
```
