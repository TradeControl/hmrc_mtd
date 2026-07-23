# hmrc_mtd — Repository Structure

July 2026  
Version: Objective 2.1

Status: Architectural Specification  
Scope: HMRC Integration Module (GitHub Submodule for TCWeb)

## 1. Overview

The `hmrc_mtd` repository provides the HMRC integration layer for Trade Control:

- Data-aware HMRC submission engine (connection string provided by TCWeb)
- Canonical payload generation (QU, EOPS, MTD Micro, VAT)
- Validation and reconciliation against Trade Control accounting views
- OAuth lifecycle management
- Fraud header construction
- HMRC transport (sandbox + production)
- Submission audit and history
- WebHarness API for development/testing
- Future HMRC Alignment Agent (scheduled reconciliation)

The module is delivered as a **GitHub submodule** and runs **inside TCWeb’s process**, using the same tenant isolation and database connection.

## 2. High-Level Architecture

``` text
TCWeb (multi-tenant host)
|
|-- passes TenantId, SubjectId, Period, TaxType
|-- passes SQL connection string
|-- passes optional XML snapshot (audit)
|
hmrc_mtd (submodule)
|
|-- loads TC accounting views (VAT, business tax, reconciliation)
|-- loads HMRC obligations + submissions
|-- compares TC vs HMRC state
|-- generates canonical payload
|-- validates + reconciles
|-- performs OAuth + fraud headers
|-- submits to HMRC
|-- logs submission + alignment status
|
```

TCWeb has **no HMRC knowledge**.  
All HMRC semantics live inside `hmrc_mtd`.

## 3. Repository Layout — Updated (July 2026)

The `hmrc_mtd` repository follows the same architectural conventions as TCExport.
It implements HMRC submission and enquiry operations through:

- a concrete payload model (`HmrcPayload`)
- a concrete result model (`HmrcResult`)
- an `OperationType` enum
- per‑operation payload builders
- per‑operation validators
- a single execution engine (`HmrcSubmissionRunner`)
- a switch‑based dispatch model inside the engine (mirroring TCExport)

The repository layout is:

``` text
hmrc_mtd/
│
├── src/
│   ├── Models/
│   │   ├── Canonical/
│   │   │   ├── QuPayload.cs
│   │   │   ├── EopsPayload.cs
│   │   │   ├── MicroPayload.cs
│   │   │   ├── VatPayload.cs
│   │   │   └── PayloadEnvelope.cs
│   │   │
│   │   ├── Hmrc/
│   │   │   ├── Obligation.cs
│   │   │   ├── Submission.cs
│   │   │   ├── Liability.cs
│   │   │   ├── Payment.cs
│   │   │   ├── FraudHeaders.cs
│   │   │   └── HmrcError.cs
│   │   │
│   │   ├── Tc/
│   │   │   ├── TcVatStatement.cs
│   │   │   ├── TcBusinessTaxView.cs
│   │   │   ├── TcReconciliation.cs
│   │   │   └── TcSubmissionHistory.cs
│   │   │
│   │   └── Alignment/
│   │       ├── AlignmentStatus.cs
│   │       └── AlignmentReport.cs
│   │
│   ├── Services/
│   │   ├── Runner/
│   │   │   ├── HmrcSubmissionRunner.cs      # Entry point (mirrors ExportRunner)
│   │   │   └── HmrcSubmissionRequest.cs
│   │   │
│   │   ├── Payload/
│   │   │   ├── QuPayloadBuilder.cs
│   │   │   ├── EopsPayloadBuilder.cs
│   │   │   ├── MicroPayloadBuilder.cs
│   │   │   └── VatPayloadBuilder.cs
│   │   │
│   │   ├── Validation/
│   │   │   ├── QuValidator.cs
│   │   │   ├── EopsValidator.cs
│   │   │   ├── MicroValidator.cs
│   │   │   ├── VatValidator.cs
│   │   │   ├── ObligationValidator.cs
│   │   │   ├── LiabilityValidator.cs
│   │   │   ├── PaymentValidator.cs
│   │   │   └── SubmissionHistoryValidator.cs
│   │   │
│   │   ├── Mapping/
│   │   │   ├── TagMapper.cs
│   │   │   └── CategoryMapper.cs
│   │   │
│   │   ├── Transport/
│   │   │   ├── HmrcClient.cs
│   │   │   ├── OAuthService.cs
│   │   │   └── FraudHeaderService.cs
│   │   │
│   │   ├── TcData/
│   │   │   ├── TcVatReader.cs
│   │   │   ├── TcBusinessTaxReader.cs
│   │   │   ├── TcReconciliationReader.cs
│   │   │   └── TcSubmissionHistoryReader.cs
│   │   │
│   │   └── Alignment/
│   │       ├── AlignmentEngine.cs
│   │       └── AlignmentScheduler.cs
│   │
│   ├── Infrastructure/
│   │   ├── Db/
│   │   │   ├── ConnectionFactory.cs
│   │   │   └── SqlHelpers.cs
│   │   ├── Logging/
│   │   │   └── SubmissionLogger.cs
│   │   └── Config/
│   │       ├── HmrcSettings.cs
│   │       └── EnvironmentSelector.cs
│   │
│   └── hmrc_mtd.csproj
│
├── tests/
│   ├── PayloadTests/
│   ├── ValidationTests/
│   ├── TransportTests/
│   ├── AlignmentTests/
│   └── WebHarnessTests/
│
└── docs/
    ├── architecture.md
    ├── payloads.md
    ├── alignment-agent.md
    └── webharness.md
```

