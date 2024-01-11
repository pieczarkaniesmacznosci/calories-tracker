# calories-tracker
Tracly application repository. Application for calories and macro nutrients intake logging.
## Hints on usage
To use tracly API for development purposes use docker-compose orchestration. docker-compose.api.yml is created for setting up and running API in docker container, docker-compose.db.yml for running only sql server in docker container.

### Backend set-up
1. Clone the solution.
2. Run your Docker Engine (e.g. through Docker Desktop on Windows).
3. Navigate to docker-compose files location `.\Backend`.
4. To run API two options are configured (for initial database creation and migration follow 4a):
    a. Running containerized API services and SQL server:
    -  execute `docker-compose -f .\docker-compose.api.yml up --build`. This will run API and provide API documentation, which can be accessed from http://localhost:5000/swagger/index.html.

    b. Running local API services and containerized SQL server:
    - execute `docker-compose -f .\docker-compose.db.yml up --build`
    - navigate to each service folder containing .csproj files (`.\Services\IdentityAPI`, `.\Services\CaloriesAPI`, `.\Services\UserAPI`) and run the following commands to create secrets.json entries: 
        - `dotnet user-secrets set "DB_PW" "<DB_PW value from .env>"`
        - `dotnet user-secrets set "DB_UID" "<DB_UID value from .env>"`
        - `dotnet user-secrets set "TOKEN_KEY" "<some very long token key that will be used.>"`
    - run dotnet API projects using Multiple startup projects (APIGateway, IdentityAPI, CaloriesAPI, UserAPI).
