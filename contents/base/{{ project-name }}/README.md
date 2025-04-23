# {{ project-title }}

**// TODO:** Add description of your project's business function.

Generated from the [.NET gRPC Service Archetype](https://github.com/p6m-archetypes/dotnet-grpc-service.archetype).

## Table of Contents
- [Prereqs](#prereqs)
  - [1. .NET SDK](#1-net-sdk)
  - [2. NuGet Package Management](#2-nuget-package-management)
  - [3. Docker Installed and Running](#3-docker-installed-and-running)
- [Overview](#overview)
  - [Project Structure / Modules](#project-structure--modules)
  - [Build System](#build-system)
- [Running the Server](#running-the-server)
  - [Running the Server Locally](#running-the-server-locally)
  - [Using your service's APIs](#using-your-services-apis)
{% if persistence != 'None' %}
  - [DB migrations](#db-migrations)
{% endif %}
- [Package management](#package-management)
  - [For Local, Offline Development](#for-local-offline-development)
- [Metrics](#metrics)
- [Contributions](#contributions)

## Prereqs
### 1. .NET SDK  
- **Version:** 9.0 or higher  
- **Verify:**  
    ```bash
    dotnet --version # → 9.x.x or greater 
    ```
- See https://developer.p6m.dev/docs/workstation/dotnet for instructions
### 2. NuGet Package Management
- **Verify** you've configured **Artifactory**
    ```bash
    echo $ARTIFACTORY_USERNAME 
    echo $ARTIFACTORY_IDENTITY_TOKEN 
    ```
- See https://developer.p6m.dev/docs/workstation/core/artifacts for instructions
### 3. Docker Installed and Running
- **Verify** you have installed docker
    ```bash
    docker --version # Should be version X.X.+
    docker info # Should list server info without any errors
    ```
- See https://developer.p6m.dev/docs/workstation/core/docker for instructions

# Overview
## Project Structure / Modules

| Directory                                                                 | Description                                                                                |
|---------------------------------------------------------------------------|--------------------------------------------------------------------------------------------|
| [{{ ProjectName }}.API]({{ ProjectName }}.API/README.md)                              | Service Interfaces with a gRPC model. gRPC/Protobuf spec.                                  |
| [{{ ProjectName }}.Client]({{ ProjectName }}.Client/README.md)                        | gRPC Client. Implements the API.                                                           |
| [{{ ProjectName }}.Core]({{ ProjectName }}.Core/README.md)                            | Business Logic. Abstracts Persistence, defines Transaction Boundaries. Implements the API. |
| [{{ ProjectName }}.IntegrationTests]({{ ProjectName }}.IntegrationTests/README.md)    | Leverages the Client to test the Server and it's dependencies.                             |
| [{{ ProjectName }}.Persistence]({{ ProjectName }}.Persistence/README.md)              | Persistence Entities and Data Repositories. Wrapped by Core.                               | 
| [{{ ProjectName }}.Server]({{ ProjectName }}.Server/README.md)                        | Transport/Protocol Host.  Wraps Core.                                                      |



## Build System
This project uses [dotnet](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet#general) as its build system. Common goals include

| Goal    | Description                                        |
|---------|----------------------------------------------------|
| clean   | Clean build outputs.                               |
| build   | Builds a .NET application.                         |
| restore | Restores the dependencies for a given application. |
| run     | Runs the application from source                   |
| test    | Runs tests using a test runner                     |

## Running the Server
This server accepts connections on the following ports:
- {{ service-port }}: used for application gRPC Service traffic.
- {{ management-port }}: used to monitor the application over HTTP (see [Actuator endpoints](https://docs.spring.io/spring-boot/docs/current/reference/html/actuator.html#actuator.endpoints)).
- {{ database-port }}: exposes the ephemeral database port
- {{ debug-port }}: remote debugging port

### Running the Server Locally
Start the server locally or using Docker. <br> From the project root, run the server:
```bash
dotnet run --project {{ ProjectName }}.Server
```

Verify things are up and running by looking at the [/health](http://localhost:{{ management-port }}/health) endpoint:
```bash
curl localhost:{{ management-port }}/health
```


#### Running DB locally with persistent state
Run Database dependencies with `docker-compose`
```bash 
docker-compose up -d
```

Shutdown local database
```bash 
docker-compose down
```

### Using your service's APIs

Create, Read, Update and Delete an entity using a gRPC client, like [grpcurl](https://github.com/fullstorydev/grpcurl) (CLI) or [grpcui](https://github.com/fullstorydev/grpcui) (GUI).

Create{{ ProjectPrefix }}
```bash
grpcurl -plaintext -d '{"name": "test"}' localhost:{{ service-port }} \
    {{ root_package }}.grpc.{{ ProjectPrefix }}{{ ProjectSuffix }}/Create{{ ProjectPrefix }}
```
Get{{ ProjectPrefix }}s
```bash
grpcurl -plaintext -d '{"start_page": "1", "page_size": "5"}' localhost:{{ service-port }} \
    {{ root_package }}.grpc.{{ ProjectPrefix }}{{ ProjectSuffix }}/Get{{ ProjectPrefix }}s
```
{% if persistence != 'None' %}
## DB migrations
### Create DB Migration
```bash
dotnet ef migrations add InitialCreation  --project {{ ProjectName }}.Persistence -s {{ ProjectName }}.Server
```

### Apply DB migrations
```bash
dotnet ef database update --project {{ ProjectName }}.Persistence -s {{ ProjectName }}.Server
```

### Remove DB migrations
```bash
dotnet ef migrations remove --project {{ ProjectName }}.Persistence -s {{ ProjectName }}.Server
```
{% endif %}

## Package management
### List NuGet repositories
```bash
dotnet nuget list source
```

### For Local, Offline Development
#### Pack NuGet packages locally
```sh
dotnet pack -c Release -o ~/.nuget_local
```

#### Add local NuGet repository
```bash
dotnet nuget add source ~/.nuget_local -n Local
```

#### Pull from local repository
```sh
dotnet add package {{ ProjectName }}.Client -v 1.0.0 -s ~/.nuget_local
```

## Metrics
Prometheus - [Prometheus](https://github.com/prometheus-net/prometheus-net)

You can verify things are up and running by looking at the [/metrics](http://localhost:{{ management-port }}/metrics) endpoint:
```bash
curl localhost:{{ management-port }}/metrics
```

## Contributions
**// TODO:** Add description of how you would like issues to be reported and people to reach out.