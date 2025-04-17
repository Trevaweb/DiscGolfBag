# DiscGolfBag API

DiscGolfBag is a C# API built with ASP.NET Core for managing and keeping track of the discs in a disc golf bag. It provides endpoints to perform CRUD operations on discs, such as adding, updating, retrieving, and deleting discs.

## Features

- **In-Memory Database**: Uses an in-memory database for storing disc data.
- **OpenAPI/Swagger Integration**: Includes Swagger UI for exploring and testing the API.
- **Endpoints**:
  - Get all discs
  - Get a disc by ID
  - Get discs by name
  - Search discs by color and name
  - Add a new disc
  - Update an existing disc
  - Delete a disc

## Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

### Running the API

1. Clone the repository.
2. Open the project in your IDE (e.g., Visual Studio or Visual Studio Code).
3. Run the application using the `dotnet run` command or your IDE's run/debug feature.
4. Access the Swagger UI at `http://localhost:5263/swagger` (or the URL specified in `launchSettings.json`).

### Example Disc Model

A disc is represented by the following properties:

```json
{
  "id": 1,
  "name": "Destroyer",
  "color": "Red",
  "flightNumbers": "12/5/-1/3"
}