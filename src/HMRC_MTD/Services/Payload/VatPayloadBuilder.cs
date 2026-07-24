using TradeControl.Tax.UK.Models.Canonical;
using TradeControl.Tax.UK.Services.Mapping;
using TradeControl.Tax.UK.Services.TcData;

namespace TradeControl.Tax.UK.Services.Payload;

public sealed class VatPayloadBuilder
{
    private readonly TcVatReader _reader;
    private readonly CategoryMapper _categoryMapper;

    public VatPayloadBuilder(TcVatReader reader, CategoryMapper categoryMapper)
    {
        _reader = reader;
        _categoryMapper = categoryMapper;
    }

    public async Task<VatPayload> BuildAsync(
        string connectionString,
        string taxSourceCode,
        string subjectCode,
        DateTime periodEndOn,
        CancellationToken cancellationToken = default)
    {
        var row = await _reader.ReadAsync(connectionString, periodEndOn, cancellationToken);
        if (row is null)
        {
            throw new InvalidOperationException("No VAT dataset row was found for the requested period.");
        }

        //var vatDueSales = _categoryMapper.ToAmount(row.HomeSalesVat);
        //var vatDueAcquisitions = _categoryMapper.ToAmount(row.ExportSalesVat);
        //var totalVatDue = _categoryMapper.ToAmount(vatDueSales + vatDueAcquisitions);
        //var vatReclaimedCurrPeriod = _categoryMapper.ToAmount(row.HomePurchasesVat + row.ExportPurchasesVat);
        //var netVatDue = _categoryMapper.ToAmount(row.VatDue);

        var vatDueSales = _categoryMapper.ToAmount(row.HomeSalesVat);
        var vatDueAcquisitions = _categoryMapper.ToAmount(row.ExportPurchasesVat);

        var totalVatDue = _categoryMapper.ToAmount(
            row.HomeSalesVat +
            row.ExportSalesVat +
            row.VatAdjustment
        );

        var vatReclaimedCurrPeriod = _categoryMapper.ToAmount(
            row.HomePurchasesVat +
            row.ExportPurchasesVat
        );

        var netVatDue = _categoryMapper.ToAmount(row.VatDue);

        var items = new List<PayloadItem>
        {
            new() { Tag = "vatDueSales", Value = vatDueSales },
            new() { Tag = "vatDueAcquisitions", Value = vatDueAcquisitions },
            new() { Tag = "totalVatDue", Value = totalVatDue },
            new() { Tag = "vatReclaimedCurrPeriod", Value = vatReclaimedCurrPeriod },
            new() { Tag = "netVatDue", Value = netVatDue },
            new() { Tag = "totalValueSalesExVAT", Value = _categoryMapper.ToWholeNumber(row.HomeSales + row.ExportSales) },
            new() { Tag = "totalValuePurchasesExVAT", Value = _categoryMapper.ToWholeNumber(row.HomePurchases + row.ExportPurchases) },
            new() { Tag = "totalValueGoodsSuppliedExVAT", Value = _categoryMapper.ToWholeNumber(row.ExportSales) },
            new() { Tag = "totalValueGoodsReceivedExVAT", Value = _categoryMapper.ToWholeNumber(row.ExportPurchases) }
        };

        return new VatPayload
        {
            PayloadVersion = "2026.1",
            TaxSourceCode = taxSourceCode,
            PeriodStart = row.StartOn.ToString("yyyy-MM-dd"),
            PeriodEnd = row.StartOn.ToString("yyyy-MM-dd"),
            SubjectCode = subjectCode,
            Items = items,
            Meta = new Dictionary<string, object?>
            {
                ["operation"] = "SUBMIT_VAT"
            }
        };
    }
}
