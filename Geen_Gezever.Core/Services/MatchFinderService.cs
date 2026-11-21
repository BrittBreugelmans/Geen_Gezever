using Geen_Gezever.Entities;

public static class MatchFinderService
{
    public record MatchRequest(int[] vibeIds, int duration, int amountPlayers);

    public static bool IsBetterThan(Game gameA, Game gameB, MatchRequest request)
    {
        var vibeCountA = gameA.VibeMatchCount(request.vibeIds);
        var vibeCountB = gameB.VibeMatchCount(request.vibeIds);

        if(vibeCountA > vibeCountB)
        {
            return true;
        } else if(vibeCountA == vibeCountB && gameA.CalculateDistanceToDuration(request.duration) < gameB.CalculateDistanceToDuration(request.duration))
        {
            return true;
        }
        return false;
    }

    public static void SortGames(List<Game> games, MatchRequest request)
    {
        for(int i = 1; i < games.Count; i++)
        {
            int j = i;
            while(j > 0 && IsBetterThan(games[j], games[j-1], request))
            {
                var temp = games[j];
                games[j] = games[j-1];
                games[j-1] = temp; 
                j--;
            }
        }
    }


}