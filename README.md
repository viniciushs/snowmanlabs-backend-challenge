# Snowmanlabs Backend Challenge
### Vinicius Haninec Silva

## Description

This is the solution to the problem proposed by SnowmanLabs: https://github.com/snowmanlabs/backend-challenge

## Table of Contents

- [Tecnical Specs](#tecnical-specs)
- [Deliverables](#deliverables)
- [Instalation](#instalation)
- [Progress](#progress)
- [TODOs](#todos)

## Tecnical Specs

- Architecture based on ASP.NET Core 3.1
- Patherns used:
-- DDD, SOLID and Clean Code
-- Unit of Work
-- Repositories and Generic Repositories
- ASP.NET WebApi Core 3.1
- Entity Framework Core 3.1 using Code First with SQL Server
- ASP.NET Identity using JWT
- AutoMapper
- An OpenAPI 3.0 document using Swagger UI
- Azure Storage Containers to save the pictures 
- CI/CD using Azure Pipelines

## Deliverables

* The source code in a public git repository: https://github.com/viniciushs/snowmanlabs-backend-challenge
* An OpenAPI 3.0 document: https://snowmanlabs-challenge-vinicius-webapi.azurewebsites.net/swagger/index.html
* A Postman collection: https://github.com/viniciushs/snowmanlabs-backend-challenge/tree/master/Postman.Collection

## Instalation

### Requirements

* Visual Studio 2017+
* SQL Server;
* A Microsoft Azure Storage Container;

### Steps

* Open the solution using a Visual Studio.
* Select the SnowmanLabsChallenge.WebApi to be the main project;
* Change the connection string located into app.settings in SnowmanLabsChallenge.WebApi;
-- ConnectionStrings:DefaultConnection
* Change the AzureBlob Configuration located into app.settings in SnowmanLabsChallenge.WebApi:
-- AzureBlobAccountName
-- AzureBlobAccountKey
-- AzureBlobUrl
* Into Package Manager Console, choose the SnowmanLabsChallenge.Inra.Data as Default Project;
* Execute the following command to insert the tables and seed:
-- update-database -Context DefaultContext
-- update-database -Context ApplicationDbContext

## Progress

### User Stories

* ![#f03c15](https://via.placeholder.com/15/f03c15/000000?text=+) As a anonymous user, I want to sign up using my Facebook account.
* ![#f03c15](https://via.placeholder.com/15/f03c15/000000?text=+) As a anonymous user, I want to sign in using my Facebook account.
* ![#c5f015](https://via.placeholder.com/15/c5f015/000000?text=+) As a logged in user, I want to see a list of tourist spots in a 5 km radius from a given location.
* ![#c5f015](https://via.placeholder.com/15/c5f015/000000?text=+) As a logged in user, I want to search for tourist spots by name.
* ![#c5f015](https://via.placeholder.com/15/c5f015/000000?text=+) As a logged in user, I want to register a tourist spot (picture, name, geographical location and category).
* ![#c5f015](https://via.placeholder.com/15/c5f015/000000?text=+) As a logged in user, I want to comment about a tourist spot.
* ![#c5f015](https://via.placeholder.com/15/c5f015/000000?text=+) As a logged in user, I want to see comments about a tourist spot.
* ![#c5f015](https://via.placeholder.com/15/c5f015/000000?text=+) As a logged in user, I want to add pictures to a tourist spot.
* ![#c5f015](https://via.placeholder.com/15/c5f015/000000?text=+) As a logged in user, I want to remove pictures I added to a tourist spot.
* ![#c5f015](https://via.placeholder.com/15/c5f015/000000?text=+) As a logged in user, I want to favorite a tourist spot.
* ![#c5f015](https://via.placeholder.com/15/c5f015/000000?text=+) As a logged in user, I want to see my favorites tourist spots.
* ![#c5f015](https://via.placeholder.com/15/c5f015/000000?text=+) As a logged in user, I want to remove a tourist spot from my favorites.
* ![#c5f015](https://via.placeholder.com/15/c5f015/000000?text=+) As a logged in user, I want to upvote a tourist spot.
* ![#c5f015](https://via.placeholder.com/15/c5f015/000000?text=+) As a logged in user, I want to see the tourist spots I registered.
* ![#c5f015](https://via.placeholder.com/15/c5f015/000000?text=+) As a logged in user, I want to create new categories.

### Business Rules

* ![#c5f015](https://via.placeholder.com/15/c5f015/000000?text=+) Anonymous users can only see things.
* ![#c5f015](https://via.placeholder.com/15/c5f015/000000?text=+) Initial Categories are "Park", "Museum", "Theater", "Monument"

## Todos

 - Write MORE Tests
 - Add a Facebook Auth
