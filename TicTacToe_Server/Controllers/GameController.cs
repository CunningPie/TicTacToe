using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TicTacToe_Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class GameController : ControllerBase
    {
        
        private readonly IMatchesRepository _matchesRepository;

        public GameController(IMatchesRepository matchesRepository)
        {
            _matchesRepository = matchesRepository;
        }

        [HttpPost("SendTurn")]
        public async Task<int> SendTurn(Guid matchId, Guid playerId, int x, int y)
        {
            try
            {
                return await _matchesRepository.MakeTurn(matchId, playerId, x, y);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return -1;
            }
        }

        [HttpPost("CreateMatch")]
        public async Task<Guid> CreateMatch(Guid playerId)
        {
            return await _matchesRepository.CreateMatch(playerId);
        }

        [HttpPost("JoinMatch")]
        public async Task<bool> JoinMatch(Guid matchId, Guid playerId)
        {
            try
            {
                return await _matchesRepository.JoinMatch(matchId, playerId);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return false;
            }
        }

        [HttpGet("LobbyMatches")]
        public async Task<List<string>> GetLobbyMatches()
        {
            return await _matchesRepository.GetLobbyMatchesId();
        }

        [HttpGet("FinishedMatches")]
        public async Task<List<string>> GetFinishedMatches()
        {
            return await _matchesRepository.GetFinishedMatchesId();
        }

        [HttpPost("GetMatchHistory")]
        public async Task<List<string>> GetMatchHistory(Guid matchId)
        {
            return await _matchesRepository.GetMatchHistory(matchId);
        }

        [HttpPost("MatchStatus")]
        public async Task<int> CheckMatchStatus(Guid matchId)
        {
            return await _matchesRepository.CheckMatchStatus(matchId);
        }

        [HttpPost("TurnStatus")]
        public async Task<int> CheckTurnStatus(Guid matchId, Guid playerId)
        {
            return await _matchesRepository.CheckTurn(matchId, playerId);
        }

        [HttpPost("RefreshField")]
        public async Task<string> RefreshField(Guid matchId)
        {
            return await _matchesRepository.GetField(matchId);
        }

        [HttpPost("GetStats")]
        public async Task<List<int>> GetStats(Guid playerId)
        {
            return await _matchesRepository.GetStats(playerId);
        }
    }
}
