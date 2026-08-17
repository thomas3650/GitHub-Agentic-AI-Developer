# Documentation

Project documentation for the GitHub Agentic AI Developer repository — a
reference implementation and validation playground for GitHub Copilot cloud
agent and code review workflows built around a small ASP.NET Core minimal
API (`SimpleWeatherApi`).

## What lives here

Documents describing the repository, the automation around it, and how to
work with the cloud agent. Source code, tests, and workflow files live
outside this folder — see the [repository root](../) for those.

## How docs-only PRs merge

Changes under `docs/**` follow a fully automated merge path so trivial
documentation updates don't need a manual approval-and-merge click. The
pipeline is:

1. Open a PR whose changed paths are all under `docs/`.
2. Apply the **`docs-only`** label.
3. The [`Docs Auto-Merge`](../.github/workflows/docs-auto-merge.yml)
   workflow verifies both conditions still hold (including that no
   rename's pre-rename path is outside `docs/`), then enables GitHub's
   native auto-merge (squash) on the PR and requests a Copilot code
   review.
4. GitHub holds the merge until the required status checks are green:
   - **`build-test`** — `restore`, `build`, and `test` from
     [`pull-request-validation.yml`](../.github/workflows/pull-request-validation.yml).
   - **`copilot-review-clean`** — posted by
     [`copilot-review-gate.yml`](../.github/workflows/copilot-review-gate.yml).
     Turns green only when Copilot has reviewed the current head commit
     with no unresolved inline comments and no `CHANGES_REQUESTED`.
5. Once both checks pass, GitHub squash-merges the PR and deletes the
   head branch.

If either eligibility condition later stops holding (the `docs-only`
label is removed, or a follow-up push adds a non-`docs/` path), the same
workflow revokes auto-merge on the next event and the PR falls back to
the standard human-review path.

The full policy — including the CODEOWNERS design and the required
branch-protection settings — is documented for the cloud agent in
[`.github/copilot-instructions.md`](../.github/copilot-instructions.md).

## Contributing a docs change

- Keep the change scoped to `docs/**`. Any file outside `docs/` disqualifies
  the PR from auto-merge (this is by design).
- Add the `docs-only` label when you open the PR so the workflow can pick
  it up.
- Anything larger than a docs update (source, tests, workflows, config,
  `.github/**`) should be a separate PR without the `docs-only` label; it
  will go through the standard human-review path.

## When auto-merge doesn't work

See [troubleshooting.md](troubleshooting.md) for the failure modes of the
auto-merge pipeline (label not matched, non-`docs/` paths sneaking in,
`copilot-review-clean` staying pending, PR falling back to human review).

## Opening a PR

Every new PR is prefilled from
[`.github/pull_request_template.md`](../.github/pull_request_template.md).
See [pr-template.md](pr-template.md) for what each section is for and
how it maps to the review paths above.
