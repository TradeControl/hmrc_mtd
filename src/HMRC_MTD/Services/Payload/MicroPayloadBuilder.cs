using TradeControl.Tax.UK.Models.Canonical;
using TradeControl.Tax.UK.Services.Mapping;
using TradeControl.Tax.UK.Services.TcData;

namespace TradeControl.Tax.UK.Services.Payload;

public sealed class MicroPayloadBuilder
{
    private static readonly string[] Tags =
    [
        "AC12",
        "AC405",
        "AC410",
        "AC415",
        "AC420",
        "AC425",
        "AC34",
        "AC435",
        "CP28",
        "CP46"
    ];

    private readonly TcBusinessTaxReader _reader;
    private readonly TagMapper _tagMapper;

    public MicroPayloadBuilder(TcBusinessTaxReader reader, TagMapper tagMapper)
    {
        _reader = reader;
        _tagMapper = tagMapper;
    }

    public async Task<MicroPayload> BuildAsync(
        string connectionString,
        string taxSourceCode,
        string subjectCode,
        DateTime periodTo,
        CancellationToken cancellationToken = default)
    {
        var rows = await _reader.ReadAsync(connectionString, taxSourceCode, periodTo, cancellationToken);
        if (rows.Count == 0)
        {
            throw new InvalidOperationException("No MICRO dataset rows were found for the requested period.");
        }

        var periodFrom = rows.Min(x => x.PeriodFrom);
        var items = _tagMapper.MapBusinessTaxItems(rows, Tags);

        return new MicroPayload
        {
            PayloadVersion = "2026.1",
            TaxSourceCode = taxSourceCode,
            PeriodStart = periodFrom.ToString("yyyy-MM-dd"),
            PeriodEnd = periodTo.ToString("yyyy-MM-dd"),
            SubjectCode = subjectCode,
            Items = items,
            Meta = new Dictionary<string, object?>
            {
                ["operation"] = "SUBMIT_MICRO"
            }
        };
    }
}
