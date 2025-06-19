namespace Back_Voting;

public class ScrutinResult
{
    public string Winner { get; set; }
    public Dictionary<string, double> Percentages { get; set; } = new();
    public List<string> SecondRoundCandidates { get; set; } = new();
    public bool IsSecondRoundNeeded => Winner == null && SecondRoundCandidates.Count == 2;
    public bool IsTieInSecondRound { get; set; } = false;
}