FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY StarshipTraveler.Client/*.csproj ./StarshipTraveler.Client/
COPY StarshipTraveler.Model/*.csproj ./StarshipTraveler.Model/
COPY StarshipTraveler.Components/*.csproj ./StarshipTraveler.Components/
RUN dotnet restore StarshipTraveler.Client/StarshipTraveler.Client.csproj 

# copy everything else and build app
COPY StarshipTraveler.Client/. StarshipTraveler.Client/.
COPY StarshipTraveler.Model/. StarshipTraveler.Model/.
COPY StarshipTraveler.Components/. StarshipTraveler.Components/.
RUN dotnet publish StarshipTraveler.Client -c release -o /app --no-restore

# final stage/image
FROM nginx:alpine
COPY StarshipTraveler.Client.Nginx/ /etc/nginx/
COPY --from=build /app/wwwroot /usr/share/nginx/html
