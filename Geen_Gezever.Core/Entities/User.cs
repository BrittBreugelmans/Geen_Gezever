namespace Geen_Gezever.Entities;

public class User
{
    public Guid Id {get; init;} = Guid.CreateVersion7();
    public required string Username {get; set;}
    public required string Email {get; set;}
}
