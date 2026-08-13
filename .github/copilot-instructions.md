# Copilot instructions for GitHub cloud agent

## Repository context
- This repository contains a small ASP.NET Core minimal API in `SimpleWeatherApi` and xUnit tests in `SimpleWeatherApi.Tests`.
- Main endpoints are `/weather` and `/weather/description` with `city` query validation.

## Expected workflow for code changes
1. Keep changes focused and minimal to the user request.
2. Preserve current API behavior unless the request explicitly changes it.
3. Add or update tests in `SimpleWeatherApi.Tests` when behavior changes.
4. Avoid unrelated refactors or formatting-only churn in touched files.

## Validation commands
Run these from the repository root:

```bash
dotnet restore GitHubAgenticAIDeveloper.slnx
dotnet build GitHubAgenticAIDeveloper.slnx --configuration Release --no-restore
dotnet test GitHubAgenticAIDeveloper.slnx --configuration Release --no-build
```

## Guardrails
- If input validation or response shape changes, update tests in the same change.
- Keep error responses explicit and consistent with existing endpoint patterns.
- Prefer existing services and patterns over introducing new architecture for small fixes.
