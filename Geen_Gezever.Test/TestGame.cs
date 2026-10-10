using Geen_Gezever.Entities;

namespace Geen_Gezever.Test;

public class TestGame
{
    //Methodenaam_Output_Scenario
    
    /*     [Fact]
    public void IsPlayersAmountInRange_ReturnsFalse_WhenPlayersAmountIsGreaterThanMaxPlayers()
    {
        //Arrange: zet klaar wat je nodig hebt
        var game = new Game {Title = "Catan",MinPlayers = 2, MaxPlayers = 10};
    
        //Act: voer precies 1 ding uit
        var result = game.IsPlayersAmountInRange(players: 12);

        //Assert: controleer het resultaat
        Assert.False(result);
    } */

    [Theory]
    [InlineData(2)]
    [InlineData(8)]
    [InlineData(10)]
    public void IsPlayersAmountInRange_ReturnsTrue_WhenPlayersAmountInRange(int playersAmount)
    {
        //Arrange: zet klaar wat je nodig hebt
        var game = new Game {Title = "Catan",MinPlayers = 2, MaxPlayers = 10};
    
        //Act: voer precies 1 ding uit
        var result = game.IsPlayersAmountInRange(players: playersAmount);

        //Assert: controleer het resultaat
        Assert.True(result);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(11)]
    public void IsPlayersAmountInRange_ReturnsFalse_WhenPlayersAmountOutOfRange(int playersAmount)
    {
        //Arrange: zet klaar wat je nodig hebt
        var game = new Game {Title = "Catan",MinPlayers = 2, MaxPlayers = 10};
    
        //Act: voer precies 1 ding uit
        var result = game.IsPlayersAmountInRange(players: playersAmount);

        //Assert: controleer het resultaat
        Assert.False(result);
    }
}