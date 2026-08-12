# GitHub-Agentic-AI-Developer

This repository contains a simple C# weather API, automated tests, and GitHub workflows that are useful for validating agentic development workflows.

## Projects

- `SimpleWeatherApi` - a minimal ASP.NET Core API with a `/weather?city=` endpoint
- `SimpleWeatherApi.Tests` - xUnit tests that validate the weather endpoint

## Run locally

```bash
dotnet restore GitHubAgenticAIDeveloper.slnx
dotnet run --project /home/runner/work/GitHub-Agentic-AI-Developer/GitHub-Agentic-AI-Developer/SimpleWeatherApi/SimpleWeatherApi.csproj
```

Example request:

```bash
curl "http://localhost:5000/weather?city=London"
```

## Test

```bash
dotnet test GitHubAgenticAIDeveloper.slnx
```

## GitHub workflows

- `pull-request-validation.yml` restores, builds, and tests the solution for every pull request update
- `pull-request-approval.yml` requires at least one approving review before the approval check passes
- `renovate.yml` runs Renovate on a schedule and can create a dependency dashboard issue for outdated dependencies