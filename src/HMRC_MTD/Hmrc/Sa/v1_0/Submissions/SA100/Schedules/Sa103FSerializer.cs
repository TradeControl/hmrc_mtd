using System.Globalization;
using System.Text;
using System.Xml;

namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Submissions.SA100.Schedules;

/// <summary>
/// Serializes the SA103F full self‑employment schedule into HMRC‑compliant XML.
/// </summary>
public static class Sa103FSerializer
{
    private const string SaNamespace = "http://www.govtalk.gov.uk/taxation/SA/SA103F/2023-24";

    public static string Serialize(Sa103F sa)
    {
        using var stream = new MemoryStream();

        var settings = new XmlWriterSettings
        {
            Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false),
            Indent = false,
            NewLineHandling = NewLineHandling.None,
            OmitXmlDeclaration = false
        };

        using (var writer = XmlWriter.Create(stream, settings))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("SA103F", SaNamespace);

            // ============================
            // BUSINESS IDENTITY
            // ============================
            writer.WriteStartElement("BusinessIdentity");
            writer.WriteElementString("BusinessName", sa.BusinessName);
            writer.WriteElementString("BusinessDescription", sa.BusinessDescription);
            writer.WriteElementString("AddressLine1", sa.BusinessAddressLine1);
            writer.WriteElementString("AddressLine2", sa.BusinessAddressLine2);
            writer.WriteElementString("Postcode", sa.BusinessPostcode);
            writer.WriteEndElement();

            // ============================
            // ACCOUNTING PERIOD
            // ============================
            writer.WriteStartElement("AccountingPeriod");
            writer.WriteElementString("Start", sa.AccountingPeriodStart.ToString("yyyy-MM-dd"));
            writer.WriteElementString("End", sa.AccountingPeriodEnd.ToString("yyyy-MM-dd"));
            writer.WriteEndElement();

