using Back_Voting;
using FluentAssertions;

namespace BDD_M1_TP2.Steps;

[Binding]
public class ScrutinStepDefinition
{
    private Scrutin _scrutin;
    private ScrutinResult _result;

    [Given(@"un scrutin clôturé avec les votes suivants :")]
    public void GivenUnScrutinClotureAvecLesVotesSuivants(Table table)
    {
        var votes = new Dictionary<string, int>();

        foreach (var row in table.Rows)
        {
            var candidat = row["Candidat"];
            var nbVotes = int.Parse(row["Votes"]);
            votes[candidat] = nbVotes;
        }

        _scrutin = new Scrutin(votes);
        _scrutin.Cloturer();
    }

    [When(@"je calcule le résultat du scrutin")]
    public void WhenJeCalculeLeResultatDuScrutin()
    {
        var calculator = new ScrutinCalculator(_scrutin);
        _result = calculator.Calculate();
    }

    [Then(@"les résultats sont :")]
    public void ThenLesResultatsSont(Table table)
    {
        foreach (var row in table.Rows)
        {
            var candidat = row["Candidat"];
            var votesAttendus = int.Parse(row["Votes"]);
            var pourcentageAttendu = double.Parse(row["Pourcentage"].Replace("%", "").Trim());

            // Vérifie que les votes bruts sont exacts
            _scrutin.Votes[candidat].Should().Be(votesAttendus);

            // Vérifie que le pourcentage est approchant
            _result.Percentages[candidat].Should().BeApproximately(pourcentageAttendu, 0.5);
        }
    }
    
    [Then(@"un second tour est lancé entre :")]
    public void ThenUnSecondTourEstLanceEntre(Table table)
    {
        var attendus = table.Rows.Select(r => r["Candidat"]).ToList();
        _result.SecondRoundCandidates.Should().BeEquivalentTo(attendus);
    }

    [Given(@"un deuxième tour entre :")]
    public void GivenUnDeuxiemeTourEntre(Table table)
    {
        var votes = new Dictionary<string, int>();

        foreach (var row in table.Rows)
        {
            var candidat = row["Candidat"];
            var nbVotes = int.Parse(row["Votes"]);
            votes[candidat] = nbVotes;
        }

        _scrutin = new Scrutin(votes);
        _scrutin.Cloturer();
    }

    [Then(@"le vainqueur est ""(.*)""")]
    public void ThenLeVainqueurEst(string bob)
    {
        _result.Winner.Should().Be(bob);
    }
    
    [Then(@"aucun vainqueur n’est désigné")]
    public void ThenAucunVainqueurNEstDesigne()
    {
        _result.Winner.Should().BeNull();
    }


    [Then(@"aucun vainqueur ne peut être déterminé")]
    public void ThenAucunVainqueurNePeutEtreDetermine()
    {
        _result.Winner.Should().BeNull();
    }
}