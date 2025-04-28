# Timelines

A comprehensive software solution aimed at providing the office workers with a stable and reliable piece of software fitting their daily workflow regardless of the level of IT competency.

## 1 Getting Started

### 1.1 Clone the Repository

Open the terminal and execute the following command to clone the repository:

```
git clone https://github.com/NikolaVetnic/Timelines Timelines && cd docker-compose
```

### 1.2 Start the Compose Stack

For local development set up the `docker-compose/.env.local` following the `docker-compose/.env.template` file. The `ASPNETCORE_ENVIRONMENT` environment variable should be set to `Development`.

As for production, the setup is the same, save for the variable being set to `Production`.

The compose stack can be spun up using the `deploy-local.sh` and `deploy-prod.sh` scripts.

### 1.3 Verify the Setup

Frontend component as well as the API should be accessible on `localhost` at the designated port. API health can be checked by sending the `GET /health` request.

## 2 Ports

The services' port numbers are as follows:

| Service       | Local Env         | Docker Env        | Within Docker   |
| ------------- | ----------------- | ----------------- | --------------- |
| Core.Api      | `25000` / `25051` | `26000` / `26061` | `8080` / `8081` |
| Core.Frontend | `23000`           | `24000`           | `23000`         |
| Core.Db       | `25432`           | `25432`           | `5432`          |

ASP.NET Core ports are listed as HTTP / HTTPS for running application.

When Nginx is on, the components can be accessed as follows:

| Service       | URL             |
| ------------- | --------------- |
| Core.Api      | `localhost/api` |
| Core.Frontend | `localhost`     |
