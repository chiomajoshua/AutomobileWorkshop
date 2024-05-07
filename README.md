# Automobile Workshop Backend Documentation

Welcome to the documentation for our Microservice Backend project! This guide provides a comprehensive overview of the project's structure, architecture, and key components. Whether you're a developer contributing or a team member seeking understanding, you're in the right place.

## Table of Contents

1. [Introduction](#introduction)
2. [Project Structure](#project-structure)
3. [Architecture Overview](#architecture-overview)
4. [Shared Library - BuildingBlocks](#shared-library-buildingblocks)
5. [Microservices](#microservices)
    - [Retailer Microservice](#retailer-microservice)
    - [Case Management Microservice](#case-management-microservice)
6. [Database](#database)
7. [Dockerization](#dockerization)
    - [Development Environment](#development-environment)
    - [Production Environment](#production-environment)
8. [Unit Testing](#unit-testing)
9. [Usage of Shared Components](#usage-of-shared-components)
    - [ApiManager](#http-service-abstraction)
    - [CacheManager](#cachemanager-with-redis)
    - [BaseQuery](#stored-procedures-with-dapper)
    - [Validation](#validation-manager)
    - [OMSResponse](#global-response-class-omsresponse)
10. [Getting Started](#getting-started)
11. [Contributing](#contributing)
12. [License](#license)

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
│       │   └───Validation
│       │   │   ...
│       └───BuildingBlocks.Test
│
│   └───Services      
│       │   │
│       │   └───Showroom
│       │   │   │   Showroom.Api
│       │   │   │   Showroom.Service
│       │   │
│       │   └───Inventory
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

## License

Licensed under the [MIT License](LICENSE).

Feel free to reach out for questions or clarifications. 

Happy coding!
