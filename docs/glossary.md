# Glossary

Short definitions of the terms used across this repository's docs
auto-merge policy. See [README.md](README.md) for the pipeline overview
and [troubleshooting.md](troubleshooting.md) for failure modes.

## `docs-only` label

A repository label that a PR author (or the cloud agent) applies to
signal "this PR touches only `docs/**` and is safe to auto-merge". The
[Docs Auto-Merge](../.github/workflows/docs-auto-merge.yml) workflow
gates on the exact string `docs-only`. Removing the label revokes
auto-merge eligibility on the next event.

## Native auto-merge

GitHub's built-in feature that queues a PR to merge as soon as required
status checks pass and any required reviews are satisfied. Enabled by
`gh pr merge --auto --squash` from the `Docs Auto-Merge` workflow. The
workflow does not merge the PR itself; it only asks GitHub to merge it
when the checks turn green.

## `build-test`

The status check produced by
[pull-request-validation.yml](../.github/workflows/pull-request-validation.yml).
Runs `dotnet restore`, `dotnet build`, and `dotnet test` against the
solution. Must be green before any PR — docs-only or not — merges.

## `copilot-review-clean`

The status check produced by
[copilot-review-gate.yml](../.github/workflows/copilot-review-gate.yml).
Turns green only when Copilot has reviewed the current head commit with
no unresolved inline comments and no `CHANGES_REQUESTED`. Any new push
resets the gate to the new head.

## CODEOWNERS

The [`.github/CODEOWNERS`](../.github/CODEOWNERS) file that assigns
default reviewers for paths in the repository. Combined with branch
protection's "require code owner review" setting, it enforces the
human-review path for any change outside `docs/**`. Paths under
`docs/**` are intentionally unowned so docs-only PRs can auto-merge
without a code-owner review.

## Human-review path

The default merge path for anything that is not a docs-only PR: the
author opens a PR, code owners review it, required checks pass, and a
human merges it manually or via GitHub's auto-merge queue with the
appropriate review approvals.

## Docs-only PR

A PR whose entire diff — including the pre-rename side of any file
renames — is contained under `docs/**`. Necessary but not sufficient
for auto-merge; the `docs-only` label must also be applied.
