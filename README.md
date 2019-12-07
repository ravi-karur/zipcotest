# Zip - Interview - Customer API

## Technical Stack
ASP.NET Core 3.0 - WebAPI
MongoDB			 - BackEnd - NoSQL DB
MediatR			 - CQRS Pattern
Xunit			 - Unit Tests
AutoMapper		 - Object Mapping
Docker			 - Containerization


## Solution Description

- Solution of this challenge has been built using microservice based architecture and implementation of CQRS and Repository Patterns. 
- CQRS is used to acheive seperation of concern and should definetely be considered in the event based solutions. in this case, most of the business requirements are event based action, hence I feel it CQRS fits better. Also I chose to use MediatR which simplyfies the implementation of CQRS.
- The Repository pattern is used to decouple the business logic and the data access layers in your application. It focuses on where data acccess layer and how data is persisted or retrieved.
- NoSQL:MongoDB which collections Customers and Accounts
- Both Application are dockerized using docker-compose.yml


## Clone the repo

```
https://gitlab.com/ravi.karur/zipcotest.git
 
```

## To Build and deploy the app , open the preferred command prompt and run docker-compose . Below example is for windows command prompt


```

docker-compose up

```

*** Make sure to run below command before rerunning docker-compose up

```

docker-compose down

```


## Check Health ** first run could take few mintutes to pull images and start container **

```
http://localhost:9000/health

```


## Use Swagger to Test the application behaviour. **Hint: Use** *try it out* **option**

```
http://localhost:9000/swagger/index.html

```


