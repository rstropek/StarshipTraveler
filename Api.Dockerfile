FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY StarshipTraveler.Api/*.csproj ./StarshipTraveler.Api/
COPY StarshipTraveler.Model/*.csproj ./StarshipTraveler.Model/
RUN dotnet restore StarshipTraveler.Api/StarshipTraveler.Api.csproj

# copy everything else and build app
COPY StarshipTraveler.Api/. StarshipTraveler.Api/.
COPY StarshipTraveler.Model/. StarshipTraveler.Model/.
RUN dotnet publish StarshipTraveler.Api -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "StarshipTraveler.Api.dll"]