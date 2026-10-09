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
}
