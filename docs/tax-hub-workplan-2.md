# Tax Hub — Work Plan 2

## Objective 2: HMRC Submission Logic

Version: Objective 2.1  
Status: Engineering Work Plan

This Work Plan defines the phased delivery framework for Objective 2 of the Tax
Hub programme. It covers the implementation of the HMRC submission logic using
the canonical payload model, validators, dataset surface, and submission runner.

All phases must be completed in sequence.  
All behaviour must follow the specifications in:

- tax-hub-spec-programme.md
- tax-hub-implementation-2.md
- tax-hub-hmrc-repo-structure.md
- tax-hub-payloads.md

## Phase 1 — Canonical Models

Implement the canonical models exactly as defined in the specification:

- QuPayload  
- EopsPayload  
- MicroPayload  
- VatPayload  
- PayloadEnvelope  

These models form the foundation for all submission operations.

**Deliverables:**

- Canonical model classes
- JSON shape matching Section 5 of implementation‑2

## Phase 2 — Dataset Surface

Implement the dataset readers for the two SQL views:

### VAT

- Read from `Cash.vwTaxVatTotals`
- Use `StartOn` and `EndOn` (HMRC period end date)

### QU / EOPS / Micro

- Read from `Cash.vwTaxHubSubmission`
- Use `PeriodTo` (HMRC period end date)

**Deliverables:**

- TcVatReader  
- TcBusinessTaxReader  

Dataset readers must return the exact fields required by the canonical payload builders.

## Phase 3 — Payload Builders

Implement the four payload builders:

- QuPayloadBuilder  
- EopsPayloadBuilder  
- MicroPayloadBuilder  
- VatPayloadBuilder  

Each builder:

- Reads from the dataset readers  
- Constructs the canonical payload  
- Applies the parameter rules defined in the specification  
- Produces a PayloadEnvelope  

**Deliverables:**

- Four builder classes  
- Full parameter enforcement  
- Correct period semantics

## Phase 4 — Validators

Implement the per‑operation validators:

- QuValidator  
- EopsValidator  
- MicroValidator  
- VatValidator  

Validators enforce:

- Required parameters  
- Period rules  
- Numeric constraints  
- Structural correctness  

**Deliverables:**

- Four validator classes  
- Validation integrated into the submission runner

## Phase 5 — Mapping Utilities

Implement the mapping utilities:

- TagMapper  
- CategoryMapper  

These map TCWeb tags and categories to canonical payload fields.

**Deliverables:**

- Mapping utilities  
- Unit tests for mapping behaviour

## Phase 6 — Submission Runner

Implement the submission runner:

- HmrcSubmissionRunner  
- HmrcSubmissionRequest  

The runner must:

- Accept an OperationType  
- Dispatch to the correct payload builder  
- Run the validator  
- Produce the final canonical payload envelope  

This phase completes the submission logic for Objective 2.

**Deliverables:**

- Submission runner  
- Dispatch logic  
- Integrated validation  
- Canonical envelope output

## Phase 7 — WebHarness (Internal Test Harness)

Implement the internal test harness controllers:

- QuTestController  
- EopsTestController  
- MicroTestController  
- VatTestController  

These controllers:

- Accept test requests  
- Call the submission runner  
- Return the canonical payload envelope  

**Deliverables:**

- Four test controllers  
- End‑to‑end test path for all submission operations

## Completion Criteria

Objective 2 is complete when:

- All canonical models are implemented  
- Dataset readers return correct period values  
- Payload builders produce correct envelopes  
- Validators enforce all rules  
- Mapping utilities are functional  
- Submission runner dispatches correctly  
- WebHarness controllers return canonical payloads  

This completes the HMRC submission logic for Objective 2.

**End of Document**