# Model Usage Log

This log records the model policy and reported model usage for project-agent tasks.

Configured project models:

| Agent | Configured model | Variant / reasoning | Purpose |
| --- | --- | --- | --- |
| architect | openai/gpt-5.6-terra | medium | Full-stack architecture, review, acceptance |
| backend-engineer | openai/gpt-5.6-terra | medium | Backend implementation and evidence |
| frontend-ui | openai/gpt-5.6-luna | medium | Blazor Persian/RTL UI work |
| housekeeping-free | kilo-auto/free | low | Git sync, status, commit/push prep, logs, simple housekeeping |

Usage entries:

| Date/Time | Task ID | Agent | Configured Model | Variant | Cost Profile | Intended Guidance | Actual Reported Model | Actual Reported Reasoning | Escalation | Status | Evidence |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| 2026-09-24 | CONFIG-BASELINE | system | see table above | see table above | n/a | Project model policy established | n/a | n/a | no | recorded | Agent frontmatter and kilo.jsonc updated |
| 2026-09-24 | PCS-GIT-001-FIRST-GITHUB-SYNC | housekeeping-free | nex-agi/nex-n2.5-pro:free | low | low | first GitHub sync using free model | not started - model unavailable | not started | no | BLOCKED | Kilo returned Model not found; config changed to kilo-auto/free for retry |

| 2026-09-24T12:21:03Z | PCS-GIT-001-FIRST-GITHUB-SYNC | housekeeping-free | kilo-auto/free | low | low | first GitHub sync using free Kilo Gateway model | not visible to agent | not visible to agent | no | IN PROGRESS | Build succeeded (0 errors), secret scan clean, .gitignore created; git init + commit + push pending |
