# Tax Hub — Implementation 2

## Objective 2: HMRC Submission Logic

July 2026  
Version: Objective 2.1  
Status: Implementation Instructions  

## 1. Purpose

This document provides instructions for how to use the supplied specifications
and the Engineering Work Plan for Objective 2 of the Tax Hub programme.

Objective 2 is fresh territory. There is no existing repository to inspect. The
HMRC submission module (`hmrc_mtd`) has a defined architecture, payload
specification, repository structure, and an Engineering Work Plan
(`tax-hub-work-plan-2.md`).

The role of this document is to define the delivery process and how the Work
Plan should be followed. No new plans or specifications are to be created.

## 2. Delivery Process

Read and interpret the following documents in order:

1. **tc-design-principles.md**  
   Architectural philosophy and constraints for all Trade Control engineering
   work.

2. **tc-development-contract.md**  
   Delivery rules, communication style, and constraints.

3. **tax-hub-spec-programme.md**  
   Programme specification defining Objective 2 and its scope.

4. **tax-hub-implementation-2.md**  
   (this document) — the implementation instructions.

5. **tax-hub-payload.md**  
   Canonical HMRC payload specification (QU, EOPS, Micro, VAT).

6. **tax-hub-hmrc-repo-structure.md**  
   Repository structure for the `hmrc_mtd` module.

7. **tax-hub-workplan-2.md**  
   Engineering Work Plan describing the phases, work packages, dependencies, and
   delivery sequencing.

Follow this sequence exactly before starting any implementation work.

## 3. Instructions

- Use `tax-hub-work-plan-2.md` as the **authoritative plan** for Objective 2.
- Do not modify the Work Plan, payload specification, programme specification,
  or repository structure.
- Implement the `hmrc_mtd` module according to:
  - the design principles,
  - the development contract,
  - the programme specification,
  - the payload specification,
  - the repo structure,
  - and the Work Plan.

Do not:

- generate new plans, specs, or architectures,
- change the defined phases or work packages,
- introduce additional objectives.

## 4. Scope of Implementation

The implementation work must:

- create the `hmrc_mtd` module as defined in `tax-hub-hmrc-repo-structure.md`,
- implement canonical payload models and builders,
- implement `HmrcSubmissionRunner` as the single entry point,
- implement the WebHarness API,
- lay the foundation for the future Alignment Agent,

all in accordance with `tax-hub-work-plan-2.md`.

## 5. Payload Model and OperationType Function Declarations

The HMRC_MTD module uses a TCExport-style function-call model. OperationType is
treated as a function declaration. Each function has a fixed parameter list,
fixed return type, and a dedicated validator.

TCWeb sends a JSON payload that encodes a function call:

```json
{
  "OperationType": "<function-name>",
  "Parameters": {
      "<parameter-name>": "<value>",
      ...
  }
}
```

The HMRC_MTD module must:

1. Identify the function from OperationType.
2. Validate the Parameters object using the correct validator.
3. Execute the function using the correct executor.
4. Return a canonical response payload.

OperationType determines the parameter list. Parameters must not be optional
unless explicitly stated. Enquiry functions do not use periodCode.

### 5.1 Submission Function Declarations

Submission functions generate canonical HMRC payloads from Trade Control
accounting data and submit them to HMRC. TCWeb always sends the HMRC period end
date as periodCode.

#### SUBMIT_VAT()

``` c
SUBMIT_VAT(
    taxSourceCode,     // e.g. "UK_MTD_VAT"
    periodEndOn,       // HMRC period end date (from Cash.vwTaxVatTotals.StartOn)
    tenantId,
    subjectId,
    connectionString,
    environment        // "sandbox" | "production"
)
```

#### SUBMIT_QU()

``` c
SUBMIT_QU(
    taxSourceCode,     // "QU"
    periodTo,          // HMRC period end date (from Cash.vwTaxHubSubmission.PeriodTo)
    tenantId,
    subjectId,
    connectionString,
    environment
)
```

#### SUBMIT_EOPS()

``` c
SUBMIT_EOPS(
    taxSourceCode,     // "EOPS"
    periodTo,
    tenantId,
    subjectId,
    connectionString,
    environment
)
```

#### SUBMIT_MICRO()

``` c
SUBMIT_MICRO(
    taxSourceCode,     // "MICRO"
    periodTo,
    tenantId,
    subjectId,
    connectionString,
    environment
)
```

### 5.2 Enquiry Function Declarations

Enquiry functions retrieve HMRC state directly from HMRC’s MTD APIs. They do not
use periodCode. Optional date ranges may be supplied if supported by HMRC.

#### GET_OBLIGATIONS()

``` c
GET_OBLIGATIONS(
    tenantId,
    subjectId,
    obligationStatus,  // "open" | "fulfilled"
    environment
)
```

#### GET_SUBMISSIONS()