            // ============================
            // INCOME
            // ============================
            writer.WriteStartElement("Income");
            writer.WriteElementString("Turnover", sa.Turnover.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("OtherIncome", sa.OtherIncome.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // ============================
            // EXPENSES
            // ============================
            writer.WriteStartElement("Expenses");
            writer.WriteElementString("CostOfGoods", sa.CostOfGoods.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("ConstructionCosts", sa.ConstructionCosts.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("WagesSalaries", sa.WagesSalaries.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("CarVanExpenses", sa.CarVanExpenses.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("TravelExpenses", sa.TravelExpenses.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("PremisesRunningCosts", sa.PremisesRunningCosts.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("MaintenanceCosts", sa.MaintenanceCosts.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("AdminCosts", sa.AdminCosts.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("AdvertisingMarketing", sa.AdvertisingMarketing.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("InterestOnLoans", sa.InterestOnLoans.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("FinancialCharges", sa.FinancialCharges.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("BadDebts", sa.BadDebts.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("ProfessionalFees", sa.ProfessionalFees.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("Depreciation", sa.Depreciation.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("OtherExpenses", sa.OtherExpenses.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // ============================
            // DISALLOWABLES
            // ============================
            writer.WriteStartElement("Disallowables");
            writer.WriteElementString("CostOfGoods", sa.DisallowableCostOfGoods.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("Wages", sa.DisallowableWages.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("Motor", sa.DisallowableMotor.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("Travel", sa.DisallowableTravel.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("Premises", sa.DisallowablePremises.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("Maintenance", sa.DisallowableMaintenance.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("Admin", sa.DisallowableAdmin.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("Advertising", sa.DisallowableAdvertising.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("Interest", sa.DisallowableInterest.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("Financial", sa.DisallowableFinancial.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("BadDebts", sa.DisallowableBadDebts.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("Professional", sa.DisallowableProfessional.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("Other", sa.DisallowableOther.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // ============================
            // ADJUSTMENTS
            // ============================
            writer.WriteStartElement("Adjustments");
            writer.WriteElementString("GoodsForOwnUse", sa.GoodsForOwnUse.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("TotalDisallowables", sa.TotalDisallowables.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("AccountingProfit", sa.AccountingProfit.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("AdjustedProfit", sa.AdjustedProfit.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // ============================
            // LOSSES
            // ============================
            writer.WriteStartElement("Losses");
            writer.WriteElementString("LossBroughtForward", sa.LossBroughtForward.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("LossUsedAgainstProfit", sa.LossUsedAgainstProfit.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("LossCarriedForward", sa.LossCarriedForward.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("LossUsedAgainstOtherIncome", sa.LossUsedAgainstOtherIncome.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("LossUsedAgainstCapitalGains", sa.LossUsedAgainstCapitalGains.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // ============================
            // BASIS PERIOD
            // ============================
            writer.WriteStartElement("BasisPeriod");
            writer.WriteElementString("Start", sa.BasisPeriodStart.ToString("yyyy-MM-dd"));
            writer.WriteElementString("End", sa.BasisPeriodEnd.ToString("yyyy-MM-dd"));
            writer.WriteElementString("AdjustedProfit", sa.BasisPeriodAdjustedProfit.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("Disallowables", sa.BasisPeriodDisallowables.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // ============================
            // OVERLAP & TRANSITIONAL
            // ============================
            writer.WriteStartElement("OverlapTransitional");
            writer.WriteElementString("OverlapProfit", sa.OverlapProfit.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("OverlapReliefUsed", sa.OverlapReliefUsed.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("TransitionalProfit", sa.TransitionalProfit.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("TransitionalRelief", sa.TransitionalRelief.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("TransitionalProfitSpread", sa.TransitionalProfitSpread.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("AdjustedProfitForTax", sa.AdjustedProfitForTax.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // ============================
            // CAPITAL ALLOWANCES
            // ============================
            writer.WriteStartElement("CapitalAllowances");
            writer.WriteElementString("AnnualInvestmentAllowance", sa.AnnualInvestmentAllowance.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("WritingDownAllowanceMainPool", sa.WritingDownAllowanceMainPool.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("WritingDownAllowanceSpecialRate", sa.WritingDownAllowanceSpecialRate.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("WritingDownAllowanceSingleAsset", sa.WritingDownAllowanceSingleAsset.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("SmallPoolsAllowance", sa.SmallPoolsAllowance.ToString("F2", CultureInfo.InvariantCulture));

            writer.WriteElementString("BalancingChargeMainPool", sa.BalancingChargeMainPool.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("BalancingChargeSpecialRate", sa.BalancingChargeSpecialRate.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("BalancingChargeSingleAsset", sa.BalancingChargeSingleAsset.ToString("F2", CultureInfo.InvariantCulture));

            writer.WriteElementString("BalancingAllowanceMainPool", sa.BalancingAllowanceMainPool.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("BalancingAllowanceSpecialRate", sa.BalancingAllowanceSpecialRate.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("BalancingAllowanceSingleAsset", sa.BalancingAllowanceSingleAsset.ToString("F2", CultureInfo.InvariantCulture));

            writer.WriteElementString("PrivateUseAdjustment", sa.PrivateUseAdjustment.ToString("F2", CultureInfo.InvariantCulture));

            writer.WriteElementString("CarMainRateAllowance", sa.CarMainRateAllowance.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("CarSpecialRateAllowance", sa.CarSpecialRateAllowance.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("CarBalancingCharge", sa.CarBalancingCharge.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("CarBalancingAllowance", sa.CarBalancingAllowance.ToString("F2", CultureInfo.InvariantCulture));

            writer.WriteElementString("EnhancedCapitalAllowance", sa.EnhancedCapitalAllowance.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("SuperDeductionAllowance", sa.SuperDeductionAllowance.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("FullExpensingAllowance", sa.FullExpensingAllowance.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("SpecialRateFirstYearAllowance", sa.SpecialRateFirstYearAllowance.ToString("F2", CultureInfo.InvariantCulture));

            writer.WriteElementString("PoolOpeningValueMainPool", sa.PoolOpeningValueMainPool.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("PoolOpeningValueSpecialRate", sa.PoolOpeningValueSpecialRate.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("PoolOpeningValueSingleAsset", sa.PoolOpeningValueSingleAsset.ToString("F2", CultureInfo.InvariantCulture));

            writer.WriteElementString("PoolClosingValueMainPool", sa.PoolClosingValueMainPool.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("PoolClosingValueSpecialRate", sa.PoolClosingValueSpecialRate.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("PoolClosingValueSingleAsset", sa.PoolClosingValueSingleAsset.ToString("F2", CultureInfo.InvariantCulture));

            writer.WriteElementString("CapitalAllowancesTotal", sa.CapitalAllowancesTotal.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // ============================
            // CESSATION
            // ============================
            writer.WriteStartElement("Cessation");
            writer.WriteElementString("HasCeasedTrading", sa.HasCeasedTrading ? "true" : "false");
            writer.WriteElementString("PostCessationReceipts", sa.PostCessationReceipts.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteElementString("PostCessationExpenses", sa.PostCessationExpenses.ToString("F2", CultureInfo.InvariantCulture));
            writer.WriteEndElement();

            // ============================
            // NI TRIGGERS
            // ============================
            writer.WriteStartElement("NationalInsurance");
            writer.WriteElementString("IsExemptFromClass4", sa.IsExemptFromClass4 ? "true" : "false");
            writer.WriteEndElement();

            writer.WriteEndElement(); // SA103F
            writer.WriteEndDocument();
        }

        return Encoding.UTF8.GetString(stream.ToArray());
    }
}
