<!--
Thanks for opening a pull request! This template exists to make the repo's
governance rules visible before you hit "Create pull request". Please fill
in the sections that apply; delete the ones that don't.

Governance quick reference:
  * Required checks on main: `build-test`, `copilot-review-clean`
  * CODEOWNERS: @thomas3650 owns everything except `docs/**`
  * Auto-merge path: docs-only PRs with the `docs-only` label
  * Everything else: human review by @thomas3650
-->

## Summary

<!-- One or two sentences describing what this PR changes and why. -->

## Type of change

<!-- Check ONE. This determines the review path. -->

- [ ] **Docs-only** — every changed path is under `docs/`. Eligible for the
      `docs-only` label + auto-merge path.
- [ ] **Bug fix** — non-breaking change that fixes an issue.
- [ ] **Feature** — non-breaking change that adds functionality.
- [ ] **Breaking change** — fix or feature that changes existing behavior
      (API shape, validation, response format, etc.).
- [ ] **CI / workflow / repo config** — changes under `.github/**` or repo
      settings. Never docs-only, even if it "feels" like documentation.
- [ ] **Refactor / chore** — no behavior change.

## Related issues

<!--
Link the issue(s) this PR resolves. Use "Closes #N" / "Fixes #N" so GitHub
auto-closes them on merge. If there is no issue, briefly explain why.
-->

Closes #

## Validation

<!--
Confirm you ran the commands from .github/copilot-instructions.md at the
repo root. For docs-only PRs, the commands still need to pass in CI but
you don't have to run them locally.
-->

- [ ] `dotnet restore GitHubAgenticAIDeveloper.slnx`
- [ ] `dotnet build GitHubAgenticAIDeveloper.slnx --configuration Release --no-restore`
- [ ] `dotnet test GitHubAgenticAIDeveloper.slnx --configuration Release --no-build`
- [ ] Not applicable — docs-only change.

## Checklist

- [ ] Changes are focused and minimal — no unrelated refactors or
      formatting-only churn in touched files.
- [ ] Behavior of `/weather` and `/weather/description` is preserved,
      unless this PR explicitly changes it.
- [ ] If input validation or response shape changed, tests in
      `SimpleWeatherApi.Tests` were updated in the same change.
- [ ] If this touches `.github/**`, source, tests, or config, the
      `docs-only` label is **not** applied.
- [ ] If this is docs-only, the `docs-only` label is applied so
      `.github/workflows/docs-auto-merge.yml` can arm auto-merge.
- [ ] I have not bypassed the workflow (no direct pushes to `main`, no
      admin merge overriding required checks).

## Notes for reviewers

<!--
Optional. Anything worth calling out: risky areas, follow-up work,
screenshots, benchmark numbers, etc.
-->
