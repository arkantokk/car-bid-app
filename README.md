# Car Bidding Platform - Backend

This project is a real-time car bidding platform built using **ASP.NET Core 8.0**. It follows a clean architecture approach, utilizing **CQRS** with **MediatR** and **SignalR** for real-time auction updates.

## 🚀 Tech Stack

* **Framework**: ASP.NET Core 8.0 Web API
* **Database**: Microsoft SQL Server
* **ORM**: Entity Framework Core 8.0
* **Real-time Communication**: ASP.NET Core SignalR
* **Patterns**: CQRS (Command Query Responsibility Segregation) with MediatR
* **Authentication**: ASP.NET Core Identity with JWT Bearer tokens
* **Documentation**: Swagger/OpenAPI
* **Containerization**: Docker Compose for SQL Server

## 🏗️ Architecture

The solution is divided into four main projects:

1. **CarBiddingPlatform.WebAPI**: The entry point, containing controllers, SignalR hubs, and middleware.
2. **CarBiddingPlatform.Application**: Core business logic, including MediatR Commands, Queries, and DTOs.
3. **CarBiddingPlatform.Infrastructure**: Implementation of external concerns like Database Context, Repositories, and Identity services.
4. **CarBiddingPlatform.Domain**: Core entities (Car, Auction, Bid) and business rules.

## 🛠️ Getting Started

### Prerequisites

* .NET 8.0 SDK
* Docker Desktop (for SQL Server)

### 1. Infrastructure Setup

Use the provided `docker-compose.yml` to spin up a local SQL Server instance:

```bash
docker-compose up -d

```

The server will be available on port **1433** with the password specified in the compose file.

### 2. Configuration

Update the `DefaultConnection` in `appsettings.json` or use User Secrets to match your local environment.
The project is configured with JWT settings for local development:

* **Issuer**: CarBiddingAPI
* **Audience**: CarBiddingFrontend

### 3. Database Migrations

Apply the existing Entity Framework migrations to create the database schema:

```bash
dotnet ef database update --project src/CarBiddingPlatform.Infrastructure --startup-project src/CarBiddingPlatform.WebAPI

```

### 4. Running the App

```bash
dotnet run --project src/CarBiddingPlatform.WebAPI

```

The API will be available at `http://localhost:5044` (HTTP) or `https://localhost:7240` (HTTPS). You can access the Swagger UI at `/swagger`.

## 📡 API Endpoints

### Authentication

* `POST /api/Auth/register`: Register a new user.
* `POST /api/Auth/login`: Authenticate and receive a JWT token.

### Auctions

* `GET /api/Auctions/{id}`: Get detailed information about a specific auction (Anonymous).
* `POST /api/Auctions`: Create a new auction (Authorized).

### Bids

* `POST /api/Bids`: Place a bid on an active auction (Authorized).

### Cars

* `POST /api/Cars`: Register a car to be used in an auction (Authorized).

## 🔄 Real-time Updates (SignalR)

The platform uses a SignalR Hub located at `/auctionHub` to provide live updates.

* **Hub Method**: `JoinAuctionGroup(string auctionId)` allows clients to subscribe to specific auction updates.
* **Client Event**: `ReceiveNewBid` broadcasts new bid details (ID, Owner, Amount, Timestamp) to all connected clients in the auction group.

## 🔒 Security and Concurrency

* **Concurrency**: The `Auction` entity uses a `rowversion` concurrency token (`Version`) to prevent race conditions during simultaneous bidding.
* **Error Handling**: A `GlobalErrorHandler` middleware catches common exceptions (e.g., `DbUpdateConcurrencyException`, `KeyNotFoundException`) and returns standardized `ProblemDetails` responses.
