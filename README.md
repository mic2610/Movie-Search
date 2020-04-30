Paginated Movie Search with Structured Data
=======================================

Setup
1) Ensure that the .NET Core 3.0 SDK is installed and setup for compilation through Visual Studio 2019
2) Replace the JSON value OmdbApi.Key with another valid API key attained from http://www.omdbapi.com/apikey.aspx, this is located within Movies.Web/appsettings.json 

# Features:
* A movie search results page to search for movies by title using the omdbapi. Example  http://www.omdbapi.com/?apikey=[yourkey]&s=Avengers
* A movie details page displaying more details about a movie using the omdbapi. Example http://www.omdbapi.com/?apikey=[yourkey]&i=tt4154756
* Movie search results page with movie carousel structured data for the current displayed movies in JSON-LD format. More guidelines are here https://developers.google.com/search/docs/data-types/movie#summary
* Movie details page with movie structured data for the displayed movie details in JSON-LD format. More guidelines are here https://developers.google.com/search/docs/data-types/movie#movie
* Pagination on all search results

# Design Choices and Architecture:

Movies solution has been developed in .NET Core 3.1 using the existing boilerplate with many modifications. It has been developed using the Onion architecture consisting of the following in order from the inner most layer to the outer most layer:

* 1) Movies.Core
* 2) Movies.Data
* 3) Movies.Business
* 4) Movies.Web

This allows more moduler and cleaner code to be written and it also avoids any issues in Dependency Injection such as a circular dependency from when two services attempt to access each other. Furthermore this solution also uses SOLID principles:

# SOLID Implementation:
* S - Single-responsiblity principle in the form of Individual services for a responsibility such as MovieService which is then injected into MovieController
* O - Open-closed principle: static extensions such as IEnumerableExtensions
* L - Liskov substitution principle: inheritance of IEnumerable for PagedCollection in Movies.Core
* I - Interface segregation principle: use of Interfaces for services as well as IPagedCollection
* D - Dependency Inversion Principle: Use of injected services into controllers such as MovieController

# Business design choices:

The ondm api is accessed through a an OmdbApiClient.cs class in Movies.Business, then instantiated within MovieService with keys passed in through appsettings.json, these are injected in using the .NET Core IOptions services. JSON returned from the API is then mapped to POCO casses within Movies.Business and then these are mapped into models within Movies.Web. In some cases they are mapped using AutoMapper, only if the mapping profile is basic and does not need many modifications. These models are then passed down into the Razor pages using .NET Core MVC. Structured Data is also JSON serialised using NewtonSoft.net and passed into the head of ever HTML page using the .NET @section tag.

Validation is carried out on the Search Form, which ensures that at least three characters are entered into the Title search field, if no values are returned, safe null or empty checks are performed on the movies search results. Furthermore, ASP.NET Core Tag helpers are used to map values to route queries in the controller, pagination is used, with selected movies even retaining a return url back to search results.

TESTING:
Structured Data has been tested using https://search.google.com/structured-data/testing-tool and has returned no errors

Michael Ayoub
GitHub: mic2610
email: ayoubmichaelwork@gmail.com

# Dependencies:
Register for an API key from http://www.omdbapi.com/apikey.aspx and palce it within appsettings.json