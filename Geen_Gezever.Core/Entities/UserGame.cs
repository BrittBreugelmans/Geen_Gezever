namespace Geen_Gezever.Entities;

public class UserGame
{
    public Guid Id {get; init;} = Guid.CreateVersion7();
    public Guid UserId {get; set;}
    public Guid GameId {get; set;}
    public string? GuestOwnerName {get; set;}
    public string? GameLocation {get; set;}
}
