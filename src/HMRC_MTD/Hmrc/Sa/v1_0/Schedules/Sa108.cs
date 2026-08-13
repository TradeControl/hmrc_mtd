namespace TradeControl.Tax.UK.Hmrc.Sa.v1_0.Schedules;

/// <summary>
/// Represents the capital gains schedule (SA108), including all disposals,
/// reliefs, losses, foreign gains, and CGT rate‑band totals.
/// </summary>
public class Sa108
{
    // All disposals (shares, property, crypto, business assets, chattels, foreign assets)
    public List<Sa108Disposal> Disposals { get; set; } = new();

    // Losses
    public decimal LossesBroughtForward { get; set; }
    public decimal LossesUsedThisYear { get; set; }
    public decimal LossesCarriedForward { get; set; }

    // Annual exempt amount
    public decimal AnnualExemptAmount { get; set; }

    // Gains split by rate bands
    public decimal GainsAt10 { get; set; }
    public decimal GainsAt20 { get; set; }
    public decimal GainsAt18 { get; set; }
    public decimal GainsAt28 { get; set; }

    // Total gains
    public decimal TotalGains { get; set; }
    public decimal TaxableGains { get; set; }

    // Foreign gains
    public decimal ForeignGains { get; set; }
    public decimal ForeignTaxPaid { get; set; }
    public decimal ForeignDoubleTaxRelief { get; set; }

    // Remittance basis interactions (if SA109 present)
    public decimal GainsNotRemitted { get; set; }
    public decimal GainsRemitted { get; set; }

    // Convenience
    public string ToXml() => Sa108Serializer.Serialize(this);
}

public enum Sa108AssetTypeCode : byte
{
    Shares = 1,
    Crypto = 2,
    ResidentialProperty = 3,
    CommercialProperty = 4,
    BusinessAsset = 5,
    Chattel = 6,
    ForeignAsset = 7,
    Other = 99
}

/// <summary>
/// Represents a single capital-gains disposal event within SA108.
/// Includes proceeds, costs, reliefs, foreign tax, and remittance flags.
/// </summary>
public class Sa108Disposal
{
    // Asset identity
    public Sa108AssetTypeCode AssetTypeCode { get; set; }

    public string AssetType => AssetTypeCode switch
    {
        Sa108AssetTypeCode.Shares => "Shares",
        Sa108AssetTypeCode.Crypto => "Cryptoassets",
        Sa108AssetTypeCode.ResidentialProperty => "Residential Property",
        Sa108AssetTypeCode.CommercialProperty => "Commercial Property",
        Sa108AssetTypeCode.BusinessAsset => "Business Asset",
        Sa108AssetTypeCode.Chattel => "Chattel",
        Sa108AssetTypeCode.ForeignAsset => "Foreign Asset",
        Sa108AssetTypeCode.Other => "Other",
        _ => "Other"
    };

    public string Description { get; set; } = string.Empty;

    // Dates
    public DateTime AcquisitionDate { get; set; }
    public DateTime DisposalDate { get; set; }

    // Proceeds & costs
    public decimal DisposalProceeds { get; set; }
    public decimal AcquisitionCost { get; set; }
    public decimal EnhancementCosts { get; set; }
    public decimal IncidentalCosts { get; set; }

    // Gains & losses
    public decimal Gain { get; set; }
    public decimal Loss { get; set; }

    // Reliefs
    public decimal PrivateResidenceRelief { get; set; }
    public decimal LettingsRelief { get; set; }
    public decimal BusinessAssetDisposalRelief { get; set; }
    public decimal RolloverRelief { get; set; }
    public decimal HoldoverRelief { get; set; }
    public decimal InvestorRelief { get; set; }

    // Special rules
    public bool IsSection104Pool { get; set; } // shares pooling
    public decimal PoolAllowableCost { get; set; } // if pooled

    // Foreign-specific
    public decimal ForeignTaxPaid { get; set; }
    public string CountryCode { get; set; } = string.Empty;

    // Remittance basis
    public bool IsRemitted { get; set; }
}
