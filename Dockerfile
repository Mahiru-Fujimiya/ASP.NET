# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln .
COPY Netflix.API/*.csproj ./Netflix.API/
COPY Netflix.Data/*.csproj ./Netflix.Data/
COPY Netflix.Business/*.csproj ./Netflix.Business/

RUN dotnet restore

COPY . .
RUN dotnet publish Netflix.API -c Release -o /out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

ENTRYPOINT ["dotnet", "Netflix.API.dll"]
