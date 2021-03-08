FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY StarshipTraveler.ServerSide/*.csproj ./StarshipTraveler.ServerSide/
COPY StarshipTraveler.Model/*.csproj ./StarshipTraveler.Model/
COPY StarshipTraveler.Components/*.csproj ./StarshipTraveler.Components/
RUN dotnet restore StarshipTraveler.ServerSide/StarshipTraveler.ServerSide.csproj

# copy everything else and build app
COPY StarshipTraveler.ServerSide/. StarshipTraveler.ServerSide/.
COPY StarshipTraveler.Model/. StarshipTraveler.Model/.
COPY StarshipTraveler.Components/. StarshipTraveler.Components/.
RUN dotnet publish StarshipTraveler.ServerSide -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "StarshipTraveler.ServerSide.dll"]