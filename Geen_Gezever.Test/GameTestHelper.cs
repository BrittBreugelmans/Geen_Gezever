using Geen_Gezever.Entities;

namespace Geen_Gezever.Test;

static class GameTestHelper
{
    public const int dur = 50;
    public static readonly int[] vibes = [1,4,7];


    public static Game NewGameWithPlayTimeHelper(int min, int max)
    {
        return new Game {Title = "Catan", MinPlayTimeMinutes = min, MaxPlayTimeMinutes = max};
    }

    public static Game NewGameWithVibesHelper(int[] gameVibesIds)
    {
        ICollection<GameVibe> gameVibes = new List<GameVibe>();
        foreach (int id in gameVibesIds)
        {
            gameVibes.Add(new GameVibe {VibeId = id});
        }
        return new Game {Title = "Catan", GameVibes = gameVibes};
    }

    public static Game NewGameHelper(int min, int max,int[] gameVibesIds, int playersMin, int playersMax)
    {
        ICollection<GameVibe> gameVibes = new List<GameVibe>();
        foreach (int id in gameVibesIds)
        {
            gameVibes.Add(new GameVibe {VibeId = id});
        }
        return new Game {Title = "Catan", GameVibes = gameVibes, MinPlayTimeMinutes = min, MaxPlayTimeMinutes = max, MinPlayers = playersMin, MaxPlayers = playersMax};
    }
}