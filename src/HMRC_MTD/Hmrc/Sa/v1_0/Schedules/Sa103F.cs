namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Schedules;

/// <summary>
/// Represents the full self‑employment schedule (SA103F), including turnover,
/// expenses, capital allowances, losses, and Class 4 NIC information.
/// </summary>
public class Sa103F
{
    // Business identity
    public string BusinessName { get; set; } = string.Empty;
    public string BusinessDescription { get; set; } = string.Empty;
    public string BusinessAddressLine1 { get; set; } = string.Empty;
    public string BusinessAddressLine2 { get; set; } = string.Empty;
    public string BusinessPostcode { get; set; } = string.Empty;

    // Accounting period
    public DateTime AccountingPeriodStart { get; set; }
    public DateTime AccountingPeriodEnd { get; set; }

    // ============================
    // INCOME
    // ============================
    public decimal Turnover { get; set; }
    public decimal OtherIncome { get; set; }

    // ============================
    // EXPENSES (QU/EOPS headings)
    // ============================
    public decimal CostOfGoods { get; set; }
    public decimal ConstructionCosts { get; set; }
    public decimal WagesSalaries { get; set; }
    public decimal CarVanExpenses { get; set; }
    public decimal TravelExpenses { get; set; }
    public decimal PremisesRunningCosts { get; set; }
    public decimal MaintenanceCosts { get; set; }
    public decimal AdminCosts { get; set; }
    public decimal AdvertisingMarketing { get; set; }
    public decimal InterestOnLoans { get; set; }
    public decimal FinancialCharges { get; set; }
    public decimal BadDebts { get; set; }
    public decimal ProfessionalFees { get; set; }
    public decimal Depreciation { get; set; }
    public decimal OtherExpenses { get; set; }

    // ============================
    // DISALLOWABLES (vendor-canonical)
    // ============================
    public decimal DisallowableCostOfGoods { get; set; }
    public decimal DisallowableWages { get; set; }
    public decimal DisallowableMotor { get; set; }
    public decimal DisallowableTravel { get; set; }
    public decimal DisallowablePremises { get; set; }
    public decimal DisallowableMaintenance { get; set; }
    public decimal DisallowableAdmin { get; set; }
    public decimal DisallowableAdvertising { get; set; }
    public decimal DisallowableInterest { get; set; }
    public decimal DisallowableFinancial { get; set; }
    public decimal DisallowableBadDebts { get; set; }
    public decimal DisallowableProfessional { get; set; }
    public decimal DisallowableOther { get; set; }

    // ============================
    // ADJUSTMENTS
    // ============================
    public decimal GoodsForOwnUse { get; set; }
    public decimal TotalDisallowables { get; set; }
    public decimal AccountingProfit { get; set; }
    public decimal AdjustedProfit { get; set; }

    // ============================
    // LOSSES
    // ============================
    public decimal LossBroughtForward { get; set; }
    public decimal LossUsedAgainstProfit { get; set; }
    public decimal LossCarriedForward { get; set; }
    public decimal LossUsedAgainstOtherIncome { get; set; }
    public decimal LossUsedAgainstCapitalGains { get; set; }

    // ============================
    // BASIS PERIOD
    // ============================
    public DateTime BasisPeriodStart { get; set; }
    public DateTime BasisPeriodEnd { get; set; }
    public decimal BasisPeriodAdjustedProfit { get; set; }
    public decimal BasisPeriodDisallowables { get; set; }

    // ============================
    // OVERLAP & TRANSITIONAL
    // ============================
    public decimal OverlapProfit { get; set; }
    public decimal OverlapReliefUsed { get; set; }
    public decimal TransitionalProfit { get; set; }
    public decimal TransitionalRelief { get; set; }
    public decimal TransitionalProfitSpread { get; set; }
    public decimal AdjustedProfitForTax { get; set; }

    // ============================
    // CAPITAL ALLOWANCES
    // ============================
    public decimal AnnualInvestmentAllowance { get; set; }
    public decimal WritingDownAllowanceMainPool { get; set; }
    public decimal WritingDownAllowanceSpecialRate { get; set; }
    public decimal WritingDownAllowanceSingleAsset { get; set; }
    public decimal SmallPoolsAllowance { get; set; }

    public decimal BalancingChargeMainPool { get; set; }
    public decimal BalancingChargeSpecialRate { get; set; }
    public decimal BalancingChargeSingleAsset { get; set; }

    public decimal BalancingAllowanceMainPool { get; set; }
    public decimal BalancingAllowanceSpecialRate { get; set; }
    public decimal BalancingAllowanceSingleAsset { get; set; }

    public decimal PrivateUseAdjustment { get; set; }

    public decimal CarMainRateAllowance { get; set; }
    public decimal CarSpecialRateAllowance { get; set; }
    public decimal CarBalancingCharge { get; set; }
    public decimal CarBalancingAllowance { get; set; }

    public decimal EnhancedCapitalAllowance { get; set; }
    public decimal SuperDeductionAllowance { get; set; }
    public decimal FullExpensingAllowance { get; set; }
    public decimal SpecialRateFirstYearAllowance { get; set; }

    public decimal PoolOpeningValueMainPool { get; set; }
    public decimal PoolOpeningValueSpecialRate { get; set; }
    public decimal PoolOpeningValueSingleAsset { get; set; }

    public decimal PoolClosingValueMainPool { get; set; }
    public decimal PoolClosingValueSpecialRate { get; set; }
    public decimal PoolClosingValueSingleAsset { get; set; }

    public decimal CapitalAllowancesTotal { get; set; }

    // ============================
    // CESSATION
    // ============================
    public bool HasCeasedTrading { get; set; }
    public decimal PostCessationReceipts { get; set; }
    public decimal PostCessationExpenses { get; set; }

    // ============================
    // NI TRIGGERS
    // ============================
    public bool IsExemptFromClass4 { get; set; }

    // Convenience method
    public string ToXml() => Sa103FSerializer.Serialize(this);
}
