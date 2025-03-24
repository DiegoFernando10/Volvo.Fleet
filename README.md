# Volvo Fleet - Vehicle Management API

This project is a Vehicle Management API for Volvo Fleet, built with .NET Core. It provides endpoints to manage vehicle data, including creation, retrieval, and updates.

## Features
- Retrieve all vehicles
- Get a vehicle by ID
- Create a new vehicle
- Update vehicle details

## Prerequisites
Ensure you have the following installed:
- .NET SDK
- MySQL database (or another supported database)

## Installation
1. Clone this repository:
```sh
git clone https://github.com/DiegoFernando10/Volvo.Fleet.git
```
2. Restore dependencies:
```sh
dotnet restore
```
3. Run the application:
```sh
dotnet run
```

## API Endpoints

### Retrieve All Vehicles
**GET** `/api/vehicle`

### Get Vehicle by ID
**GET** `/api/vehicle/{id}`

### Create a New Vehicle
**POST** `/api/vehicle`
#### Request Body:
```json
{
  "chassisSeries": "ABC123",
  "chassisNumber": 123456,
  "type": 2,
  "color": "Blue"
}
```

### Update Vehicle
**PUT** `/api/vehicle/{id}`
#### Request Body:
```json
{
  "color": "Red"
}
```

## Running Tests
### Using Visual Studio
1. Open the project in Visual Studio.
2. Open **Test Explorer** (`Ctrl + E, T` or via **Test** → **Test Explorer**).
3. Click **Run All** to execute all tests.

### Using Command Line
```sh
dotnet test
```