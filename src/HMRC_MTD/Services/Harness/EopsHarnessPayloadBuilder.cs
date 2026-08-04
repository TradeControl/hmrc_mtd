using TradeControl.Tax.UK.Models.Harness;
using TradeControl.Tax.UK.Services.Mapping;
using TradeControl.Tax.UK.Services.TcData;

namespace TradeControl.Tax.UK.Services.Harness;

public sealed class EopsHarnessPayloadBuilder
{
    private static readonly string[] Tags =
    [
        "turnover",
        "otherIncome",
        "costOfGoods",
        "constructionCosts",
        "wagesSalaries",
        "carVanExpenses",
        "travelExpenses",
        "premisesRunningCosts",
        "maintenanceCosts",
        "adminCosts",
        "advertisingMarketing",
        "interestOnLoans",
        "financialCharges",
        "badDebts",
        "professionalFees",
        "depreciation",
        "otherExpenses",
        "goodsForOwnUse",
        "disallowableCostOfGoods",
        "disallowableWages",
        "disallowableMotor",
        "disallowableTravel",
        "disallowablePremises",
        "disallowableMaintenance",
        "disallowableAdmin",
        "disallowableAdvertising",
        "disallowableInterest",
        "disallowableFinancial",
        "disallowableBadDebts",
        "disallowableProfessional",
        "disallowableOther",
        "accountingProfit",
        "totalDisallowables",
        "adjustedProfit",
        "lossBroughtForward",
        "lossUsedAgainstProfit",
        "lossCarriedForward",
        "lossUsedAgainstOtherIncome",
        "lossUsedAgainstCapitalGains",
        "postCessationReceipts",
        "postCessationExpenses",
        "basisPeriodStart",
        "basisPeriodEnd",
        "basisPeriodAdjustedProfit",
        "basisPeriodDisallowables",
        "overlapProfit",
        "overlapReliefUsed",
        "transitionalProfit",
        "transitionalRelief",
        "transitionalProfitSpread",
        "adjustedProfitForTax",
        "capitalAllowancesClaimed",
        "annualInvestmentAllowance",
        "writingDownAllowanceMainPool",
        "writingDownAllowanceSpecialRate",
        "writingDownAllowanceSingleAsset",
        "smallPoolsAllowance",
        "balancingChargeMainPool",
        "balancingChargeSpecialRate",
        "balancingChargeSingleAsset",
        "balancingAllowanceMainPool",
        "balancingAllowanceSpecialRate",
        "balancingAllowanceSingleAsset",
        "privateUseAdjustment",
        "carMainRateAllowance",
        "carSpecialRateAllowance",
        "carBalancingCharge",
        "carBalancingAllowance",
        "enhancedCapitalAllowance",
        "superDeductionAllowance",
        "fullExpensingAllowance",
        "specialRateFirstYearAllowance",
        "poolOpeningValueMainPool",
        "poolOpeningValueSpecialRate",
        "poolOpeningValueSingleAsset",
        "poolClosingValueMainPool",
        "poolClosingValueSpecialRate",
        "poolClosingValueSingleAsset",
        "capitalAllowancesTotal"
    ];

    private readonly TcBusinessTaxReader _reader;
    private readonly TagMapper _tagMapper;

    public EopsHarnessPayloadBuilder(TcBusinessTaxReader reader, TagMapper tagMapper)
    {
        _reader = reader;
        _tagMapper = tagMapper;
    }

    public async Task<EopsHarnessPayload> BuildAsync(
        string connectionString,
        string taxSourceCode,
        string subjectCode,
        DateTime periodTo,
        CancellationToken cancellationToken = default)
    {
        var rows = await _reader.ReadAsync(connectionString, taxSourceCode, periodTo, cancellationToken);
        if (rows.Count == 0)
        {
            throw new InvalidOperationException("No EOPS dataset rows were found for the requested period.");
        }

        var periodFrom = rows.Min(x => x.PeriodFrom);
        var items = _tagMapper.MapBusinessTaxItems(rows, Tags)
            .Select(x =>
            {
                if (x.Tag is "basisPeriodStart")
                {
                    return new PayloadHarnessItem { Tag = x.Tag, Value = periodFrom.ToString("yyyy-MM-dd") };
                }

                if (x.Tag is "basisPeriodEnd")
                {
                    return new PayloadHarnessItem { Tag = x.Tag, Value = periodTo.ToString("yyyy-MM-dd") };
                }

                return x;
            })
            .ToList();

        return new EopsHarnessPayload
        {
            PayloadVersion = "2026.1",
            TaxSourceCode = taxSourceCode,
            PeriodStart = periodFrom.ToString("yyyy-MM-dd"),
            PeriodEnd = periodTo.ToString("yyyy-MM-dd"),
            SubjectCode = subjectCode,
            Items = items,
            Meta = new Dictionary<string, object?>
            {
                ["operation"] = "SUBMIT_EOPS"
            }
        };
    }
}
