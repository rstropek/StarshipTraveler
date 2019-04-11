# Starship Traveler Storyboard

**Theme:** Book travel between stars

## Start

* Open *Start* solution

* Run it, talk about use of WebAssembly

* Talk about solution structure (everything **preview**)
  * Backend ASP.NET Core 3
  * Blazor Client
  * Components (reusable UI component library)
  * Model (data model, business logic)

## Ticket List

* Add reference from *Client* to *Model*

* Uncomment `@using StarshipTraveler.Model` in *_ViewImports.cshtml*

* Copy *010-Basic TicketList* into *Client/Pages*

* Talking points:
  * Razor syntax (mention code-behind with inheritance)
  * Reuse of existing business logic (server and client)
  * `HttpClient` injection, interaction with browser (custom `HttpMessageHandler`)
  * Point out `async/await` programming model
  * Point out Linq
  * JSON support

## Ticket Card

* Add reference from *Client* to *Components*

* Uncomment `@using StarshipTraveler.Components` in *_ViewImports.cshtml*

* Copy...
  * ...*020-UI Composition/TicketCard.cshtml* and *TicketQRCode.cshtml* into *Components*
  * ...*020-UI Composition/TicketList.cshtml* into *Client*

* Talking points:
  * Reusable component libraries (run on server and client)
  * Growing 3rd party ecosystem or components
  * UI composition
  * Parameters from parent to child components

## More Complex Parent/Child Relationships

* Copy...
  * ...*030-Parent-Child/TimeSelector.cshtml* into *Components*
  * ...*030-Parent-Child/TicketList.cshtml* into *Client*

* Talking points:
  * Enabling two-way binding with parameters (in *TimeSelector* and *TicketList*)
  * *Class bindings*
  * Manual refreshing of bindings with `StateHasChanged`

## More About Router

* Copy...
  * ...*040-Router/BaseImage.cshtml* into *Components*
  * ...*040-Router/TicketDetails.cshtml* into *Client*

* Talking points:
  * Route parameter
  * Parallel web requests with `Task`-based programming model
  * Links using `a href` (*TicketQRCode.cshtml*)

## Razor Compiler

* Show *TicketList.cshtml.g.cs* in *Client/Pages*

* Talking points:
  * *cshtml* becomes C# classes
  * Attributes for Routing, Layouting, DI, etc.

* Copy...
  * ...*050-Razor Compiler/FlightNetwork.cs* into *Components*
  * ...*050-Razor Compiler/Network.cshtml* into *Client*
  * ...*050-Razor Compiler/Flightplan.cs* into *Model*

* Add `services.AddSingleton<IFlightplan, Flightplan>();` to *Client/Startup.cs*

* Talking points:
  * C#-only components (*FlightNetwork.cs* and *Network.cshtml*)
  * Render Trees
  * DI for application services (`IFlightplan`)

## Forms and Validation

* Copy *060-Forms-Validation/BookTicket.cshtml* into *Client*

* Talking points:
  * Blazor forms and validation engine
  * Data annotation
  * Central validation logic
  * Used in client *and* server (see *requests.http*)

## JS Interop

* Copy...
  * ...*070-JS Interop/BookTicket.cshtml* into *Client*
  * ...*070-JS Interop/index.html* into *Client*
  * ...*070-JS Interop/Flightplan.cs* into *Model*

* Add NuGet reference to *Dijkstra.NET* to *Model*

* Let *Luke Skywalker* travel from *Tatooine* to *Earth*

* Talking points:
  * Benefit from large C# ecosystem of business logic libraries
  * JS interop to interact with (existing) JavaScript
