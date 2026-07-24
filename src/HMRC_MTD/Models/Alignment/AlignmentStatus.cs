namespace TradeControl.Tax.UK.Models.Alignment;

public enum AlignmentStatus
{
    Unknown = 0,
    Ready = 1,
    Conflict = 2,
    Mismatch = 3,
    AlreadySubmitted = 4,
    Error = 5
}
