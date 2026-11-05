using System.Data.Common;
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


    // <-- Spelersaantal -->
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


    // <-- Helper --> 
    private const int dur = 50;
    private Game NewGameWithPlayTimeHelper(int min, int max)
    {
        return new Game {Title = "Catan", MinPlayTimeMinutes = min, MaxPlayTimeMinutes = max};
    }

    static readonly int[] vibes = [1,4,7];
    private Game NewGameWithVibesHelper(int[] gameVibesIds)
    {
        ICollection<GameVibe> gameVibes = new List<GameVibe>();
        foreach (int id in gameVibesIds)
        {
            gameVibes.Add(new GameVibe {VibeId = id});
        }
        return new Game {Title = "Catan", GameVibes = gameVibes};
    }


    // <-- Exacte match op speelduur --> 
    [Theory]
    [InlineData(45, 90)]
    [InlineData(50, 60)]
    [InlineData(20, 50)]
    public void IsPlayTimeInRange_ReturnsTrue_WhenPlayTimeInRange(int min, int max)
    {
        //Arrange: zet klaar wat je nodig hebt
        var game = NewGameWithPlayTimeHelper(min, max);
    
        //Act: voer precies 1 ding uit
        var result = game.IsPlayTimeInRange(duration: dur);

        //Assert: controleer het resultaat
        Assert.True(result);
    }

    [Theory]
    [InlineData(35, 49)]
    [InlineData(51, 70)]
    public void IsPlayTimeInRange_ReturnsFalse_WhenPlayTimeOUtOfRange(int min, int max)
    {
        //Arrange: zet klaar wat je nodig hebt
        var game = NewGameWithPlayTimeHelper(min, max);
    
        //Act: voer precies 1 ding uit
        var result = game.IsPlayTimeInRange(duration: dur);

        //Assert: controleer het resultaat
        Assert.False(result);
    }


    // <-- Korter binnen de stretch van speelduur --> 
    [Theory]
    [InlineData(25, 45)]
    [InlineData(15, 49)]
    [InlineData(15, 35)]
    public void IsPlayTimeShorterStretchInRange_ReturnsTrue_WhenPlayTimeInRange(int min, int max)
    {
        //Arrange: zet klaar wat je nodig hebt
        var game = NewGameWithPlayTimeHelper(min, max);
    
        //Act: voer precies 1 ding uit
        var result = game.IsPlayTimeShorterStretchInRange(duration: dur);

        //Assert: controleer het resultaat
        Assert.True(result);
    }

    [Theory]
    [InlineData(50, 60)]
    [InlineData(45, 90)]
    [InlineData(15, 30)]
    [InlineData(35, 55)]
    [InlineData(25, 50)]
    [InlineData(5, 34)]
    public void IsPlayTimeShorterStretchInRange_ReturnsFalse_WhenPlayTimeOutOfRange(int min, int max)
    {
        //Arrange: zet klaar wat je nodig hebt
        var game = NewGameWithPlayTimeHelper(min, max);
    
        //Act: voer precies 1 ding uit
        var result = game.IsPlayTimeShorterStretchInRange(duration: dur);

        //Assert: controleer het resultaat
        Assert.False(result);
    }


    // <-- Langer binnen de stretch van speelduur --> 
    [Theory]
    [InlineData(55, 80)]
    [InlineData(51, 75)]
    [InlineData(65, 90)]
    [InlineData(64, 85)]
    public void IsPlayTimeLongerStretchInRange_ReturnsTrue_WhenPlayTimeInRange(int min, int max)
    {
        //Arrange: zet klaar wat je nodig hebt
        var game = NewGameWithPlayTimeHelper(min, max);
    
        //Act: voer precies 1 ding uit
        var result = game.IsPlayTimeLongerStretchInRange(duration: dur);

        //Assert: controleer het resultaat
        Assert.True(result);
    }

    [Theory]
    [InlineData(49, 60)]
    [InlineData(50, 55)]
    [InlineData(50, 75)]
    [InlineData(45, 90)]
    [InlineData(66, 90)]
    public void IsPlayTimeLongerStretchInRange_ReturnsFalse_WhenPlayTimeOutOfRange(int min, int max)
    {
        //Arrange: zet klaar wat je nodig hebt
        var game = NewGameWithPlayTimeHelper(min, max);
    
        //Act: voer precies 1 ding uit
        var result = game.IsPlayTimeLongerStretchInRange(duration: dur);

        //Assert: controleer het resultaat
        Assert.False(result);
    }

    [Theory]
    [InlineData(51, 49)]
    public void IsPlayTimeShorterAndLonger_ReturnsTrue_WhenMinHigherThanMax(int min, int max)
    {
        //Arrange: zet klaar wat je nodig hebt
        var game = NewGameWithPlayTimeHelper(min, max);
    
        //Act: voer precies 1 ding uit
        var resultLong = game.IsPlayTimeLongerStretchInRange(duration: dur);
        var resultShort = game.IsPlayTimeShorterStretchInRange(duration: dur);

        //Assert: controleer het resultaat
        Assert.True(resultLong);
        Assert.True(resultShort);
    }

    
    // <-- Vibe matching --> 
    [Theory]
    [InlineData(new [] {1,4}, 2)]
    [InlineData(new [] {4,7,3}, 2)]
    [InlineData(new [] {5,8,2}, 0)]
    [InlineData(new [] {1,4,7}, 3)]
    public void VibeMatchCount_ReturnsNumberOfMatchedVibes_WhenVibesMatchGame(int[] gameVibesIdsParam, int expected_result)
    {
        //Arrange: zet klaar wat je nodig hebt vibes
        var game = NewGameWithVibesHelper(gameVibesIdsParam);
    
        //Act: voer precies 1 ding uit
        var result = game.VibeMatchCount(vibes);

        //Assert: controleer het resultaat
        Assert.Equal(expected_result, result);
    }

    [Theory]
    [InlineData(0)]
    public void VibeMatchCount_ReturnsNumberOfMatchedVibes_WhenGameHasNoVibes(int expected_result)
    {
        //Arrange: zet klaar wat je nodig hebt vibes
        var game = new Game {Title = "Catan"};
    
        //Act: voer precies 1 ding uit
        var result = game.VibeMatchCount(vibes);

        //Assert: controleer het resultaat
        Assert.Equal(expected_result, result);
    }
}