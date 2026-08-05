using System.Text.Json;
using System.Text.Json.Serialization;

namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Liabilities;

public class SaLiabilitiesRequest
{
    [JsonIgnore]
    public string Utr { get; set; }

    public DateOnly? From { get; set; }
    public DateOnly? To { get; set; }

    public SaLiabilitiesRequest(string utr, DateOnly? from = null, DateOnly? to = null)
    {
        Utr = utr;
        From = from;
        To = to;
    }

    public string ToJson(bool indented = false)
    {
        var options = new JsonSerializerOptions { WriteIndented = indented };
        return JsonSerializer.Serialize(this, options);
    }
}
