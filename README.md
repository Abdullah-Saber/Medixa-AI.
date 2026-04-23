# Medixa AI - Full Stack Application

🩸 Blood test booking system with AI-powered interpretation

## Architecture

This project follows **Clean Architecture** principles with a full-stack solution:

### Backend (.NET 8)
- **Domain Layer**: Core business entities and enums
- **Application Layer**: Business logic, services, DTOs, dependency injection
- **Infrastructure Layer**: Data access (Entity Framework Core), migrations
- **API Layer**: Web API with Swagger documentation and CORS for React frontend

### Frontend (React 19 + Vite)
- Modern React application with Vite
- Tailwind CSS for styling
- i18next for internationalization
- React Router for navigation
- Context API for state management

## Project Structure

```
Medixa-AI/
├── backend/
│   ├── Medixa-AI.Domain/          # Core entities and enums
│   ├── Medixa-AI.Application/     # Business logic and services
│   ├── Medixa-AI.Infrastructure/  # Data access and migrations
│   └── Medixa-AI.Api/             # Web API (Controllers, Swagger)
├── src/                            # React frontend
│   ├── components/
│   ├── pages/
│   ├── context/
│   └── i18n/
├── docs/                           # Documentation
├── design/                         # Wireframes and designs
└── Medixa-AI.sln                   # .NET solution file
```

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- Node.js 18+
- SQL Server (or LocalDB)

### Backend Setup

1. Navigate to backend directory:
```bash
cd backend
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Update connection string in `backend/Medixa-AI.Api/appsettings.json`

4. Run migrations:
```bash
cd Medixa-AI.Infrastructure
dotnet ef database update
```

5. Run the API:
```bash
cd ../Medixa-AI.Api
dotnet run
```

API will be available at: `https://localhost:5001` (or configured port)
Swagger UI: `https://localhost:5001/swagger`

### Frontend Setup

1. Install dependencies:
```bash
npm install
```

2. Start development server:
```bash
npm run dev
```

Frontend will be available at: `http://localhost:5173`

## Clean Architecture Rules

- **Domain**: No dependencies on other layers
- **Application**: Depends only on Domain and Infrastructure
- **Infrastructure**: Depends only on Domain
- **API**: Depends only on Application
- **Frontend**: Communicates with API via HTTP (no direct database access)

## Features

- 🩸 Blood test booking system
- 🧪 Lab results management
- 🤖 AI interpretation
- 📊 Trend tracking
- 🧠 Smart medical recommendations
- 🗂 Patient history insights
- 🗣 Simplified reports for non-medical users

## Documentation

See the `docs/` folder for:
- SRS (Software Requirements Specification)
- Class diagrams and specifications
- System overview

See the `design/` folder for:
- Wireframes
- UI/UX designs
