using TradeControl.Tax.UK.Models.Harness;
using TradeControl.Tax.UK.Services.Mapping;
using TradeControl.Tax.UK.Services.TcData;

namespace TradeControl.Tax.UK.Services.Harness;

public sealed class QuHarnessPayloadBuilder
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
        "otherExpenses"
    ];

    private readonly TcBusinessTaxReader _reader;
    private readonly TagMapper _tagMapper;

    public QuHarnessPayloadBuilder(TcBusinessTaxReader reader, TagMapper tagMapper)
    {
        _reader = reader;
        _tagMapper = tagMapper;
    }

    public async Task<QuHarnessPayload> BuildAsync(
        string connectionString,
        string taxSourceCode,
        string subjectCode,
        DateTime periodTo,
        CancellationToken cancellationToken = default)
    {
        var rows = await _reader.ReadAsync(connectionString, taxSourceCode, periodTo, cancellationToken);
        if (rows.Count == 0)
        {
            throw new InvalidOperationException("No QU dataset rows were found for the requested period.");
        }

        var periodFrom = rows.Min(x => x.PeriodFrom);
        var items = _tagMapper.MapBusinessTaxItems(rows, Tags);

        return new QuHarnessPayload
        {
            PayloadVersion = "2026.1",
            TaxSourceCode = taxSourceCode,
            PeriodStart = periodFrom.ToString("yyyy-MM-dd"),
            PeriodEnd = periodTo.ToString("yyyy-MM-dd"),
            SubjectCode = subjectCode,
            Items = items,
            Meta = new Dictionary<string, object?>
            {
                ["operation"] = "SUBMIT_QU"
            }
        };
    }
}
