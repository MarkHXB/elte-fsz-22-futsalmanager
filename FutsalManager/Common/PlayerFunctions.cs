using Attribute = FutsalManager.Models.Attribute;

namespace FutsalManager.Common;

public static class PlayerFunctions
{
    public static int GetOverallRating(Player? player)
    {
        int rating = 0;
        
        if (player?.Attribute != null && player?.Position != null)
        {
            rating = player.Position.Pos.ToLower() switch
            {
                "goalkeeper" => rating = CalculateRating(player.Age, new[] { player.Attribute.Tackling, player.Attribute.Vision, player.Attribute.Reaction }),
                "defender" => rating = CalculateRating(player.Age, new[] { player.Attribute.Passing, player.Attribute.Tackling, player.Attribute.Reaction }),
                "pivot" => rating = CalculateRating(player.Age, new[] { player.Attribute.Passing, player.Attribute.Vision, player.Attribute.Positioning, player.Attribute.Dribbling }),
                "winger" => rating = CalculateRating(player.Age, new[] { player.Attribute.Passing, player.Attribute.Vision, player.Attribute.Shooting, player.Attribute.Positioning, player.Attribute.Juggling }),
                "universal" => rating = CalculateRating(player.Age, new[] { player.Attribute.Dribbling, player.Attribute.Passing, player.Attribute.Shooting, player.Attribute.Reaction, player.Attribute.Positioning, player.Attribute.Juggling }),
                _ => rating
            };
        }

        return rating;
    }

    private static int CalculateRating(int playerAge, params int[] attr)
    {
        double avg = attr.Aggregate<int, double>(0, (current, val) => current + val);

        if(playerAge > 30)
        {
            avg *= 0.85;
        }
        
        return (int)(avg / attr.Length);
    }
}