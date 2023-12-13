# calories-tracker
Tracly application repository. Application for calories and macro nutrients intake logging.
## Hints on usage
To use tracly API for development purposes use docker-compose orchestration. 
docker-compose.dbmigrator.yml is created for setting up and seeding SQL database, docker-compose.api.yml for running api in docker container, docker-compose.db.yml for running just sql server in docker container.

1. Clone the solution.
2. Run your Docker Engine (e.g. through Docker Desktop on Windows).
3. Navigate to docker-compose files location `.\API`.
4. To create and migrate db execute `docker-compose -f .\docker-compose.dbmigrator.yml up --build`. After execution of all scripts stop containers by `CTRL+C`.
5. To run API two options are configured:
    a. Running containerized API application and SQL server:
    -  execute `docker-compose -f .\docker-compose.api.yml up --build`. This will run API and provide API documentation, which can be accessed from http://localhost:5000.

    b. Running local API application and containerized SQL server:
    - execute `docker-compose -f .\docker-compose.db.yml up --build`
    - navigate to folder containing API.csproj and run the following commands to create secrets.json entries: 
        - `dotnet user-secrets set "DB_PW" "<DB_PW value from .env>"`
        - `dotnet user-secrets set "DB_UID" "<DB_UID value from .env>"`
        - `dotnet user-secrets set "TOKEN_KEY" "<some very long token key that will be used.>"`
    - run dotnet API project using 'Web' launchSettings profile.   
