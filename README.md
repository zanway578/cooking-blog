# A Simple Cooking Blog in .NET
![Build status](https://img.shields.io/github/actions/workflow/status/zanway578/cooking-blog/dotnet.yml?branch=main)
## Purpose
Every time I go on a cooking blog site, I am bombarded with endless ads, messy UIs, and overly intense colors. This project is designed to be easy to deploy using Azure, quick to load, and easy to read. Furthermore, it's designed to allow for easy creation of recipes, including using the USDA FDC API
to auto-generate nutrition info.

## Progress
This project is still some time away from being a full production project, but it does support does currently support the creation of recipes, recipe tags, and ingredients. I am currently building out the non-admin part of the site, including the home, search, and suggested recipe pages/components.
Recently, integration of the USDA FDC API has been completed.

## Tooling
This project is using SQL server and .NET 8 Razor Pages + .NET 8 Web API. Authentication is done via the Identity Framework and database interaction is handles via the Entity Framework.

## Future Features
- Storing recipe info in a JSON blob in a database column so that reading recipe data is lower cost and lets web page render faster.
- Generating a shopping list of ingredients based on selected recipes.
