# Banking.Trading

This repository contains the **Banking.Trading** project, which is microservice sample solution. The project is designed to handle trading functionalities for a banking system.

## Features

- **Execute Trade**: Buy a trade.
- **Get Trades**: Retrieve all trades made by all customers.
- **Get Single Trade**: Retrieve one trade by the Trade Id.
- **Get Trades By Client Id**: Retrieve All trades made by a single client.

## Project Folder Structure

```
/Banking.Trading
├── client/            # Console Application for monitoring the message queue
├── src/               # Backend application
└── tests/             # Unit and integration tests
```

## Prerequisites

All those requirements for this project are already configured on the `docker-compose.yml`

- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0 or higher)
- SQL Server
- RabbitMQ

## Getting Started

1. Clone the repository:

```bash
git clone https://github.com/your-repo/Banking.Trading.git
cd Banking.Trading
```

2. Run docker compose file:

```bash
docker-compose up -d

```

3. Access the API at `http://localhost:5050`.

## Testing

```bash
Run the test suite using the following command:

dotnet test

```

## Contact

For any questions or issues, please contact me [on](giovanni@pellizzoni.net).
