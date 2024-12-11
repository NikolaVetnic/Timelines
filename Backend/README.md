# Timelines Backend Solution

Timelines backend solution.

## 1 Ports

The services' port numbers are as follows:

| Service  | Local Env         | Docker Env        | Within Docker   |
| -------- | ----------------- | ----------------- | --------------- |
| Core.Api | `25001` / `25051` | `26001` / `26061` | `8080` / `8081` |
| Postgres | `25432`           | `25432`           | `5432`          |

ASP.NET Core ports are listed as HTTP / HTTPS for running application.

## 2 Solution Structure

Solution structure diagrams can be viewed [here](https://drive.google.com/drive/folders/1HYEQCzZ2Otbqf1CSzVkrOl4q5qZRX4Vi?usp=sharing).

## 3 Various

### 3.1 Docker Compose Aliases

From the solution root run `source ./Various/aliases` to source the following aliases:

| Alias   | Command                                                                                                             |
| ------- | ------------------------------------------------------------------------------------------------------------------- |
| `tlup`  | `docker compose -f ./DockerCompose/docker-compose.yml -f ./DockerCompose/docker-compose.override.yml up -d`         |
| `tlupb` | `docker compose -f ./DockerCompose/docker-compose.yml -f ./DockerCompose/docker-compose.override.yml up --build -d` |
| `tldn`  | `docker compose -f ./DockerCompose/docker-compose.yml -f ./DockerCompose/docker-compose.override.yml down`          |

All commands work from anywhere in the file system, it is not necessary to stay at the root.

## 4. Getting Started

Follow the steps below to set up and run the project:
### Step 1: Clone the Repository

Open your terminal and execute the following command to clone the repository:

```
git clone https://github.com/NikolaVetnic/Timelines.git
```

### Step 2: Load Predefined Aliases (Optional)

Navigate to the `./Various` directory and load the predefined aliases into your terminal by running:

```
source aliases
```

This step configures your terminal with convenient shortcuts. For instance, the alias `tlupb` (short for _Timelines Up Build_) automates pulling the necessary Docker images and setting up the containers.

### Step 3: Start the Containers

To start the containers, use the alias:

```
tlupb
```

Alternatively, if you prefer not to use aliases, navigate to the `/Backend/src` directory and run the equivalent command manually:

```
docker compose -f ./DockerCompose/docker-compose.yml -f ./DockerCompose/docker-compose.override.yml up --build -d
```

### Step 4: Verify the Setup

To confirm the containers are running correctly, open your browser and visit the following URLs:

- [http://localhost:26001](http://localhost:26001)
- [http://localhost:26001/health](http://localhost:26001/health)

These endpoints will indicate whether the setup was successful.