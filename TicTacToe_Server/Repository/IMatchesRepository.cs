namespace TicTacToe_Server
{
    public interface IMatchesRepository
    {
        Task<int> MakeTurn(Guid matchId, Guid playerId, int x, int y);
        Task<Guid> CreateMatch(Guid playerId);
        Task<bool> JoinMatch(Guid matchId, Guid playerId);
        Task<int> CheckMatchStatus(Guid matchId);
        Task<int> CheckTurn(Guid matchId, Guid playerId);
        Task<List<String>> GetLobbyMatchesId();
        Task<string> GetField(Guid matchId);
        Task<List<String>> GetFinishedMatchesId();
        Task<List<String>> GetMatchHistory(Guid matchId);
        Task<List<int>> GetStats(Guid playerId);
    }
}
