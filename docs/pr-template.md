# Pull request template

This repository ships a pull request template at
[`.github/pull_request_template.md`](../.github/pull_request_template.md).
GitHub prefills it into the body of every new PR opened against `main`,
whether the PR is created from the web UI or from a tool like the GitHub
cloud agent.

## What the template asks for

- **Summary** — one or two sentences on what changed and why.
- **Type of change** — a taxonomy that maps directly to the review path:
  docs-only, bug fix, feature, breaking change, CI / workflow / repo
  config, refactor / chore. Only "docs-only" is eligible for the
  automated merge path.
- **Related issues** — `Closes #N` / `Fixes #N` so GitHub auto-closes
  them when the PR merges.
- **Validation** — checkboxes for the three commands from
  [`.github/copilot-instructions.md`](../.github/copilot-instructions.md)
  (`dotnet restore`, `build`, `test`), with a "not applicable —
  docs-only" escape hatch.
- **Checklist** — the non-negotiable guardrails: keep changes focused,
  preserve `/weather` and `/weather/description` behavior, update tests
  when validation or response shape changes, apply the `docs-only` label
  only when every path is under `docs/`, and never bypass the workflow
  with a direct merge.
- **Notes for reviewers** — optional context: risky areas, follow-up
  work, screenshots.

## What the template does not do

- It does not enforce anything by itself. Enforcement lives in the
  ruleset on `main` (`build-test`, `copilot-review-clean`), in
  [`.github/CODEOWNERS`](../.github/CODEOWNERS), and in
  [`.github/workflows/docs-auto-merge.yml`](../.github/workflows/docs-auto-merge.yml).
- It does not decide which review path applies. That's decided by the
  actual changed paths plus the presence or absence of the `docs-only`
  label, not by which checkbox you tick.

## Filling it out

Delete sections that don't apply and leave the checkboxes you don't need
unchecked. The template is a starting point, not a form to submit.
