# Copilot Code Review — Approval Behavior Test

This file exists to validate a single question about the `docs/` auto-merge
pipeline set up in #22:

> When GitHub Copilot code review is invoked on a docs-only pull request
> that it finds no issues with, does it submit a review with
> `state = APPROVED`, or does it only submit `state = COMMENTED`?

The distinction matters because [`.github/CODEOWNERS`](../.github/CODEOWNERS)
makes `@Copilot` the sole code owner for `docs/**`. With the branch
protection rule "Require review from Code Owners" enabled on `main`, only
an APPROVED review from Copilot will satisfy the code owner requirement
and unblock GitHub's native auto-merge.

If this PR ends up with a Copilot review whose state is `APPROVED`, the
pipeline is complete: docs-only PRs will be gated on a real, model-based
approval and then merged automatically once required status checks are
green.

If the Copilot review only submits `COMMENTED` state (as observed on
PR #22, which touches workflow logic rather than docs), we need to add a
human fallback owner to `CODEOWNERS` so a docs-only PR still has a viable
path to approval.

This file is intentionally trivial and should be removed after the
observation is recorded — it has no value as documentation.
