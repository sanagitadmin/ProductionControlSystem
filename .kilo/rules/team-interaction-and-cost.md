# Team Interaction And Cost Protocol

## Principle

The team must behave like three people working together, but with strict cost discipline. Every interaction must have a purpose, a small context, and a concrete output. Do not use inter-agent discussion as a substitute for clear task design.

## Roles

- `architect` is the full-stack lead. It owns domain/data architecture, UI/UX coherence, backend/frontend contracts, sequencing, cost control, review, and final acceptance.
- `backend-engineer` owns backend implementation, server-side rules, Domain/Application/Infrastructure work when assigned, tests, and backend evidence.
- `frontend-ui` owns Blazor UI, Persian/RTL user experience, touch-friendly forms, visual states, frontend contracts, and UI evidence.

## Who talks to whom

- The user talks only to `architect`.
- `architect` may delegate to `backend-engineer` or `frontend-ui`.
- `backend-engineer` and `frontend-ui` may talk to each other only when a shared backend/frontend contract is unclear or a task explicitly requires coordination.
- `architect` supervises all cross-agent discussion and owns the final decision.

## Cost-first communication rules

Before any agent asks another agent a question, it must check:

1. Can this be answered from the current task packet?
2. Can this be answered from already verified project rules?
3. Can this be resolved by reading one or two specific files instead of asking broadly?
4. Is this ambiguity important enough to affect implementation or acceptance?

If the answer is not important for implementation or acceptance, do not start an inter-agent discussion.

## Interaction budget

Default limits unless `architect` explicitly approves more:

- One focused discussion per task.
- Maximum 3 questions in one discussion.
- Maximum 1 response round from each subagent before architect decides.
- No broad repository scan for discussion.
- No reading `bin`, `obj`, logs, generated output, artifacts, or unrelated old repository files.
- No re-reading old decisions that the architect has already included in the task packet.

## When backend and frontend must coordinate

Coordinate only when at least one is true:

- UI needs DTOs, fields, validation semantics, workflow actions, permission flags, or routes not yet specified.
- Backend changes affect visible UI states, validation messages, error handling, or action availability.
- A page/form depends on a server contract that does not exist yet.
- Permission visibility and server authorization must be aligned.
- There is a risk frontend will invent business rules or backend will ignore UX requirements.

## Discussion packet

A coordination discussion must use this format:

```text
DISCUSSION_ID:
COST_PROFILE: low | balanced | high-risk
CONTEXT_BUDGET:
QUESTION:
KNOWN_DECISIONS:
OPTIONS:
RECOMMENDED_DECISION:
RISKS_IF_WRONG:
OUTPUT_REQUIRED:
```

## Discussion output

The discussion must end with this compact contract:

```text
CONTRACT_DECISION:
BACKEND_RESPONSIBILITIES:
FRONTEND_RESPONSIBILITIES:
DATA_SHAPE:
VALIDATION_AND_ERRORS:
PERMISSION_AND_WORKFLOW_RULES:
UI_STATES:
OPEN_TBD:
COST_CONTROL_NOTES:
ARCHITECT_DECISION_REQUIRED: yes/no
```

## Architect approval of discussions

`architect` must not let subagents continue debating. After the allowed discussion round, architect must do one of:

- Accept the contract and create the next task.
- Correct the contract and create the next task.
- Mark `BLOCKED` and ask the user for a business decision.

## Task sequencing

Prefer this sequence for cost and clarity:

1. Architect writes or confirms the contract.
2. Backend implements minimal server/domain contract if needed.
3. Backend returns evidence.
4. Architect verifies.
5. Frontend implements UI against the verified contract.
6. Frontend returns evidence.
7. Architect verifies.

Do not run backend and frontend implementation in parallel unless the shared contract is already stable and small.

## Frontend page rule

`frontend-ui` must not invent pages. Every UI task must include a Page Contract:

```text
PAGE_NAME:
ROUTE:
USER_GOAL:
PRIMARY_USER:
DATA_NEEDED:
ACTIONS:
FIELDS:
VALIDATION:
STATES:
PERMISSION_RULES:
BACKEND_CONTRACT:
UX_RULES:
ACCEPTANCE_CRITERIA:
```

If the Page Contract is missing or materially incomplete, `frontend-ui` must return `BLOCKED` or request a backend/frontend discussion.

## Backend contract rule

`backend-engineer` must not invent UI behavior. Every backend task that affects UI must state:

```text
SERVER_CONTRACT:
DTO_OR_RESULT_SHAPE:
VALIDATION_ERRORS:
PERMISSION_REQUIREMENTS:
WORKFLOW_ACTIONS:
FRONTEND_IMPACT:
```

If these are missing and the backend change affects UI, `backend-engineer` must request coordination with `frontend-ui`.

## Model and cost reporting

Every subagent report must include:

```text
MODEL_USED:
REASONING_USED:
COST_PROFILE:
WHY_THIS_MODEL:
ESCALATION_USED: yes/no
COST_CONTROL_NOTES:
```

If the real model is not visible to the agent, report:

```text
MODEL_USED: not visible to agent
REASONING_USED: not visible to agent
```

The agent must still report the intended cost tier from the task packet.

## Rejection rule

`architect` must reject or revise work when:

- A subagent used broad context without need.
- A subagent escalated model/reasoning without evidence.
- A frontend task invented backend rules.
- A backend task invented UI behavior.
- A shared contract was needed but no coordination happened.
- The report lacks cost/model notes or verification evidence.

## Simple housekeeping model rule

For Git sync, commit, push, file listing, and other simple housekeeping tasks, use a free Kilo Gateway model such as Nex, NVIDIA free, Laguna free, or kilo-auto/free. Do not spend paid model calls on routine synchronization unless the free model is unavailable or fails and architect approves rerouting.

