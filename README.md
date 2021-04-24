# calories-tracker
Repository for calories and macro nutrients intake logging
## Hints on usage
To use tracly API for development purposes use docker-compose orchestration. 
docker-compose.data.yml is created for setting up database, while docker-compose.api.yml for running api in docker container.

1. Copy docker-compose.data.yml, docker-compose.api.yml, .env files on your local machine,
2. Run your Docker Engine (e.g. through Docker Desktop on Windows),
3. Execute `docker-compose -f docker-compose.data.yml up --build` and after execution of all scripts stop containers by `CTRL+C`. This will create and migrate sql database, 
4. Execute `docker-compose -f docker-compose.api.yml up --build --remove-orphans`. This will run API, which can be accessed from http://localhost:5000 and provides API documentation
