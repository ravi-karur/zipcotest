FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY CustomerService.sln ./
COPY Src/Customer.API/Customer.API.csproj ./Src/Customer.API/
COPY Src/Customer.Service/Customer.Service.csproj ./Src/Customer.Service/
COPY Src/Customer.Data/Customer.Data.csproj ./Src/Customer.Data/
COPY Src/Customer.Domain/Customer.Domain.csproj ./Src/Customer.Domain/

COPY Tests/Customer.Domain.UnitTests/Customer.Domain.UnitTests.csproj ./Tests/Customer.Domain.UnitTests/
COPY Tests/Customer.Service.UnitTests/Customer.Service.UnitTests.csproj ./Tests/Customer.Service.UnitTests/
COPY Tests/Customer.API.Tests/Customer.API.Tests.csproj ./Tests/Customer.API.Tests/
#COPY docker-compose.dcproj ./

RUN dotnet restore 
COPY . .

WORKDIR "/src"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Customer.API.dll"]