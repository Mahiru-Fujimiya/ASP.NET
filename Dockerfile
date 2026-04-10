# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY *.sln .
COPY Netflix.API/*.csproj ./Netflix.API/
COPY Netflix.Data/*.csproj ./Netflix.Data/
COPY Netflix.Business/*.csproj ./Netflix.Business/

RUN dotnet restore

COPY . .
RUN dotnet publish Netflix.API -c Release -o /out

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /out .

ENTRYPOINT ["dotnet", "Netflix.API.dll"]