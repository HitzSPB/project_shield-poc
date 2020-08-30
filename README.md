# project_shield-poc
This repository includes multiple of solutions that together creates a poc how to create a shield around a old monolith application that supports API calls. It uses Azure resources so partners and other application can talk with it through Azure API Management and Azure Relay.

# Missing
## Things I will fix myself unless a PR arrive
I will fix following things once I run out of projects to do
- Add the integration tests missing to the infrastructure layer with CosmosDB ( note I already added so the tests can spin up a docker container that contains a CosmosDB Emulator)
-  Split the build pipeline into multiple builds with specific responsibility
- 
## Things I am not going to fix or add
The following things should be added if you are going to use the project for a production-like environment
- End to end test with a tool like Postman
- Arm Template/Terraform templates to automatic resource creation in Azure or on a Azure Stack
- Deployment of code to Azure
- Relay listener on your API server or what you wish to use it for
- Some sort of backend to validate if is correct customer that creating relays
# Diagram over the system
Blueboxes er created in this POC and red boxes are a recommended final structure that are not created

# How to start developing
This is how to start developing
## Requirements 
- Visual Studio or Visual Studio Code
- DotNET Core SDK installed
- To debug you need the Azure package in Visual Studio installed

## Optional
- To run all Integration test you need docker installed
- Azure subscription if you wish to run it in a production like environment
- Postman or a similar tool to test

## How to debug
- Add environment variables to run the entire system. You can find them in Utilities/ApplicationSettings.cs
## Github actions/Automated build
- Either add your own SonarCloud/SonarQube server information or remove it from the code
