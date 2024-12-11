# Timelines Backend Solution

Timelines backend solution.

## 1 Ports

The services' port numbers are as follows:

| Service  | Local Env         | Docker Env        | Within Docker   |
| -------- | ----------------- | ----------------- | --------------- |
| Core.Api | `25001` / `25051` | `26001` / `26061` | `8080` / `8081` |
| Postgres | `25432`           | `25432`           | `5432`          |

ASP.NET Core ports are listed as HTTP / HTTPS for running application.

## 2 Various

### 2.1 Docker Compose Aliases

From the solution root run `source ./Various/aliases` to source the following aliases:

| Command | Description                                                                                                         |
| ------- | ------------------------------------------------------------------------------------------------------------------- |
| `tlup`  | `docker compose -f ./DockerCompose/docker-compose.yml -f ./DockerCompose/docker-compose.override.yml up -d`         |
| `tlupb` | `docker compose -f ./DockerCompose/docker-compose.yml -f ./DockerCompose/docker-compose.override.yml up --build -d` |
| `tldn`  | `docker compose -f ./DockerCompose/docker-compose.yml -f ./DockerCompose/docker-compose.override.yml down`          |

All commands work from anywhere in the file system, it is not necessary to stay at the root.
