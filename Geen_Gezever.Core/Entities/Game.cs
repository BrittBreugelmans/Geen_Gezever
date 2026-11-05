namespace Geen_Gezever.Entities;

public class Game
{
    public Guid Id {get; init;} = Guid.CreateVersion7();
    public int BggId {get; set;}
    public required string Title {get; set;}

    public string? ThumbnailUrl {get; set;}
    public string? ImageUrl {get; set;}

    //VRAAG: wat als BGG geen spelersaantal heeft of 0 returned?
    public int MinPlayers {get;set;}
    public int MaxPlayers {get; set;}

    public int MinPlayTimeMinutes {get; set;}
    public int MaxPlayTimeMinutes {get; set;}

    public string? RulesUrl {get; set;}

    public ICollection<GameVibe> GameVibes {get; init;} = new List<GameVibe>();

    // <-- Helpers --> 
    private const int StretchPlayTimeMinutes = 15;

    // <-- Vibe matching -->
    public int VibeMatchCount(int[] vibes)
    {
        int counter = 0;
        foreach(int vibe in vibes)
        {
            foreach(GameVibe gameVibe in GameVibes)
            {
                if(gameVibe.VibeId == vibe)
                {
                    counter++;
                }
            }
        }
        return counter;
    }

    // <-- Players --> 
    public bool IsPlayersAmountInRange(int players)
    {
        return MinPlayers <= players && MaxPlayers >= players;
    }

    // <-- PlayTime --> 
    public bool IsPlayTimeInRange(int duration)
    {
        return MinPlayTimeMinutes <= duration &&  duration <= MaxPlayTimeMinutes;
    }

    public bool IsPlayTimeShorterStretchInRange(int duration)
    {
        return (duration - StretchPlayTimeMinutes) <= MaxPlayTimeMinutes && MaxPlayTimeMinutes < duration;
    }

    public bool IsPlayTimeLongerStretchInRange(int duration)
    {
        return duration < MinPlayTimeMinutes && MinPlayTimeMinutes <= (duration + StretchPlayTimeMinutes);
    }
}
