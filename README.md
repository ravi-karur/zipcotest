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

- Solution of this challenge has been built using microservice based architecture and with support of CQRS and Repository Patterns. 
- CQRS is used to acheive seperation of concern and should definetely be considered in the event based solutions. in this case, most of the business requirements are event based action, hence I feel it CQRS fits better. Also I chose to use MediatR which simplyfies the implementation of CQRS.
- The Repository pattern is used to decouple the business logic and the data access layers in your application. It focuses on where data acccess layer and how data is persisted or retrieved.
- NoSQL:MongoDB which collections Customers and Accounts
- Both Api and Backend are dockerized using docker-compose.yml
- GitLab pipelines yml used for CI/CD. ⚠  Basic CI/CD with master branch only. any check-ins to master will deploy the changes automatically.
- GitLab runner and DigitalOcean droplet used for cloud hosting

## Test scenarios's coverd
- Customer Creation
    - Request validation
        - Email format validation
    - Business validation
        - Email already not exists 

- Account Creation
   - Request validation
        - Email format validation         
   - Business validation
        - Customer already exists , but has no account  - Create new Account
        - Customer already exists , but do not have enough balance linit (1000) for ZipPay credit - 400 Bad Request
        - Customer does not exists , 404 - Customer not found
        

## Clone the repo

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

## Realtime - Cloud Hosting use
