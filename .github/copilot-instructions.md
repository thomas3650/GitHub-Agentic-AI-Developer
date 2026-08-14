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

## Auto-merge policy for docs-only PRs
A PR is eligible for automated merge only when **both** conditions hold:

1. **Path scope:** every changed file path starts with `docs/`.
2. **Explicit label:** the PR carries the `docs-only` label.

When those hold, the `Docs Auto-Merge` workflow
(`.github/workflows/docs-auto-merge.yml`) enables GitHub's native auto-merge
(squash) on the PR. The PR still waits for required status checks before it
actually merges.

Guidance for the cloud agent:
- If your PR touches **only** files under `docs/`, apply the `docs-only`
  label so the workflow can auto-merge once checks pass.
- If the PR touches **any** file outside `docs/` (source, tests, workflows,
  configuration, `.github/**`, etc.), do **not** apply the `docs-only`
  label. Leave the PR for human review.
- Never bypass the workflow with a direct merge; the human-review path is
  the default and only exception is the docs-only path above.
