using Geen_Gezever.Entities;

namespace Geen_Gezever.Test;

public class TestMatchFinderService
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


    // <-- Prioriteit berekenen --> 
    [Theory]
    [InlineData(49, 90, new [] {1,4}, 0, 0)]
    [InlineData(50, 70, new [] {4,7,3}, 0, 0)]
    [InlineData(60, 120, new [] {1,4,7}, 0, 0)]
    [InlineData(0, 100, new [] {1,2,4,7,9}, 0, 0)]
    public void IsBetterThan_ReturnsTrue_WhenGameAIsBetterThanGameB(int min, int max, int[] gameVibesIdsParam, int playersMin, int playersMax)
    {
        //Arrange: zet klaar wat je nodig hebt
        MatchFinderService.MatchRequest matchRequest = new MatchFinderService.MatchRequest(GameTestHelper.vibes, GameTestHelper.dur, 0);
        var gameA = GameTestHelper.NewGameHelper(min, max, gameVibesIdsParam, playersMin, playersMax);
        var gameB = GameTestHelper.NewGameHelper(60, 80, new [] {1,5,7}, 0, 0);
    
        //Act: voer precies 1 ding uit
        var result = MatchFinderService.IsBetterThan(gameA, gameB, matchRequest);

        //Assert: controleer het resultaat
        Assert.True(result);
    }

    [Theory]
    [InlineData(30, 60, new [] {5,8,2}, 0, 0)]
    [InlineData(60, 120, new [] {1,6,7}, 0, 0)]
    [InlineData(60, 80, new [] {2,4,7,8}, 0, 0)]
    public void IsBetterThan_ReturnsFalse_WhenGameAIsNotBetterThanGameB(int min, int max, int[] gameVibesIdsParam, int playersMin, int playersMax)
    {
        //Arrange: zet klaar wat je nodig hebt
        MatchFinderService.MatchRequest matchRequest = new MatchFinderService.MatchRequest(GameTestHelper.vibes, GameTestHelper.dur, 0);
        var gameA = GameTestHelper.NewGameHelper(min, max, gameVibesIdsParam, playersMin, playersMax);
        var gameB = GameTestHelper.NewGameHelper(60, 80, new [] {1,5,7}, 0, 0);
    
        //Act: voer precies 1 ding uit
        var result = MatchFinderService.IsBetterThan(gameA, gameB, matchRequest);

        //Assert: controleer het resultaat
        Assert.False(result);
    }


    // <-- Sorteren van games --> 
    [Fact]
    public void SortGames_OrdersByOverlapThenDistance_WhenInWrongOrder()
    {
        //Arrange: zet klaar wat je nodig hebt
        var gameA = GameTestHelper.NewGameHelper(49, 90, new [] {1,4,8}, 0, 0);
        var gameB = GameTestHelper.NewGameHelper(50, 70, new [] {4,7,3}, 0, 0);
        var gameC = GameTestHelper.NewGameHelper(30, 60, new [] {2,1,9}, 0, 0);
        var gameD = GameTestHelper.NewGameHelper(60, 120, new [] {5,8,2}, 0, 0);
        List<Game> games = [gameD, gameC, gameA, gameB];
        MatchFinderService.MatchRequest request = new MatchFinderService.MatchRequest(GameTestHelper.vibes, GameTestHelper.dur, 0);

        //Act: voer precies 1 ding uit
        MatchFinderService.SortGames(games, request);

        //Assert: controleer het resultaat
        Assert.Same(gameB, games[0]);
        Assert.Same(gameA, games[1]);
        Assert.Same(gameC, games[2]);
        Assert.Same(gameD, games[3]);
    }

    [Fact]
    public void SortGames_StaysEmpty_WhenEmptyList()
    {
        //Arrange: zet klaar wat je nodig hebt
        List<Game> games = [];
        MatchFinderService.MatchRequest request = new MatchFinderService.MatchRequest(GameTestHelper.vibes, GameTestHelper.dur, 0);

        //Act: voer precies 1 ding uit
        MatchFinderService.SortGames(games, request);

        //Assert: controleer het resultaat
        Assert.Empty(games);
    }

    [Fact]
    public void SortGames_DoesNotChangeOrder_WhenListWithOneItem()
    {
        //Arrange: zet klaar wat je nodig hebt
        var gameA = GameTestHelper.NewGameHelper(49, 90, new [] {1,4,8}, 0, 0);
        List<Game> games = [gameA];
        MatchFinderService.MatchRequest request = new MatchFinderService.MatchRequest(GameTestHelper.vibes, GameTestHelper.dur, 0);

        //Act: voer precies 1 ding uit
        MatchFinderService.SortGames(games, request);

        //Assert: controleer het resultaat
        var game = Assert.Single(games);
        Assert.Same(gameA, game);
    }
}