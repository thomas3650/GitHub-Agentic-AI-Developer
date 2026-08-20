# Copilot instructions for GitHub cloud agent

## Repository context
- This repository contains a small ASP.NET Core minimal API in `SimpleWeatherApi` and xUnit tests in `SimpleWeatherApi.Tests`.
- Main endpoints are `/weather` and `/weather/description` with `city` query validation.

## Agent personas
This repo defines a two-agent, plan-then-implement workflow. Both persona
files work in the GitHub Copilot cloud agent and locally (VS Code Copilot
Chat, Copilot CLI). Activate one by pointing an agent at the file
(e.g. "Act as the agent described in `.github/agents/planner.md`").

- [`.github/agents/planner.md`](./agents/planner.md) — use when a request
  needs a plan before code is written. The planner never edits code; it
  posts a structured plan directly in the current chat / PR / issue
  conversation and ends with an explicit
  `Handoff → @implementer (see .github/agents/implementer.md): please execute the plan above.` line.
- [`.github/agents/implementer.md`](./agents/implementer.md) — use to
  execute a plan the planner has just posted in the same conversation.
  The implementer treats the plan's scope as fixed, opens a PR, and may
  ask only about implementation details (naming, file placement, minor
  design choices).

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
(squash) on the PR and requests a Copilot code review. The PR still
waits for the required status checks before it actually merges:

- **`build-test`** — the Pull Request Validation workflow (`restore`,
  `build`, `test`) must pass.
- **`copilot-review-clean`** — posted by
  `.github/workflows/copilot-review-gate.yml`; turns `success` only when
  Copilot code review has reviewed the current head commit with no
  unresolved inline comments and no `CHANGES_REQUESTED`.

No human approval is required for docs-only PRs: `docs/**` is
intentionally unowned in `.github/CODEOWNERS`, so with
`require_code_owner_review: true` and `required_approvals: 0` the code
owner gate is vacuous for docs-only PRs. Non-docs paths are still owned
by `@thomas3650`, so anything outside `docs/**` continues to need a
human review.

If either eligibility condition later stops holding (the label is
removed, or a follow-up push adds a non-`docs/` path — including as the
pre-rename side of a rename), the same workflow revokes auto-merge on the
next event so the PR falls back to the human-review path.

Guidance for the cloud agent:
- If your PR touches **only** files under `docs/`, apply the `docs-only`
  label so the workflow can auto-merge once checks pass.
- If the PR touches **any** file outside `docs/` (source, tests, workflows,
  configuration, `.github/**`, etc.), do **not** apply the `docs-only`
  label. Leave the PR for human review.
- Never bypass the workflow with a direct merge; the human-review path is
  the default and only exception is the docs-only path above.
