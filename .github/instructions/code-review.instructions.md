# Code review instructions for GitHub cloud agent

Focus feedback on correctness, design quality, and test confidence.

## Review priorities
1. **Behavioral correctness**
   - Validate endpoint behavior for `/weather` and `/weather/description`.
   - Flag response-shape changes, validation regressions, and unhandled error paths.
2. **Design quality (SOLID + DI)**
   - Prefer single-responsibility services and clear interface boundaries.
   - Ensure dependency injection registration and usage stay coherent and testable.
   - Identify duplication that should be centralized when it risks behavior drift.
3. **Test discipline (TDD mindset)**
   - Require tests for behavior changes, bug fixes, and edge-case handling.
   - Check that tests cover both happy paths and failure/validation paths.
4. **Workflow safety**
   - Ensure changes remain compatible with the pull request validation workflow (`restore`, `build`, `test`).
5. **Signal quality**
   - Avoid style-only feedback unless it directly affects correctness, maintainability, or readability.

## What to avoid
- Do not request broad architectural rewrites for small, localized fixes.
- Do not suggest changes unrelated to the pull request's intent.
