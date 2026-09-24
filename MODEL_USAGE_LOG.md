# Model Usage Log

This log records the model policy and reported model usage for project-agent tasks.

Configured project models:

| Agent | Configured model | Variant / reasoning | Purpose |
| --- | --- | --- | --- |
| architect | openai/gpt-5.6-terra | medium | Full-stack architecture, review, acceptance |
| backend-engineer | openai/gpt-5.6-terra | medium | Backend implementation and evidence |
| frontend-ui | openai/gpt-5.6-luna | medium | Blazor Persian/RTL UI work |
| housekeeping-free | openai/gpt-5.4-mini | low | Git sync, status, commit/push prep, logs, simple housekeeping |

Usage entries:

| Date/Time | Task ID | Agent | Configured Model | Variant | Cost Profile | Intended Guidance | Actual Reported Model | Actual Reported Reasoning | Escalation | Status | Evidence |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| 2026-09-24 | CONFIG-BASELINE | system | see table above | see table above | n/a | Project model policy established | n/a | n/a | no | recorded | Agent frontmatter and kilo.jsonc updated |
| 2026-09-24 | PCS-GIT-001-FIRST-GITHUB-SYNC | housekeeping-free | nex-agi/nex-n2.5-pro:free | low | low | first GitHub sync using free model | not started - model unavailable | not started | no | BLOCKED | Kilo returned Model not found; config changed to kilo-auto/free for retry |
| 2026-09-24T12:21:03Z | PCS-GIT-001-FIRST-GITHUB-SYNC | housekeeping-free | kilo-auto/free | low | low | first GitHub sync using free Kilo Gateway model | not visible to agent | not visible to agent | no | DONE | Build 0 errors; secret scan clean; .gitignore created; 37 files staged; initial commit b92f1d1; pushed to origin/main |
| 2026-09-24T12:28:33Z | PCS-GIT-002-SYNC-POLICY | housekeeping-free | kilo-auto/free | low | low | record Git/GitHub sync policy in docs | not visible to agent | not visible to agent | no | DONE | Policy section added to cost-and-model-routing.md; log entry added here |
| 2026-09-24T16:13:44Z | PCS-GIT-003-SYNC-POLICY | housekeeping-free | kilo-auto/free | low | low | commit and push approved policy docs | not visible to agent | not visible to agent | no | IN PROGRESS | Staging approved .kilo/rules/cost-and-model-routing.md and MODEL_USAGE_LOG.md |
| 2026-09-24 | PCS-CONFIG-LOWCOST-HOUSEKEEPING | architect | openai/gpt-5.4-mini | low | low | switch housekeeping from free router due rate limits | not run | not run | no | DONE | housekeeping-free config changed from kilo-auto/free to openai/gpt-5.4-mini; no source code changed |

