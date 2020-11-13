FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
ARG DATABASE_URL
ENV ENV_DATABASE_URL = $DATABASE_URL
#RUN echo "Variable : $ENV_DATABASE_URL" 
WORKDIR /app

COPY ["Api/Catalog.Api.xml", "./"]

# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src/Catalog
COPY ["Api/Catalog.Api.csproj", "Api/"]
COPY ["Infrastructure/Catalog.Infrastructure.csproj", "Infrastructure/"]
COPY ["Domain/Catalog.Domain.csproj", "Domain/"]
RUN dotnet restore "Api/Catalog.Api.csproj"
COPY . .
# Update WORKDIR to below
WORKDIR "/src/Catalog/."

RUN dotnet build "Api/Catalog.Api.csproj" -c Release -o ./build

# publish
FROM build AS publish
RUN dotnet publish "Api/Catalog.Api.csproj" -c Release -o ./publish

FROM base AS runtime
WORKDIR /app

COPY --from=publish src/Catalog/publish .

# heroku uses the following 
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Catalog.Api.dll