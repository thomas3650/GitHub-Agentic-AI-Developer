# Code review instructions for GitHub cloud agent

Focus review feedback on correctness, regressions, and missing verification.

## Review priorities
1. **Behavioral correctness**
   - Validate endpoint behavior for `/weather` and `/weather/description`.
   - Flag breaking response-shape changes or validation regressions.
2. **Test coverage impact**
   - Check whether behavior changes are reflected in `SimpleWeatherApi.Tests`.
   - Request tests when new paths, edge cases, or validation logic are introduced.
3. **Workflow safety**
   - Ensure changes remain compatible with the pull request validation workflow (`restore`, `build`, `test`).
4. **Signal quality**
   - Avoid style-only or subjective feedback unless it directly affects readability, maintainability, or correctness.

## What to avoid
- Do not request broad architectural rewrites for small bug fixes.
- Do not suggest changes unrelated to the pull request's intent.
