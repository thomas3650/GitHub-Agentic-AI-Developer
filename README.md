# GitHub-Agentic-AI-Developer

This repository contains a simple C# weather API, automated tests, and GitHub workflows that are useful for validating agentic development workflows.

## Projects

- `SimpleWeatherApi` - a minimal ASP.NET Core API with a `/weather?city=` endpoint
- `SimpleWeatherApi.Tests` - xUnit tests that validate the weather endpoint

## Run locally

```bash
dotnet restore GitHubAgenticAIDeveloper.slnx
dotnet run --project SimpleWeatherApi/SimpleWeatherApi.csproj --urls http://127.0.0.1:5055
```

Example request:

```bash
curl "http://127.0.0.1:5055/weather?city=London"
```

## Test

```bash
dotnet test GitHubAgenticAIDeveloper.slnx
```

## GitHub workflows

- `pull-request-validation.yml` restores, builds, and tests the solution for every pull request update
- `renovate.yml` runs Renovate on a schedule with a dedicated `RENOVATE_TOKEN` secret and can create a dependency dashboard issue for outdated dependencies

## Cloud agent instructions

- `.github/copilot-instructions.md` contains repository-level coding instructions for the GitHub cloud AI agent.
- `.github/instructions/code-review.instructions.md` contains focused review guidance for Copilot code review.