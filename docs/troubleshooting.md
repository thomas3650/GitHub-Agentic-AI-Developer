# Troubleshooting docs auto-merge

Companion to [README.md](README.md). Use this when a docs-only PR does
not sail through auto-merge as expected.

## Auto-merge never enabled

Symptom: PR is open, `docs-only` label applied, but GitHub does not show
"Auto-merge enabled" in the merge box.

Check, in order:

1. **Label spelling.** The workflow gates on the exact label name
   `docs-only`. `docs`, `docs only`, or `Docs-Only` will not match.
2. **All changed paths are under `docs/`.** Even one non-`docs/` file
   (source, tests, workflows, config, `.github/**`) disqualifies the PR.
   Run `gh pr view <n> --json files -q '.files[].path'` to see the full
   list. Renames count on both sides — the pre-rename path must also be
   under `docs/`.
3. **PR is not a draft.** Auto-merge cannot be enabled on a draft PR.
   Mark it ready for review.
4. **Docs Auto-Merge workflow ran.** Open the PR's "Checks" tab; look
   for the `Docs Auto-Merge` run and read its logs. It will explicitly
   log which eligibility check failed.

## `build-test` failing

The `Pull Request Validation` workflow runs `dotnet restore / build /
test`. A pure docs change should not affect it, so a failure here almost
always means either:

- an unrelated infrastructure hiccup (rerun the job), or
- the PR is accidentally not docs-only after all (see previous section).

## `copilot-review-clean` stays pending

The [copilot-review-gate.yml](../.github/workflows/copilot-review-gate.yml)
status check turns green only when Copilot has reviewed the current head
commit with no unresolved inline comments and no `CHANGES_REQUESTED`.

Common causes:

- **Copilot has not reviewed yet.** The `Docs Auto-Merge` workflow
  requests a review on eligibility; give it a minute and refresh.
- **Copilot posted a review on an older commit.** Any push resets the
  gate to the new head. Wait for the new review.
- **Copilot requested changes or left unresolved inline comments.**
  Address them with an additional commit on the same branch, or resolve
  the threads if the concern no longer applies. The gate re-evaluates
  automatically.

## PR fell back to the human-review path

Symptom: auto-merge was previously enabled, then GitHub disabled it and
the PR now waits for a human reviewer.

The auto-merge workflow revokes eligibility when either condition stops
holding:

- The `docs-only` label was removed.
- A new push introduced a non-`docs/` path (including as the pre-rename
  side of a rename).

Restore both conditions if the PR should still auto-merge, or leave the
PR as-is and let a human review it.

## Escaping the workflow

Do not merge a docs-only PR by hand while the pipeline is running — it
short-circuits the review gate. If auto-merge is genuinely broken, fix
the workflow rather than bypassing it.
