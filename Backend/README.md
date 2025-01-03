# Timelines Backend Solution

Description pending.

## 1 Getting Started

Follow the steps below to set up and run the project:

### 1.1 Clone the Repository

Open your terminal and execute the following command to clone the repository:

```
git clone https://github.com/NikolaVetnic/Timelines Timelines && cd Timelines/Backend/src
```

### 1.2 Start the Containers

After navigating to the `/Backend/src` directory in the previous step run the following command to start the containers:

```
docker compose -f ./DockerCompose/docker-compose.yml -f ./DockerCompose/docker-compose.override.yml up --build -d
```

For Linux and macOS users, predefined aliases are available. To load the aliases, execute the following command:

```
source ./Various/aliases
```

Consult the [table given below](#4-Various) for the list of available aliases.

### 1.3 Verify the Setup

Once the containers are running the following `Core.Api` endpoints will be available on `localhost:26001`

-   `GET /health`
-   `GET /Nodes/Test`

The entire Postman collection can be found [here](https://www.postman.com/nikolavetnic/workspace/timelines).

## 2 Ports

The services' port numbers are as follows:

| Service  | Local Env         | Docker Env        | Within Docker   |
| -------- | ----------------- | ----------------- | --------------- |
| Core.Api | `25001` / `25051` | `26001` / `26061` | `8080` / `8081` |
| Postgres | `25432`           | `25432`           | `5432`          |

ASP.NET Core ports are listed as HTTP / HTTPS for running application.

## 3 Solution Structure

Solution structure diagrams can be viewed [here](https://drive.google.com/drive/folders/1HYEQCzZ2Otbqf1CSzVkrOl4q5qZRX4Vi?usp=sharing).

## 4 Various

### 4.1 Docker Compose Aliases

From the solution root run `source ./Various/aliases` to source the following aliases:

| Alias   | Command                                                                                                             |
| ------- | ------------------------------------------------------------------------------------------------------------------- |
| `tlup`  | `docker compose -f ./DockerCompose/docker-compose.yml -f ./DockerCompose/docker-compose.override.yml up -d`         |
| `tlupb` | `docker compose -f ./DockerCompose/docker-compose.yml -f ./DockerCompose/docker-compose.override.yml up --build -d` |
| `tldn`  | `docker compose -f ./DockerCompose/docker-compose.yml -f ./DockerCompose/docker-compose.override.yml down`          |

All commands work from anywhere in the file system, it is not necessary to stay at the root.
