---
name: code-review
description: Repository-tailored review procedure for GitHub Copilot code review on the SimpleWeatherApi solution. Use this skill when reviewing any pull request in this repository so feedback aligns with the ASP.NET Core minimal API contract, xUnit test conventions, the pull request validation workflow, and the docs-only auto-merge policy.
---

# `code-review` skill — SimpleWeatherApi

Use this skill on every pull request in this repository. It complements
[`.github/copilot-instructions.md`](../../copilot-instructions.md) (repo
context and workflow) and
[`.github/instructions/code-review.instructions.md`](../../instructions/code-review.instructions.md)
(review priorities) by turning them into a repeatable review procedure.

## Repository at a glance

- **Production code:** `SimpleWeatherApi/` — ASP.NET Core minimal API,
  endpoints `/weather` and `/weather/description`, with a
  `CityQueryValidator` that normalizes and validates the `city` query
  parameter.
- **Tests:** `SimpleWeatherApi.Tests/` — xUnit, one test class per unit
  under test, `[Theory]` + `[InlineData]` for parameterized cases.
- **Solution:** `GitHubAgenticAIDeveloper.slnx`.
- **CI:** `.github/workflows/pull-request-validation.yml` runs
  `restore` → `build` → `test`, publishing the required `build-test`
  check.
- **Docs-only auto-merge:** governed by
  `.github/workflows/docs-auto-merge.yml` and
  `.github/workflows/copilot-review-gate.yml`. Auto-merge is armed only
  when every changed path is under `docs/**` **and** the PR carries the
  `docs-only` label.

## Review procedure

Walk each PR through these passes in order. Skip a pass only if it is
provably irrelevant to the diff.

### 1. Classify the PR

Determine the change type from the diff, not just the PR body:

- **docs-only** — every changed path is under `docs/**`.
- **tests-only** — every changed path is under `SimpleWeatherApi.Tests/`.
- **workflow / config** — touches `.github/**` or repo configuration.
- **production behavior** — touches `SimpleWeatherApi/`.
- **mixed** — any combination of the above.

If the PR body's type checkbox contradicts the diff (for example, body
says "docs-only" but the diff touches `SimpleWeatherApi/`), flag it —
the docs-only auto-merge policy depends on this classification being
honest.

### 2. Behavioral correctness (production or mixed PRs)

- **Endpoint contract:** verify `/weather` and `/weather/description`
  still return the same response shape (status codes, JSON schema,
  error bodies) unless the PR explicitly changes the contract.
- **Input validation:** confirm `CityQueryValidator.TryNormalize`
  semantics are preserved — trims surrounding whitespace only, does
  **not** collapse internal whitespace, and rejects null / empty input
  consistently.
- **Error paths:** ensure new failure modes produce explicit,
  pattern-consistent error responses rather than throwing.
- If any of the above change intentionally, require a matching test
  update in the same PR.

### 3. Design quality (SOLID + DI)

- Prefer single-responsibility services with clear interface
  boundaries.
- Ensure DI registration in `Program.cs` matches actual usage
  (lifetime, interface bindings) and stays testable.
- Flag duplication that would let behavior drift between call sites —
  centralize it.
- Do **not** request broad architectural rewrites for a small,
  localized fix. Right-size feedback to the PR's scope.

### 4. Test discipline (TDD mindset)

- Any behavior change, bug fix, or new edge case must be covered by a
  test in `SimpleWeatherApi.Tests/`.
- Prefer `[Theory]` + `[InlineData]` for parameterized cases (see
  `CityQueryValidatorTests` for the established style).
- Cover both the happy path and the failure / validation path.
- Test names should describe the observable behavior, not the
  implementation.

### 5. Workflow safety

- Verify the change remains compatible with the `restore` → `build` →
  `test` sequence in
  `.github/workflows/pull-request-validation.yml`.
- If the PR touches `.github/workflows/**`, confirm it does not weaken
  the `build-test` or `copilot-review-clean` required checks and does
  not bypass the auto-merge eligibility rules.
- If the PR touches `.github/CODEOWNERS`, confirm the docs-only
  exemption still holds (see the file's inline design intent).

### 6. Docs-only auto-merge policy check

Independent of correctness feedback, verify the PR's classification and
labels are consistent with the auto-merge policy:

- If **every** changed path is under `docs/**`, the PR **should**
  carry the `docs-only` label. If it does not, note that the
  auto-merge workflow will not arm.
- If **any** changed path is outside `docs/**`, the PR **must not**
  carry the `docs-only` label. If it does, flag it as a policy
  violation — auto-merge would either fail to arm or be revoked, and
  the label is misleading to reviewers.
- Never suggest bypassing the workflow with a direct merge.

## Signal quality — what to leave in and out

Leave **in**:

- Correctness bugs, missing validation, response-shape regressions.
- Missing tests for behavior changes or new edge cases.
- DI / lifetime mistakes or interface-boundary violations that affect
  testability.
- Auto-merge policy inconsistencies (label vs. diff scope).

Leave **out**:

- Style-only nits that do not affect correctness, maintainability, or
  readability.
- Unrelated refactor suggestions ("while you're here…").
- Broad architectural rewrites for a small, localized fix.
- Suggestions that expand the PR's scope beyond its stated intent —
  offer them as follow-up ideas in the summary rather than as
  requested changes on the diff.

## Output format

Structure the review as:

1. **Summary** — one paragraph: what the PR does and the review verdict
   (clean / minor comments / blocking concerns).
2. **Findings** — grouped by the passes above. Cite file paths and line
   ranges. Distinguish **blocking** (must fix), **should fix**, and
   **nit / follow-up**.
3. **Auto-merge check** — one line: label matches diff scope?
   yes / no / N/A.

If the PR is clean, say so plainly and do not invent findings.
