# Tax Hub — Payload Specification  

July 2026  
Version: Objective 2.1  

Author: TradeControl / Tax Hub  
Status: Draft for Implementation

## 1. Overview

This document defines the **payload schemas** for all four tax sources supported by the Tax Hub:

- **UK‑ITSA‑SE‑QU** — Quarterly Update (Self‑Employment)  
- **UK‑ITSA‑SE‑EOPS** — End of Period Statement (Self‑Employment)  
- **UK‑MTD** — Micro‑entity (FRS105 + CT600‑aligned)  
- **VAT** — MTD VAT Return  

Payloads are derived from:

- HMRC statutory definitions  
- SA103F/SA103S forms  
- MTD ITSA guidance  
- FRS105 micro‑entity accounting standard  
- CT600 guidance  
- HMRC VAT API schema  
- Internal tag sets and mappings defined in the SQL templates  
  (QU tags: ;  
   EOPS tags: ;  
   Micro‑entity tags: AC12/AC405/etc. )

All payloads follow the Tax Hub transport conventions:

- `payloadVersion`  
- `taxSourceCode`  
- `periodStart` / `periodEnd`  
- `subjectCode`  
- `items[]` — list of tagged values  
- `meta` — optional metadata

## 2. HMRC Source References

### 2.1 UK‑ITSA‑SE‑QU

Quarterly Update statutory definitions:

