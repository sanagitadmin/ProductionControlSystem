# Cost And Model Routing Policy

## Core principle

All agents must treat lowest practical cost as a permanent architectural constraint. Use the cheapest model, smallest context, narrowest task, and least tool usage that can still produce reliable, reviewable work.

## User-controlled model choice

The user may change the model of any agent at any time in Kilo. Agent behavior must not depend on a hardcoded model name. Prompts and task packets should describe required capability and cost tier, not assume a fixed provider/model.

## Default routing

Use this default policy unless the user explicitly chooses different models in Kilo:

| Agent | Default economic model | Escalate only when |
| --- | --- | --- |
| `architect` | `gpt-5.6-terra` with `medium` reasoning | Important architecture, DB, Workflow, Security, or sensitive final approval: `gpt-5.6-sol` with `high` reasoning |
| `backend-engineer` | `gpt-5.6-terra` with `medium` reasoning | EF migration, concurrency, authorization, workflow executor, schema design, or hard backend failures: `gpt-5.6-sol` with `high` reasoning |
| `frontend-ui` | `gpt-5.6-luna` with `medium` reasoning | Complex UX, difficult RTL, large forms, hard responsive layout, or sensitive visual QA: `gpt-5.6-terra` with `medium` reasoning |

If one of these exact models is unavailable in the active Kilo provider, choose the cheapest available equivalent tier: low-cost for routine frontend, balanced for ordinary architecture/backend, stronger reasoning only for high-risk work.

## When to spend more

Higher-cost models or higher reasoning are allowed only when one or more are true:

- A wrong decision could cause data loss, security weakness, bad migration, or architecture rework.
- The task crosses backend/frontend contracts and ambiguity remains.
- The code touches workflow, authorization, money/data integrity, concurrency, or database schema.
- A lower-cost attempt failed and the failure evidence is available.
- The architect is doing final review of a high-risk change.

## Cost-saving techniques

Agents must use these techniques before increasing model/cost:

- Split work into small tasks with tight `FILES_ALLOWED` and acceptance criteria.
- Read only the files needed for the current task.
- Prefer summaries, file lists, diffs, and targeted searches over pasting large files.
- Reuse verified decisions and contracts instead of re-discussing them.
- Ask subagents for evidence in compact structured form.
- Run focused builds/tests first; expand only when risk requires it.
- Avoid repeated full-repository scans; scope searches to likely folders and exclude `bin`, `obj`, logs, generated output, and artifacts.
- Do not generate decorative or speculative UI; build only what the current workflow needs.
- Do not introduce frameworks, engines, abstractions, or packages unless they remove real complexity.
- Do not ask expensive agents to do mechanical file enumeration or routine formatting.

## Task packet cost fields

Architect task packets should include these fields:

```text
COST_PROFILE: low | balanced | high-risk
MODEL_GUIDANCE:
CONTEXT_BUDGET:
ESCALATION_RULE:
```

Definitions:

- `COST_PROFILE: low`: routine, small, low-risk task. Use cheapest capable model/reasoning.
- `COST_PROFILE: balanced`: ordinary implementation or review requiring care.
- `COST_PROFILE: high-risk`: data/security/workflow/schema/architecture task where higher reasoning may be justified.

`MODEL_GUIDANCE` must describe capability, not hardcode a model unless the user explicitly asks. Example: "use the lowest-cost model that can reliably edit Blazor components and run build verification".

`CONTEXT_BUDGET` should state what files or docs are allowed to be read.

`ESCALATION_RULE` must say when the subagent may ask architect to use a stronger model or more reasoning.

## Approval rule

The architect must reject or revise a task if it used a high-cost model or broad context without justification.
## Project-pinned model

This project pins each active agent model in `.kilo/agents/*.md`: architect and backend-engineer use `openai/gpt-5.6-terra` with `medium`, frontend-ui uses `openai/gpt-5.6-luna` with `medium`, and housekeeping-free uses `kilo-auto/free` with `low`. `kilo.jsonc` pins the project default to `openai/gpt-5.6-terra`. If Kilo shows a different model in the picker, verify the selected agent file and reload Kilo before starting paid work.
## Free-model override for simple housekeeping

For simple, mechanical, low-risk tasks, use a free Kilo Gateway model instead of the paid default models. Examples include:

- Git status, branch/remote inspection, commit, push, and sync after approved work
- Listing changed files
- Checking `.gitignore`
- Secret scans using targeted patterns
- Build-result summarization
- Cleanup recommendations that do not edit source logic
- Formatting a report from already gathered evidence

Preferred free-model order for these tasks:

1. `kilo-auto/free` as the stable default free router
2. A specific resolvable free model from the picker, such as NVIDIA free or Laguna free, only after updating the agent config
3. `nex-agi/nex-n2.5-pro:free` only if Kilo resolves it in this installation

Rules:

- Free-model override is only for `COST_PROFILE: low` housekeeping.
- Do not use free-model override for database schema, migrations, security, workflow, concurrency, business-rule design, or final review of high-risk work.
- If the exact free model is unavailable, choose the cheapest available Kilo free model and report it in `MODEL_USED`.
- The task packet must explicitly say `MODEL_GUIDANCE: use free Kilo Gateway model for housekeeping`.
- If a free model fails or cannot use required tools reliably, return `BLOCKED` or ask architect to reroute; do not silently escalate to a paid model.


## Git/GitHub Sync Policy

This section governs when and how the three-agent team commits and pushes changes. It applies to all agents and is enforced by `housekeeping-free` on a free Kilo Gateway model.

### 1. Local commit criteria
- Commit only after an **architect-approved** task.
- Changes must be **meaningful** (not temporary, incomplete, or cosmetic-only).
- Appropriate **build/test evidence** must exist for the changed scope.

### 2. Push triggers (remote sync checkpoints)
Push to `origin` only at defined boundaries:
- Approved phase boundaries (e.g., PCS-002, PCS-003, PCS-004 completion).
- Before high-risk work: DB schema, migrations, EF/Identity, workflow, authorization.
- After Kilo/agent/cost-policy configuration changes.
- User-requested explicit sync.
- Explicit rollback checkpoint requested by architect.

### 3. No commit/push for
- Temporary, incomplete, or unapproved changes — report state only.
- Work that has not passed architect approval.

### 4. Pre-commit/push gate (run by `housekeeping-free`)
Before any commit or push:
1. `git status` — confirm clean intent.
2. Review changed/staged files — scope matches approved task.
3. Targeted secret scan — explicit patterns for passwords, connection strings, tokens.
4. Proportionate build/test — `dotnet build` for backend; UI smoke if frontend changed.
5. `MODEL_USAGE_LOG.md` entry exists for the task being synced.

### 5. Blockers — commit/push must be BLOCKED if any detected
- Secrets, connection strings, passwords, tokens in staged changes.
- Temporary/build artifacts (`bin/`, `obj/`, logs, artifacts).
- Out-of-scope changes not covered by the approved task.
- Model mismatch: `MODEL_CONFIGURED` ≠ `MODEL_VISIBLE_IN_UI` or `CONFIG_MATCHES_UI: no`.

### 6. Simple sync work routing
- All Git status, commit, push, sync, file listing, secret scan, and log formatting **must** use `housekeeping-free` on a **free Kilo Gateway model** (`kilo-auto/free` / `low`).
- **No paid fallback**. If the free model is unavailable or fails, return `BLOCKED` and ask architect to reroute.

### 7. Post-push report fields (required in sync task result)
- Branch name
- Remote name
- Commit hash (short)
- Push result (success/failure/output)
- Build/test outcome
- Secret scan result (clean/findings)
- Model report: `MODEL_USED`, `REASONING_USED`, `COST_PROFILE`
- Configuration-policy match: `CONFIG_MATCHES_UI: yes/no/unknown`

### 8. Model verification gate (before starting any sync task)
The sync task must report these fields before proceeding:
- `MODEL_CONFIGURED`: from agent frontmatter (e.g., `kilo-auto/free` / `low`)
- `MODEL_VISIBLE_IN_UI`: what Kilo Agent Manager shows for this agent (`unknown` if not visible)
- `MODEL_USED_REPORTED`: what the runtime reports (`not visible to agent` if hidden)
- `CONFIG_MATCHES_UI`: `yes` / `no` / `unknown` — mismatch blocks the task
- If UI visibility is unavailable, record `unknown` — do not guess and do not treat as a match.