``` c
GET_SUBMISSIONS(
    tenantId,
    subjectId,
    limit,             // e.g. 4
    dateFrom?,         // optional
    dateTo?,           // optional
    environment
)
```

#### GET_LIABILITIES()

``` c
GET_LIABILITIES(
    tenantId,
    subjectId,
    limit,
    dateFrom?,         // optional
    dateTo?,           // optional
    environment
)
```

#### GET_PAYMENTS()

``` c
GET_PAYMENTS(
    tenantId,
    subjectId,
    limit,
    dateFrom?,         // optional
    dateTo?,           // optional
    environment
)
```

### 5.3 Submission Return Payload

All submission functions return the same canonical structure:

``` json
{
    "status": "success | validation_error | hmrc_error",
    "canonicalPayload": { ... },
    "hmrcResponse": { ... },
    "submissionReference": "...",
    "submittedAt": "YYYY-MM-DDTHH:MM:SS",
    "warnings": [ ... ],
    "errors": [ ... ]
}
```

### 5.4 Enquiry Return Payloads

Enquiry functions return HMRC’s canonical MTD VAT enquiry payloads:

- obligations[]
- submissions[]
- liabilities[]
- payments[]

The HMRC_MTD module must not alter HMRC’s canonical structures.

### 5.5 Validator Requirements

Each OperationType must have a dedicated validator. Validators must:

- enforce required parameters
- enforce parameter types
- enforce parameter semantics
- reject unused parameters
- reject missing parameters
- reject invalid combinations (e.g., dateFrom without dateTo)
- ensure dataset availability for submission functions

Enquiry validators must be added to the Services.Validation namespace.

### 5.6 Period Semantics

Submission functions use HMRC period end dates:

- VAT → StartOn (from Cash.vwTaxVatTotals)
- QU/EOPS/Micro → PeriodTo (from Cash.vwTaxHubSubmission)

Enquiry functions do not use periodCode.

The HMRC_MTD module must not compute StartOn. It must use the dataset value
provided by TCWeb.

### 5.7 JSON Payload Implementation Rules

The coding model must implement:

- OperationType as a function declaration
- Parameters as the function argument list
- Validators bound to OperationType
- Canonical return payloads bound to OperationType
- Strict parameter enforcement
- Strict separation of submission vs enquiry semantics

This model follows the OperationModel conventions defined in the HMRC_MTD repository structure (see: tax-hub-hmrc-repo-structure.md, Section 3 — OperationModel).

## 6. Authoritative Dataset Surface

The HMRC_MTD module requires exactly two SQL views from the Trade Control
accounting database. These views provide the complete dataset surface for all
HMRC submission operations. No additional tables or views are required.

Submission functions (SUBMIT_VAT, SUBMIT_QU, SUBMIT_EOPS, SUBMIT_MICRO) must
read exclusively from these views.

Enquiry functions (GET_OBLIGATIONS, GET_SUBMISSIONS, GET_LIABILITIES,
GET_PAYMENTS) do not use SQL datasets; they query HMRC directly.

---

### 6.1 VAT Submission Dataset

VAT submissions use the `Cash.vwTaxVatTotals` view. Th HMRC period end date is the 'StartOn' column, which is the name of the composite Primary Key of Cash.tbYearPeriod ('YearNumber;StartOn')

Required columns:

```sql
SELECT YearNumber,
       Description,
       Period,
       StartOn,
       HomeSales,
       HomePurchases,
       ExportSales,
       ExportPurchases,
       HomeSalesVat,
       HomePurchasesVat,
       ExportSalesVat,
       ExportPurchasesVat,
       VatAdjustment,
       VatDue
FROM Cash.vwTaxVatTotals;
```

The `StartOn` column is the HMRC period end date and is passed to SUBMIT_VAT as `periodEndOn`.

### 6.2 Business Tax Submission Dataset (QU, EOPS, Micro)

Quarterly Update, End‑of‑Period Statement, and Micro submissions use the
`Cash.vwTaxHubSubmission` view. This view provides the HMRC‑aligned period end date (`PeriodTo`) directly.

Required columns:

``` sql
SELECT TaxSourceCode,
       TagCode,
       PeriodFrom,
       PeriodTo,          -- HMRC period end date
       TaxableAmount
FROM Cash.vwTaxHubSubmission;
```

### 6.3 Dataset Rules

- Submission functions must read only from these two views.
- HMRC_MTD must not compute HMRC period dates; it must use the values provided by TCWeb from these views.
- VAT submissions use `EndOn` from `Cash.vwTaxVatTotals`.
- QU/EOPS/Micro submissions use `PeriodTo` from `Cash.vwTaxHubSubmission`.
- Enquiry functions do not use SQL datasets.

These two views constitute the complete dataset surface required for HMRC_MTD submission operations.

**End of Document**