- [MTD ITSA Quarterly Update guidance](https://www.gov.uk/guidance/using-making-tax-digital-for-income-tax)
- [SA103S (short return) - Quarterly Update categories](https://www.gov.uk/guidance/income-tax-quarterly-updates-for-making-tax-digital)
- [SA103F (full self‑employment return) — underlying statutory definitions](https://www.gov.uk/government/publications/self-assessment-tax-return-sa103f)

### 2.2 UK‑ITSA‑SE‑EOPS

Annual business return statutory definitions:

- [SA103F (full self‑employment return)](https://www.gov.uk/government/publications/self-assessment-tax-return-sa103f)
- [SA103S (short self‑employment return)](https://www.gov.uk/government/publications/self-assessment-tax-return-sa103s)
- [EOPS guidance](https://www.gov.uk/guidance/end-of-period-statements-for-making-tax-digital)
- Basis period reform rules  
- Capital allowances rules  
- Losses rules

### 2.3 UK‑MTD (Micro‑entity)

Micro‑entity statutory definitions:

- [FRS 105 (micro‑entity accounting standard)](https://www.frc.org.uk/accountants/accounting-and-reporting-policy/frs-105)
- [Companies House micro‑entity accounts guidance](https://www.gov.uk/prepare-file-abridged-or-micro-entity-accounts)
- [CT600 guidance (for tax adjustments)](https://www.gov.uk/government/publications/self-assessment-company-tax-return-ct600)

### 2.4 VAT

HMRC VAT API (official JSON schema):

- [MTD VAT API documentation](https://developer.service.hmrc.gov.uk/api-documentation)
- [VAT obligations API](https://developer.service.hmrc.gov.uk/api-documentation/docs/api/service/vat-api/1.0)
- [VAT returns API](https://developer.service.hmrc.gov.uk/api-documentation/docs/api/service/vat-api/1.0#vat-returns)

## 3. Transport Envelope (all payloads)

``` json
{
    "payloadVersion": "2026.1",
    "taxSourceCode": "UK-ITSA-SE-QU",
    "periodStart": "2026-04-06",
    "periodEnd": "2026-07-05",
    "subjectCode": "SUB123",
    "items": [
    { "tag": "turnover", "value": 12345 }
    ],
    "meta": {
    "submittedAt": "2026-07-21T10:00:00Z"
    }
}
```

## 4. UK‑ITSA‑SE‑QU Payload

Quarterly Update field set (tags defined in SQL seed: )

### 4.1 Tag List

All tags are numeric except where noted.

- turnover  
- otherIncome  
- costOfGoods  
- constructionCosts  
- wagesSalaries  
- carVanExpenses  
- travelExpenses  
- premisesRunningCosts  
- maintenanceCosts  
- adminCosts  
- advertisingMarketing  
- interestOnLoans  
- financialCharges  
- badDebts  
- professionalFees  
- depreciation  
- otherExpenses  

### 4.2 JSON Schema

``` json
{
    "payloadVersion": "2026.1",
    "taxSourceCode": "UK-ITSA-SE-QU",
    "periodStart": "...",
    "periodEnd": "...",
    "subjectCode": "...",
    "items": [
    { "tag": "turnover", "value": 0 },
    { "tag": "otherIncome", "value": 0 },
    { "tag": "costOfGoods", "value": 0 },
    { "tag": "constructionCosts", "value": 0 },
    { "tag": "wagesSalaries", "value": 0 },
    { "tag": "carVanExpenses", "value": 0 },
    { "tag": "travelExpenses", "value": 0 },
    { "tag": "premisesRunningCosts", "value": 0 },
    { "tag": "maintenanceCosts", "value": 0 },
    { "tag": "adminCosts", "value": 0 },
    { "tag": "advertisingMarketing", "value": 0 },
    { "tag": "interestOnLoans", "value": 0 },
    { "tag": "financialCharges", "value": 0 },
    { "tag": "badDebts", "value": 0 },
    { "tag": "professionalFees", "value": 0 },
    { "tag": "depreciation", "value": 0 },
    { "tag": "otherExpenses", "value": 0 }
    ]
}
```

## 5. UK‑ITSA‑SE‑EOPS Payload

Annual business return field set (tags defined in SQL seed: )

### 5.1 Tag Groups

EOPS includes all QU tags plus:

#### Adjustments

- goodsForOwnUse  
- disallowableCostOfGoods  
- disallowableWages  
- disallowableMotor  
- disallowableTravel  
- disallowablePremises  
- disallowableMaintenance  
- disallowableAdmin  
- disallowableAdvertising  
- disallowableInterest  
- disallowableFinancial  
- disallowableBadDebts  
- disallowableProfessional  
- disallowableOther  

#### Derived totals

- accountingProfit  
- totalDisallowables  
- adjustedProfit  

#### Losses

- lossBroughtForward  
- lossUsedAgainstProfit  
- lossCarriedForward  
- lossUsedAgainstOtherIncome  
- lossUsedAgainstCapitalGains  
- postCessationReceipts  
- postCessationExpenses  

#### Basis period

- basisPeriodStart  
- basisPeriodEnd  
- basisPeriodAdjustedProfit  
- basisPeriodDisallowables  
- overlapProfit  
- overlapReliefUsed  
- transitionalProfit  
- transitionalRelief  
- transitionalProfitSpread  
- adjustedProfitForTax  

#### Capital allowances

- capitalAllowancesClaimed  
- annualInvestmentAllowance  
- writingDownAllowanceMainPool  
- writingDownAllowanceSpecialRate  
- writingDownAllowanceSingleAsset  
- smallPoolsAllowance  
- balancingChargeMainPool  
- balancingChargeSpecialRate  
- balancingChargeSingleAsset  
- balancingAllowanceMainPool  
- balancingAllowanceSpecialRate  
- balancingAllowanceSingleAsset  
- privateUseAdjustment  
- carMainRateAllowance  
- carSpecialRateAllowance  
- carBalancingCharge  
- carBalancingAllowance  
- enhancedCapitalAllowance  
- superDeductionAllowance  
- fullExpensingAllowance  
- specialRateFirstYearAllowance  
- poolOpeningValueMainPool  
- poolOpeningValueSpecialRate  
- poolOpeningValueSingleAsset  
- poolClosingValueMainPool  
- poolClosingValueSpecialRate  
- poolClosingValueSingleAsset  
- capitalAllowancesTotal  

### 5.2 JSON Schema

``` json
{
    "payloadVersion": "2026.1",
    "taxSourceCode": "UK-ITSA-SE-EOPS",
    "periodStart": "...",
    "periodEnd": "...",
    "subjectCode": "...",
    "items": [
    { "tag": "turnover", "value": 0 },
    { "tag": "otherIncome", "value": 0 },
    { "tag": "costOfGoods", "value": 0 },
    { "tag": "goodsForOwnUse", "value": 0 },
    { "tag": "disallowableCostOfGoods", "value": 0 },
    { "tag": "accountingProfit", "value": 0 },
    { "tag": "adjustedProfit", "value": 0 },
    { "tag": "lossBroughtForward", "value": 0 },
    { "tag": "basisPeriodStart", "value": "2026-04-06" },
    { "tag": "capitalAllowancesClaimed", "value": 0 },
    { "tag": "capitalAllowancesTotal", "value": 0 }
    ]
}
```

## 6. UK‑MTD Micro‑Entity Payload

Tags defined in SQL template: AC12, AC405, AC410, AC415, AC420, AC425, AC34, AC435, CP28, CP46  
(see: `App.proc_Template_CO_MICRO_CUR_2026`)

### 6.1 Tag List

- AC12 — Turnover  
- AC405 — Other Income  
- AC410 — Cost of Sales  
- AC415 — Staff Costs  
- AC420 — Depreciation Total  
- AC425 — Other Charges  
- AC34 — Tax on Profit  
- AC435 — Profit and Loss  
- CP28 — Depreciation charge  
- CP46 — Depreciation adjustment  

### 6.2 JSON Schema

``` json
{
    "payloadVersion": "2026.1",
    "taxSourceCode": "UK-MTD",
    "periodStart": "...",
    "periodEnd": "...",
    "subjectCode": "...",
    "items": [
    { "tag": "AC12", "value": 0 },
    { "tag": "AC405", "value": 0 },
    { "tag": "AC410", "value": 0 },
    { "tag": "AC415", "value": 0 },
    { "tag": "AC420", "value": 0 },
    { "tag": "AC425", "value": 0 },
    { "tag": "AC34", "value": 0 },
    { "tag": "AC435", "value": 0 },
    { "tag": "CP28", "value": 0 },
    { "tag": "CP46", "value": 0 }
    ]
}
```

## 7. VAT Payload

Fields defined by HMRC VAT API.

### 7.1 Tag List

- vatDueSales  
- vatDueAcquisitions  
- totalVatDue  
- vatReclaimedCurrPeriod  
- netVatDue  
- totalValueSalesExVAT  
- totalValuePurchasesExVAT  
- totalValueGoodsSuppliedExVAT  
- totalValueGoodsReceivedExVAT  

### 7.2 JSON Schema

``` json
{
    "payloadVersion": "2026.1",
    "taxSourceCode": "VAT",
    "periodStart": "...",
    "periodEnd": "...",
    "subjectCode": "...",
    "items": [
    { "tag": "vatDueSales", "value": 0 },
    { "tag": "vatDueAcquisitions", "value": 0 },
    { "tag": "totalVatDue", "value": 0 },
    { "tag": "vatReclaimedCurrPeriod", "value": 0 },
    { "tag": "netVatDue", "value": 0 },
    { "tag": "totalValueSalesExVAT", "value": 0 },
    { "tag": "totalValuePurchasesExVAT", "value": 0 },
    { "tag": "totalValueGoodsSuppliedExVAT", "value": 0 },
    { "tag": "totalValueGoodsReceivedExVAT", "value": 0 }
    ]
}
```

## 8. Validation Rules (all payloads)

- All numeric fields must be non‑negative.  
- Dates must be ISO‑8601.  
- Tag codes must match the tax source.  
- Items must not contain duplicates.  
- Derived totals (EOPS) must be consistent with HMRC rules.  
- VAT fields must satisfy HMRC VAT API constraints.

## 9. Implementation Notes

- QU and EOPS tags are created in the Sole Trader template (see: `App.proc_Template_ST_SOLE_CUR_MIN_2026`).  
- Micro‑entity tags are created in the MICRO template (see: `App.proc_Template_CO_MICRO_CUR_2026`).  
- Category mappings for QU/EOPS are defined in section 80 (see: ).  
- VAT is handled separately via HMRC API.

## 10. Appendix — Tag Classes

TagClassCode meanings (from SQL seeds):

- 0 - Rollup
- 1 - Component
- 2 - Derived

**End of document.**
