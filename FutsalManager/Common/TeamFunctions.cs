namespace FutsalManager.Common;

public static class TeamFunctions
{
    public static int GetOverallRating(Team? team)
    {
        if (team == null) return 0;
        if (!team.Players.Any()) return 0;

        double r = 0;

        var count = team.Players.Count;
        
        r = PlayerFunctions.GetOverallRating(team.Players.Select(p => p).First()) / count;
        
        return (int)r;
    }
}