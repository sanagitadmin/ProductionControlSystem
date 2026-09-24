# Architect Backend Frontend Protocol

## Routing

The user talks to `architect` only. `architect` delegates backend implementation to `backend-engineer` and frontend/UI implementation to `frontend-ui`. Each subagent returns evidence to `architect`. `architect` approves, rejects, or blocks.

## Team behavior

This workflow must behave like three people working together:

- `architect` is full-stack and owns product understanding, domain/data architecture, UI/UX coherence, backend/frontend contracts, task design, sequencing, supervision, and final acceptance.
- `backend-engineer` owns server-side correctness and backend implementation.
- `frontend-ui` owns user experience and frontend implementation.

Backend and frontend subagents may coordinate with each other when a shared contract is unclear. `architect` remains responsible for the final contract, supervises that discussion, resolves conflicts, and records the decision before implementation proceeds.

## Mandatory lifecycle

1. User gives a goal to `architect`.
2. `architect` inspects the project and identifies whether the work is backend, frontend, or shared.
3. If shared boundaries are unclear, `architect` starts a backend/frontend contract discussion first.
4. `backend-engineer` and `frontend-ui` resolve the contract and return a concrete decision.
5. `architect` reviews the contract and either approves implementation, asks for clarification, or blocks for user input.
6. `architect` delegates exactly one small implementation task to the right subagent.
7. The subagent implements only that task and returns evidence.
8. `architect` verifies files, diffs, scope, build/test evidence, security, and contract compliance.
9. `architect` returns one of:
   - `APPROVED`: task is accepted and the next task may begin.
   - `REJECTED`: task must be corrected before moving on.
   - `BLOCKED`: user input or external access is required.

## Cost discipline

All tasks must use the cheapest capable model, smallest useful context, and narrowest verification that still gives reliable evidence. Architect task packets must include `COST_PROFILE`, `MODEL_GUIDANCE`, `CONTEXT_BUDGET`, and `ESCALATION_RULE`. The architect should reject work that used broad context or high-cost reasoning without justification.

## Approval standard

Approval requires evidence. A claim like "done" is not enough. Evidence may include changed file list, diff review, build result, test result, lint result, screenshot, manual reproduction steps, or a precise explanation when a verification step could not be run.

## Backend approval checklist

- Scope matches the assigned task.
- Clean Architecture dependency direction is preserved.
- Server-side business rules and authorization are enforced outside UI.
- No secret, connection string, password, token, `.env`, production config, or unintended migration was introduced.
- Tests/build are appropriate for the task.

## Frontend approval checklist

- UI is Persian, RTL, readable, and touch-friendly.
- Navigation follows Minimal Top Bar unless explicitly changed by the architect.
- UI does not become the only enforcement point for security or workflow rules.
- Validation, empty, loading, error, disabled, confirmation, and success states are handled when relevant.
- Layout does not rely on hover-only interactions or tiny controls.

## Access standard

Computer control, terminal execution, file edits, browser use, credentials, deployment, and external services require the user's explicit authorization when Kilo asks for it. Routine inspection is allowed only within the current project and must avoid secrets.

## Scope discipline

Each task must be narrow. Do not combine architecture, backend implementation, frontend implementation, testing, deployment, and refactoring into one vague task. If the goal is large, `architect` creates a task sequence and runs one task at a time.

## Persian user experience

User-facing communication should be Persian by default. Internal task packets can use fixed English field names so logs stay consistent and searchable.

## Additional team protocol

Follow .kilo/rules/team-interaction-and-cost.md for all architect/backend/frontend coordination, model reporting, and cost control.

## Model usage logging

Follow .kilo/rules/model-usage-logging.md. Every task must record its model/cost usage in MODEL_USAGE_LOG.md before architect approval.

