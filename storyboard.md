# Starship Traveler Storyboard

**Theme:** Book travel between stars

**Note: While the demo app has been updated to .NET 5 and latest version of Blazor, this storyboard has not been brought up to date!**

## Start

* Open *Start* solution

* Run it, talk about use of WebAssembly
  * Show browser cache with .NET assemblies
  * Demo deleting cache and reloading

* Talk about solution structure
  * Backend ASP.NET Core 5
  * Blazor Client
  * Components (reusable UI component library)
  * Model (data model, business logic)
  * Server-side Blazor

## Ticket List

* Point out reference from *Components* to *Model*

* Point out `@using StarshipTraveler.Model` in *_Imports.razor*

* Copy *010-Basic TicketList* into *Components/Pages*

* Talking points:
  * Razor syntax (mention code-behind with inheritance)
  * Reuse of existing business logic (server and client)
  * `HttpClient` injection, interaction with browser (custom `HttpMessageHandler`)
  * Point out `async/await` programming model
  * Point out Linq
  * JSON support

## Ticket Card

* Copy...
  * ...*020-UI Composition/TicketCard.razor* and *TicketQRCode.razor* and *TicketList.razor* into *Components*

* Talking points:
  * Reusable component libraries
  * Growing 3rd party ecosystem of components
  * UI composition
  * Parameters from parent to child components

## More Complex Parent/Child Relationships

* Copy...
  * ...*030-Parent-Child/TimeSelector.razor* and *TicketList.razor* into *Components*

* Talking points:
  * Enabling two-way binding with parameters (in *TimeSelector* and *TicketList*)
  * *Class bindings*
  * Manual refreshing of bindings with `StateHasChanged`

## More About Router

* Copy...
  * ...*040-Router/BaseImage.razor* and *TicketDetails.razor* into *Components*

* Talking points:
  * Route parameter
  * Parallel web requests with `Task`-based programming model
  * Links using `a href` (*TicketQRCode.razor*)

## Razor Compiler

* Show *TicketList.razor.g.cs* in *StarshipTraveler.Components\obj\Debug\netstandard2.0\Razor\Pages*

* Talking points:
  * *cshtml* becomes C# classes
  * Attributes for Routing, Layouting, DI, etc.

* Copy...
  * ...*050-Razor Compiler/FlightNetwork.cs* and *Network.razor* into *Components*
  * ...*050-Razor Compiler/Flightplan.cs* into *Model*

* Add `builder.Services.AddScoped<IFlightplan, Flightplan>();` to *Client/Startup.cs*
* Add `services.AddSingleton<IFlightplan, Flightplan>();` to *ServerSide/Startup.cs*

* Talking points:
  * C#-only components (*FlightNetwork.cs* and *Network.razor*)
  * Render Trees
  * DI for application services (`IFlightplan`)

## Forms and Validation

* Copy *060-Forms-Validation/BookTicket.razor* into *Components*

* Talking points:
  * Blazor forms and validation engine
  * Data annotation
  * Central validation logic
  * Used in client *and* server (see *requests.http*)

## JS Interop

* Point out JavaScript in *StarshipTraveler.Client\wwwroot\index.html*

* Point out NuGet reference to *Dijkstra.NET* in *Model*

* Copy...
  * ...*070-JS Interop/BookTicket.razor* into *Components*
  * ...*070-JS Interop/Flightplan.cs* into *Model*

* Let *Luke Skywalker* travel from *Tatooine* to *Earth*

* Talking points:
  * Benefit from large C# ecosystem of business logic libraries
  * JS interop to interact with (existing) JavaScript

## API Service

* Copy...
  * ...*080-ApiService/IStarshipApi.cs* and *080-ApiService/StarshipApi.cs* to *Model*
  * ...*080-ApiService/Data/StarshipApi.cs* to *Client/Data*
  * ...all files from *080-ApiService/Pages/* to *Components/Pages*

* Add `builder.Services.AddScoped<IStarshipApi, StarshipApi>();` to *Client/Startup.cs*
* Add `services.AddSingleton<IStarshipApi, StarshipApi>();` to *ServerSide/Startup.cs*

* Talking points:
  * Reuse of `HttpClient` in WASM
  * Reuse of existing API clients that build on `HttpClient`

## Server-Side Blazor

* Copy *StarshipTraveler.ServerSide* project into solution folder

* Add *StarshipTraveler.ServerSide* to solution

* Run server-side solution and show Websockets traffic

* Talking points:
  * Reuse of Components
  * Get production support today, switch to WASM later

## Docker

docker build -f Api.Dockerfile -t starshipapi .
docker run -t -p 5000:80 --rm --name starshipapi starshipapi
docker tag starshipapi rstropek/starshipapi
docker push rstropek/starshipapi

docker build -f Client.Dockerfile -t starshipclient .
docker run -t -p 5002:80 --rm --name starshipclient starshipclient
docker tag starshipclient rstropek/starshipclient
docker push rstropek/starshipclient
