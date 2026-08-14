# FAQ

Short answers to the questions that come up most often about the docs
auto-merge pipeline. See [README.md](README.md) for the overview and
[troubleshooting.md](troubleshooting.md) for concrete failure modes.

## Do I need someone to review my docs PR?

No. If your PR touches only files under `docs/` and carries the
`docs-only` label, the [Docs Auto-Merge](../.github/workflows/docs-auto-merge.yml)
workflow will enable GitHub's native auto-merge. Once `build-test` and
`copilot-review-clean` are green the PR squash-merges automatically.

## Can I add the `docs-only` label to a PR that also touches source code?

You can — but the workflow will refuse to enable auto-merge because at
least one changed path is outside `docs/`. The label alone is not
enough; both conditions must hold. Non-docs paths always follow the
human-review path.

## What happens if I push a code change on top of an already-eligible docs PR?

The workflow re-evaluates on every `pull_request` event. As soon as the
new push introduces a non-`docs/` path, GitHub disables auto-merge and
the PR falls back to the human-review path. Removing the offending
change (or moving it to a separate PR) restores eligibility.

## Who is `app/github-actions` in the merge history?

That's the actor GitHub records when a PR is merged by native
auto-merge. If you see it as the merger of a docs-only PR, that means
the pipeline worked as designed.

## Why did my PR merge without a Copilot approval?

Copilot code review in this repo submits `COMMENTED` reviews, never
`APPROVED`. Approval is not required for docs-only PRs — the pipeline
only requires that Copilot has reviewed the current head commit and
left no unresolved inline comments or `CHANGES_REQUESTED`. See the
[Copilot Review Gate](../.github/workflows/copilot-review-gate.yml)
workflow for the exact evaluation logic.
