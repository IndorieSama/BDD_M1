namespace Back_Voting;

public class ScrutinCalculator
{
    private readonly Scrutin _scrutin;

    public ScrutinCalculator(Scrutin scrutin)
    {
        _scrutin = scrutin;
    }
    public ScrutinResult Calculate()
    {
        if (!_scrutin.EstCloture)
            throw new InvalidOperationException("Le scrutin doit être clôturé pour en calculer les résultats.");

        int total = _scrutin.TotalVotes;

        var percentages = _scrutin.Votes.ToDictionary(
            kvp => kvp.Key,
            kvp => Math.Round((double)kvp.Value / total * 100, 2)
        );

        string? winner = percentages.FirstOrDefault(kvp => kvp.Value > 50).Key;

        var result = new ScrutinResult
        {
            Winner = winner,
            Percentages = percentages
        };

        if (winner == null)
        {
            var sorted = percentages
                .Where(kvp => kvp.Key.ToLower() != "blanc")
                .OrderByDescending(kvp => kvp.Value)
                .ToList();

            var secondRound = new List<string> { sorted[0].Key, sorted[1].Key };

            if (sorted.Count > 2 && sorted[1].Value == sorted[2].Value)
            {
                secondRound.Add(sorted[2].Key);
            }

            result.SecondRoundCandidates = secondRound;
        }


        return result;
    }
}