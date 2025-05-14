# Game of Chance API

## Overview

The **Game of Chance API** is a backend service that simulates a betting game where players can create an account, place bets, and either win or lose points based on a randomly generated number. The player begins with 10,000 points and can wager those points on a prediction. If the prediction is correct, the player wins 9 times the wagered points; if incorrect, they lose their wagered points.

### Key Features:
- **Create Player**: Players can create an account(new player) via the API.
- **Place Bet**: Players can place a bet by predicting a number. The system compares the prediction with a random number to determine whether the bet was successful.
- **Error Handling**: The API includes robust error handling, ensuring that invalid requests are properly managed and appropriate error messages are returned.


## Installation

To run the **Game of Chance API** locally, follow these steps:

### Prerequisites
- **.NET 8 or later**: Ensure you have the latest version of .NET installed.
- **Postman** (optional): For testing the API endpoints easily.

### Steps to Run Locally:

1. Clone the repository:

   ```bash
   git clone https://github.com/yourusername/game-of-chance.git
   cd game-of-chance
   ```

Restore the dependencies:
```bash
	dotnet restore
```

Run the application:
```bash
  dotnet run
```

You can test the API endpoints using Postman or any HTTP client, or you can navigate to the Swagger UI.

## Setup Instructions
Once you have the project running locally, you can interact with the following API endpoints:

### 1. Create Player
**Endpoint**: POST /create-player

**Description**: Creates a new player and stores their PlayerId in the session.

**Request**: No body is needed.

**Response**: Returns a new player with a unique PlayerId and an initial account balance of 10,000 points.


### 2. Place Bet
**Endpoint**:
POST /place-bet

**Description**:
Places a bet for the player by predicting a number between 0-9 and specifying the points to wager.

**Request Body**:
```json
  {
    "playerId": 1,
    "points": 10,
    "number": 3
  }
```

**Response**:
Success(200 OK):
```json
  {
    "accountBalance": 10090,
    "status": "Won",
    "points": "+90"
  }
```

400 BadRequest: Returns error if the points are invalid or if the bet is placed incorrectly.

**Error Handling**:

If the player doesn't have enough points to place a bet, an InvalidOperationException is thrown.

Invalid predicted numbers or bet points result in a 400 Bad Request response.


## Error Handling
**Bad Requests**:
If the player provides invalid inputs (e.g., negative points or a number outside the 0-9 range), a 400 Bad Request response is returned.

**Not Enough Points**:
If the player tries to bet more points than they have, an InvalidOperationException is thrown and a 400 error response is returned.

**Unauthorized**:
If a player is not logged in or if their session has expired, a 401 Unauthorized response is returned.


## Future Enhancements
The game is currently single-player, but there are many potential features to extend the functionality in the future:

Multiplayer Mode: Support for multiple players to place bets against each other.

Leaderboard: Track and display top players based on their account balance.


## Technologies Used
**.NET 8**: For building the API backend.

**Swagger**: For API documentation and testing.

**Moq**: For unit testing and mocking dependencies in tests.



## Conclusion:
This README provides a comprehensive overview of the Game of Chance API. It includes:

Setup instructions to get the project running locally.

Detailed information about key API endpoints, such as creating a player and placing a bet.

A section on error handling to explain what happens when things go wrong.

Potential future enhancements to show that the game can be extended.

A brief list of technologies used and contributing guidelines.