## 4. WebHarness API

Purpose: **developer testing**, mirroring TCExport’s WebHarness.

Endpoints:

``` text
POST /harness/itsa/qu
POST /harness/itsa/eops
POST /harness/mtd/micro
POST /harness/vat
GET  /harness/status?taxType=VAT&period=2024Q2
```

Each endpoint accepts:

- connection string  
- tenant ID  
- subject ID  
- period  
- tax type  
- optional XML snapshot  

Returns:

- validation results  
- canonical payload  
- HMRC submission simulation (sandbox)  
- alignment status (TC vs HMRC)

## 5. HMRC Alignment Agent (future)

Runs as:

- Azure Function  
- WebJob  
- or background worker inside TCWeb

Responsibilities:

- read TC VAT/business views  
- read HMRC obligations + submissions  
- compare TC vs HMRC  
- write alignment status  
- notify TCWeb (dashboard, warnings)

Uses:

- `AlignmentEngine`  
- `AlignmentScheduler`  
- `TcData` readers  
- `HmrcClient`

## 6. Integration Contract (TCWeb → hmrc_mtd)

TCWeb passes:

``` json
{
"tenantId": "...",
"subjectId": "...",
"period": "2024Q2",
"taxType": "VAT",
"connectionString": "...",
"xmlSnapshot": "<TCExport>...</TCExport>"
}
```

hmrc_mtd returns:

``` json
{
"status": "ready | conflict | mismatch | already_submitted | error",
"hmrc": { ... },
"tc": { ... },
"comparison": { ... },
"submissionReference": "...",
"submittedAt": "..."
}
```

TCWeb never sees HMRC payloads.

## 7. Summary

- **Data-aware module**: Yes  
- **Connection string passed in**: Yes  
- **State integrity + reconciliation**: Yes  
- **WebHarness API**: Yes  
- **Alignment Agent**: Yes  
- **TCWeb HMRC-agnostic**: Yes  
- **GitHub submodule**: Yes  

This structure supports:

- first release (simple submission)  
- future releases (full HMRC alignment)  
- multi-tenant Azure deployment  
- clean separation of concerns  
- TCExport architectural consistency  

**End of document.**

