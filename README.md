# calories-tracker
Repository for calories and macro nutrients intake logging application development.
## Hints on usage
To use tracly API for development purposes use docker-compose orchestration. 
docker-compose.dbmigrator.yml is created for setting up and seeding SQL database, while docker-compose.api.yml for running api in docker container.

1. Clone the solution.
2. Run your Docker Engine (e.g. through Docker Desktop on Windows).
3. To create and migrate db `execute docker-compose -f .\docker-compose.dbmigrator.yml up --build`. After execution of all scripts stop containers by `CTRL+C`.
4a. To run containerizer API backend and sql server execute ` docker-compose -f .\docker-compose.api.yml up --build`. This will run API and provide API documentation, which can be accessed from http://localhost:5000.
4b. To run local API only execute `docker-compose -f .\docker-compose.db.yml up --build`- this will run containerizer sql server only. To run API project change db address to "host.docker.internal,1434" and provide UserId and Password initially set-up in .env file.