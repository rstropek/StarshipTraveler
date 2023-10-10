[![api](https://github.com/rstropek/StarshipTraveler/actions/workflows/api.yaml/badge.svg)](https://github.com/rstropek/StarshipTraveler/actions/workflows/api.yaml)
[![client](https://github.com/rstropek/StarshipTraveler/actions/workflows/client.yaml/badge.svg)](https://github.com/rstropek/StarshipTraveler/actions/workflows/client.yaml)
[![server](https://github.com/rstropek/StarshipTraveler/actions/workflows/server.yaml/badge.svg)](https://github.com/rstropek/StarshipTraveler/actions/workflows/server.yaml)

# Starship Traveler Blazor Demo

![Screenshot](starship-travel.gif)

I created this demo app for the [DevOne 2019 conference](https://devone.at). It has been updated for my Blazor sessions at [Techorama Belgium 2019](https://techorama.be/), [.NET Day Switzerland](https://dotnetday.ch/), and the [*International JavaScript Conferences* in London and Munich](https://javascript-conference.com/), Webinar for [M&F Engineering, Switzerland](https://www.m-f.ch/events/event/29-webinare/73-webframeworks-webinar_2021), Wasm workshop for [University of Eduction Upper Austria](https://ph-ooe.at/). It showcases the capabilities of the ASP.NET Core Blazor framework.

**Note** that this sample will not be continuously maintained. I will update it every now and then whenever I use it in workshops and/or conference talks. If you update and/or extend it, feel free to send me a pull request.

## Demo Content

* Fundamentals of programming with Blazor in a non-trivial app
* Same app in Blazor Webassembly **and** Server-side Blazor
* Encapsulation of components in a class library that can be used on client and server
* Code sharing between client and server
* Use of "legacy" .NET components (.NET Framework library) in Blazor WASM **and** Server-side Blazor
* Debugging
* Release deployments using container technology
* CI/CD with GitHub Actions (deployment to *Azure App Service*)

## Run the Full Demo

* Install *Blazor* by installing *Visual Studio Preview*.

* Clone the repository.

* Open the solution [StarshipTraveler.sln](StarshipTraveler.sln) and run it.

## Replay Demo?

Do you want to try the demo yourself? Here is what you do:

* Install *Blazor* by installing *Visual Studio Preview*.

* Clone the repository.

* Start the [API project](StarshipTraveler.Api) because the Blazor frontend consumes this Web API. **Note** that you should run the Web API as a console app or [in a Docker container](https://github.com/rstropek/StarshipTraveler/blob/master/storyboard.md#docker). Don't start the API in IIS as this can lead to CORS issues.

* Open the solution in the [*Start*](Start) folder.

* Follow the steps mentioned in [*storyboard.md*](storyboard.md). You will find the files referenced there in the [*Assets*](Assets) folder.

## Docker

This repository contains Dockerfiles for all three projects:

* *Api.Dockerfile*
* *Client.Dockerfile* (WASM Client)
* *Server.Dockerfile* (Serverside Blazor)

GitHub Actions build the Dockerfiles and publish the resulting images to Docker hub:

* [*rstropek/starshipapi*](https://hub.docker.com/repository/docker/rstropek/starshipapi)
* [*rstropek/starshipclient*](https://hub.docker.com/repository/docker/rstropek/starshipclient)
* [*rstropek/starshipserver*](https://hub.docker.com/repository/docker/rstropek/starshipserver)

Note that the Dockerfiles build release versions of the apps. Therefore, you can use the resulting images to demonstrate WASM differences between debug and release builds.

## Azure

Currently, the Docker images are deployed in Azure. **Note** that I do not always keep those deployments active. I sometimes turn them off to save costs when not doing presentations.

* API: [https://starshipapi.azurewebsites.net](https://starshipapi.azurewebsites.net)
  * Try it by opening [https://starshipapi.azurewebsites.net/api/tickets](https://starshipapi.azurewebsites.net/api/tickets)
* Client: [https://starshipclient.azurewebsites.net](https://starshipclient.azurewebsites.net)
* Server: [https://starshipserver.azurewebsites.net](https://starshipserver.azurewebsites.net)

## Recording

![Rainer on Stage](rainer-on-stage.jpg)

* Recording of the session at DevOne Linz in Spring 2019: https://www.youtube.com/watch?v=_gYgkZ1UBQ4
* Recording of the session at iJS London in 2020: https://youtu.be/mFBqThJQa0o
