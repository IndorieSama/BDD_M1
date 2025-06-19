namespace Back_Voting;

public class Scrutin
{
    public Dictionary<string, int> Votes { get; private set; }
    public bool EstCloture { get; private set; }

    public Scrutin(Dictionary<string, int> votes)
    {
        Votes = votes;
        EstCloture = false;
    }

    public void Cloturer()
    {
        EstCloture = true;
    }

    public int TotalVotes => Votes.Values.Sum();
}