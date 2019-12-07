# Zip - Interview - Customer API

## Tech Stack
- ASP.NET Core 3.0   - WebAPI
- MongoDB			 - BackEnd - NoSQL DB
- MediatR			 - CQRS Pattern
- Xunit			     - Unit Tests
- AutoMapper		 - Object Mapping
- Docker			 - Containerization
- GitLab CI\CD yml   - Git CI\CI pipeline yml with GitLab runner
- DigitalOcean       - cloud hosting


## Solution Description

- CustomerAPI built on microservice based architecture with CQRS and Repository Patterns. 
- CQRS pattern used to acheive seperation of model for reading and writing data.
- Repository pattern is used to decouple the business logic and the data access layers.
- NoSQL:MongoDB with two collections (Customers and Accounts )
- Both Api and Backend are dockerized using docker-compose.yml
- GitLab pipelines(yml) used for CI/CD. ⚠  Basic CI/CD with master branch only. any check-ins to master will deploy the changes automatically.
- GitLab runner and DigitalOcean droplet used for cloud hosting

## Test scenarios's coverd
- Customer Creation
    - Request validation
        - Email format validation
    - Business validation
        - Email already exists - 400 Bad Request

- Account Creation
   - Request validation
        - Email format validation         
   - Business validation
        - Customer already exists , but has no account  - Create new Account
        - Customer already exists , but do not have enough balance linit (1000) for ZipPay credit - 400 Bad Request
        - Customer does not exists , 404 - Customer not found
        

## GitLab Repo

```https://gitlab.com/ravi.karur/zipcotest.git```

## To Build and deploy the app in LOCAL 
Open the preferred command prompt and run docker-compose. ⚠ Below example is for windows command prompt
```
docker-compose up -d
```
⚠ Make sure to run below command before rerunning docker-compose up
```
docker-compose down
```
## Check Health 
⚠ first run could take few mintutes to pull images and start container
```
http://localhost:9000/health

```
## Use Swagger to Test the application behaviour

⚠ Usetry it out option
```http://localhost:9000/swagger/index.html```

## CI/CD - GitLab - Deploying to DigitalOcean droplet
```https://gitlab.com/ravi.karur/zipcotest/pipelines```

## Final result hosted on DigitalOcean cloud ( Enabled with Continous deployment )
```http://157.245.173.234:9000/swagger/index.html```
