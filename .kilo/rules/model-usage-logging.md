# Model Usage Logging

## Purpose

The project must keep one visible model-usage log so the user can verify which model each agent was configured to use, what the task expected, and what the agent reported.

Log file:

`MODEL_USAGE_LOG.md`

## Required logging

Every completed, blocked, or failed task must add one row to `MODEL_USAGE_LOG.md` before final approval. If the primary architect cannot edit the file directly, it must delegate the log append to the assigned implementation subagent or `housekeeping-free`.

## Required row fields

Each row must include:

- Date/time
- Task ID
- Agent
- Configured model from agent file
- Configured variant/reasoning
- Cost profile
- Intended model guidance
- Actual reported model
- Actual reported reasoning
- Escalation used
- Status
- Evidence link or short note

If the actual runtime model is not visible to the agent, write `not visible to agent` and include the configured model from frontmatter. The architect must still check the Kilo UI or task log when possible.

## Approval gate

Architect must not mark a task `APPROVED` until:

- The task report includes model/cost fields.
- `MODEL_USAGE_LOG.md` has a row for the task.
- Any deviation from configured model policy is explained.

## Configured project models

- `architect`: `openai/gpt-5.6-terra`, variant `medium`
- `backend-engineer`: `openai/gpt-5.6-terra`, variant `medium`
- `frontend-ui`: `openai/gpt-5.6-luna`, variant `medium`
- `housekeeping-free`: `kilo-auto/free`, variant `low`
- Project default in `kilo.jsonc`: `openai/gpt-5.6-terra`

## Free housekeeping rule

For simple Git sync, commit/push preparation, status checks, file listing, and targeted secret scans, use `housekeeping-free`. It is pinned to `kilo-auto/free` so Kilo can route to an available free model. If the user explicitly chooses another resolvable free model, update this config first.

