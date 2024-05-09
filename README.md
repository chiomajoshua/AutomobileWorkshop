# Automobile Workshop Backend Documentation

Welcome to the documentation for our Microservice Backend project! This guide provides a comprehensive overview of the project's structure, architecture, and key components. Whether you're a developer contributing or a team member seeking understanding, you're in the right place.
Please note that this is a POC(not working as expected) but this I believe is an efficient way to solve the problem statement

## Table of Contents

1. [Introduction](#introduction)
2. [Project Structure](#project-structure)
3. [Architecture Overview](#architecture-overview)
4. [Database](#database)
5. [Dockerization](#dockerization)
    - [Development Environment](#development-environment)
6. [Unit Testing](#unit-testing)
7. [Getting Started](#getting-started)
8. [Contributing](#contributing)
9. [Todo](#TODO)
10.[Assumptions](#Assumptions) 
11.[License](#license)

## Introduction

Our Microservice Backend project uses .NET 6 and follows a microservices architecture for modularity, scalability, and maintainability. 
It comprises 4 microservices for now: 
Showroom API, Inventory API, Production API, Assembly API using Entity Framework Core, CQRS, Docker, and more.

## Project Structure

The project is structured as follows:

Automobile Workshop Microservice Backend

 ───src
│   │
│   └───BuildingBlocks
│       │
│       └───BuildingBlocks.Domain
│       │   │   Constants
│       │   │   Entities
│       │   │   Enumerators
│       │   │   Models
│       │   │   ...
│       │
│       └───BuildingBlocks.Infrastructure
│       │   │
│       │   └───DatabaseContext
│       │   └───Extensions
│       │   └───RepositoryManager
│       │   │   ...
│       └───BuildingBlocks.Test
│
│   └───Services      
│       │   │
│       │   └───Showroom
│       │   │   │   Showroom.Api
│       │   │   │   Showroom.Service
│       │   │
│       │   └───Order
│       │   │   │   ...
│       │   │
│       │   └───Production
│       │   │   │   ...
│       │   │
        │   └───Assembly
│       │   │   │   ...
│
└───docker-compose
       |───docker-compose.yml
       |───docker-compose.prod.yml


- `docker-compose.yml` and `docker-compose.prod.yml` define Docker configs.
- `src` contains microservice source and the shared library.

## Architecture Overview

Our microservices follow a CQRS architecture, with the nd utilizing Entity Framework Core,RabbitMq, Portainer(for container management), and more.)
This architecture promotes separation of concerns and allows for efficient query and command handling.
Some elements used in the project include:
- Containerisation
- Microservice
- Mediator patterns
- CQRS patterns
- DDD patterns
- Event based patterns
Additional items that demonstrated
- Structured Logging
- Request Correlation
- Error handling
- Configuration

## Database

Both microservices share a common SQL Server database. 
This centralizes data storage, facilitating data consistency and integrity.

## Dockerization

### Development Environment

Use `docker-compose.yml` for development. Run `docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d` to start services.

## Unit Testing

Unit tests are crucial for maintaining code quality. 
We're currently working on the unit tests for the project.

## Getting Started

1. Clone the repository.
2. Set up Docker and Docker Compose.
3. Choose Docker config (`docker-compose.yml` for dev, `docker-compose.prod.yml` for prod).
4. Run Docker commands to start services.
5. Utilize microservices and shared components from `BuildingBlocks`.

## Contributing

1. Fork the repository.
2. Create a feature branch.
3. Implement changes and write tests.
4. Open a pull request with details.

## TODO
1. Expand The Vehicle Entity to include other entities like Company, Model, etc
2. Implement Unit Of Work to improve efficiency and handle failures
3. There are other scenarios when ordering a vehicle, eg. order more than one vehicle. This scenario has not been taking care of.
4. When an order was persisted to the Database, we made an assumption that the order was successfully placed and afterwards published to the Warehouse Orders queue.
5. Write Unit Tests

## Assumptions
1. When an order was persisted to the Database, we made an assumption that the order was successfully placed and afterwards published to the Warehouse Orders queue.
2. We have made an Assumption that the component has been built and the component quantity has been updated on inventory records. 
3. We are assuming that the topics are published and consumed using FIFO.
4. We are assuming the order has a list of all the components(and the quantity) needed in building the vehicle.
5. We are assuming RabbitMq, Seq, Portainer, and SQLServer have been setup.
6. All configurations are expected from the Environment Variable


## License

Licensed under the [MIT License](LICENSE).

Feel free to reach out for questions or clarifications. 

Happy coding!
