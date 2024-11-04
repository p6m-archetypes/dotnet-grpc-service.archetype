# {{ project-title }}

**// TODO:** Add description of your project's business function.

Generated from the [.NET gRPC Service Archetype](https://github.com/p6m-dev/dotnet-grpc-service-archetype).

[[_TOC_]]

## Prereqs
Running this service requires .NET 8+ and NuGet to be configured with an Artifactory encrypted key. 
For development, be sure to have Docker installed and running locally.

## Overview


## Build System
This project uses [dotnet](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet#general) as its build system. Common goals include

| Goal    | Description                                        |
|---------|----------------------------------------------------|
| clean   | Clean build outputs.                               |
| build   | Builds a .NET application.                         |
| restore | Restores the dependencies for a given application. |
| run     | Runs the application from source                   |
| test    | Runs tests using a test runner                     |

## **Access Token Setup**
1. Login to [JFrog Artifactory](https://p6m.jfrog.io/)
2. Once logged in, click the `Profile Icon` (top-right corner) > click `Edit Profile` > click `Generate an Identity Token`
3. Add a descriptive name (ie: PLATFORM_TOKEN-front-end-apps) > click `Next` > copy the `Username` and `Reference Token` (for the next step)
4. Add the `Username` and `Reference Token` to your local machine environment variables (for Macs, it usually goes to `~/.zshrc` or `~/.zprofile`)
```bash
export ARTIFACTORY_USERNAME="...."        # generated 'Username'
export ARTIFACTORY_IDENTITY_TOKEN="****"  # generated 'Reference Token'
export ARTIFACTORY_TOKEN=$(echo -n "$ARTIFACTORY_USERNAME:$ARTIFACTORY_IDENTITY_TOKEN" | base64)
```
   - If changes were made to `~/.zshrc`: run `source ~/.zshrc` and then `restart your terminal` to apply the new environment variable changes
   - If changes were made to `~/.zprofile`: run `source ~/.zprofile` and then `restart your terminal` to apply the new environment variable changes


## NuGet with JFrog Artifactory Setup
```bash
dotnet nuget add source --name "Artifactory" --username ${ARTIFACTORY_USERNAME} --password ${ARTIFACTORY_IDENTITY_TOKEN} --store-password-in-clear-text "https://p6m.jfrog.io/artifactory/api/nuget/{{ solution-name }}-{{ org_name }}-nuget"
```

## Running the Server
This server accepts connections on the following ports:
- {{ service-port }}: used for application gRPC Service traffic.
- {{ management-port }}: used to monitor the application over HTTP (see [Actuator endpoints](https://docs.spring.io/spring-boot/docs/current/reference/html/actuator.html#actuator.endpoints)).
- {{ database-port }}: exposes the ephemeral database port
- {{ debug-port }}: remote debugging port


Next, start the server locally or using Docker. You can verify things are up and running by looking at the [/health](http://localhost:{{ management-port }}/health) endpoint:
```bash
curl localhost:{{ management-port }}/health
```

You can try to create an entity using a gRPC client, like [grpcurl](https://github.com/fullstorydev/grpcurl) (CLI) or [grpcui](https://github.com/fullstorydev/grpcui) (GUI).
For example,

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
{% endif %}

## Local
Run Database dependencies with `docker-compose`
```bash 
docker-compose up -d
```

From the project root, run the server:
```bash
dotnet run --project {{ ProjectName }}.Server
```


Shutdown local database
```bash 
docker-compose down
```

### Metrics
Prometheus - [Prometheus](https://github.com/prometheus-net/prometheus-net)

You can verify things are up and running by looking at the [/metrics](http://localhost:{{ management-port }}/metrics) endpoint:
```bash
curl localhost:{{ management-port }}/metrics
```

## Modules (UPDATE)

| Directory                                                                 | Description                                                                                |
|---------------------------------------------------------------------------|--------------------------------------------------------------------------------------------|
| [{{ ProjectName }}.API]({{ ProjectName }}.API/README.md)                              | Service Interfaces with a gRPC model. gRPC/Protobuf spec.                                  |
| [{{ ProjectName }}.Client]({{ ProjectName }}.Client/README.md)                        | gRPC Client. Implements the API.                                                           |
| [{{ ProjectName }}.Core]({{ ProjectName }}.Core/README.md)                            | Business Logic. Abstracts Persistence, defines Transaction Boundaries. Implements the API. |
| [{{ ProjectName }}.IntegrationTests]({{ ProjectName }}.IntegrationTests/README.md)    | Leverages the Client to test the Server and it's dependencies.                             |
| [{{ ProjectName }}.Persistence]({{ ProjectName }}.Persistence/README.md)              | Persistence Entities and Data Repositories. Wrapped by Core.                               | 
| [{{ ProjectName }}.Server]({{ ProjectName }}.Server/README.md)                        | Transport/Protocol Host.  Wraps Core.                                                      |

## Contributions
**// TODO:** Add description of how you would like issues to be reported and people to reach out.