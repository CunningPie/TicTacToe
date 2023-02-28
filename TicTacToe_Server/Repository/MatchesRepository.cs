using TicTacToe_Server.Models;

namespace TicTacToe_Server
{
    public class MatchesRepository : IMatchesRepository
    {
        private List<Match> _matches = new List<Match>();

        private List<Match> GetLobbyMatches()
        {
            return _matches.Where(x => x.Status == Models.MatchStatus.Lobby).ToList();
        }

        private List<Match> GetStartedMatches()
        {
            return _matches.Where(x => x.Status == Models.MatchStatus.Started).ToList();
        }

        private List<Match> GetFinishedMatches()
        {
            return _matches.Where(x => x.Status == Models.MatchStatus.Finished).ToList();
        }

        public async Task<int> MakeTurn(Guid matchId, Guid playerId, int x, int y)
        {
            var match = await Task.FromResult(GetStartedMatches().Single(x => x.Id == matchId));

            if (match.TurnOwner == playerId)
            {
                var status = match.Game.Move(x, y, match.Player1Id == playerId ? 1 : 2);
                match.History.Add("Player: " + playerId + " x: " + x + " y: " + y + " put " + (match.Player1Id == playerId ? 'X' : 'O'));
                if (status > 0)
                {
                    match.Status = MatchStatus.Finished;
                    match.Winner = status == 1 ? playerId : Guid.Empty;
                    match.History.Add(status == 1 ? "Player " + playerId + " win" : "Draw");
                }

                match.TurnOwner = match.GetOtherPlayerId(playerId);
                return status;
            }

            return 0;
        }

        public async Task<Guid> CreateMatch(Guid playerId)
        {
            if (await Task.FromResult(GetLobbyMatches().SingleOrDefault(x => x.Status != Models.MatchStatus.Finished &&
                                                                   (x.Player1Id == playerId ||
                                                                    x.Player2Id == playerId)) == null))
            {
                var newMatchId = Guid.NewGuid();

                _matches.Add(new Match { Id = newMatchId, Player1Id = playerId, Game = new TicTacToe.Game(3, 3), TurnOwner = playerId, Status = Models.MatchStatus.Lobby, History = new List<string>()});
                return newMatchId;
            }
            return Guid.Empty;
        }

        public async Task<bool> JoinMatch(Guid matchId, Guid playerId)
        {
            var match = await Task.FromResult(GetLobbyMatches().SingleOrDefault(x => x.Id == matchId && x.Player1Id != playerId));

            lock (match)
            {
                if (match != null)
                {
                    match.Player2Id = playerId;
                    match.Status = Models.MatchStatus.Started;

                    return true;
                }
            }

            return false;
        }

        public async Task<List<String>> GetLobbyMatchesId() 
        {
            var matches = new List<String>();

            foreach (var match in GetLobbyMatches())
            {
                matches.Add(match.Id.ToString());
            }

            return await Task.FromResult(matches);
        }
        public async Task<List<String>> GetFinishedMatchesId()
        {
            var matches = new List<String>();

            foreach (var match in GetFinishedMatches())
            {
                matches.Add(match.Id.ToString());
            }

            return await Task.FromResult(matches);
        }

        public async Task<List<String>> GetMatchHistory(Guid matchId)
        {
            var match = await Task.FromResult(_matches.Single(x => x.Id == matchId));

            return match.History;
        }



        public async Task<int> CheckMatchStatus(Guid matchId)
        {
            var match = await Task.FromResult(_matches.Single(x => x.Id == matchId));

            if (match != null)
            {
                return (int)match.Status;
            }

            return -1;
        }

        public async Task<int> CheckTurn(Guid matchId, Guid playerId)
        {
            var match = await Task.FromResult(_matches.Single(x => x.Id == matchId));

            if (match == null)
            {
                return -2;
            }

            if (match.Status == MatchStatus.Started)
            {
                if (match.TurnOwner == playerId)
                {
                    return 1;
                }
                else if (match.TurnOwner != playerId)
                {
                    return 0;
                }
            }
            else
            {
                if (match.Winner == playerId)
                {
                    return 2;
                }
                else if (match.Winner == Guid.Empty)
                {
                    return 3;
                }
            }

            return -1;
        }

        public async Task<string> GetField(Guid matchId)
        {
            var field = await Task.FromResult(_matches.Single(x => x.Id == matchId));

            return field.Game.GetField();
        }

        public async Task<List<int>> GetStats(Guid playerId)
        {
            var playerMatches = await Task.FromResult(_matches.Where(x => (x.Player1Id == playerId || x.Player2Id == playerId) && x.Status == MatchStatus.Finished));
            var wins = await Task.FromResult(playerMatches.Where(x => x.Winner == playerId));
            var draws = await Task.FromResult(playerMatches.Where(x => x.Winner == Guid.Empty));
            var loses = await Task.FromResult(playerMatches.Where(x => x.Winner != playerId && x.Winner != Guid.Empty));

            var stats = new List<int>();

            stats.Add(wins.Count());
            stats.Add(draws.Count());
            stats.Add(loses.Count());
            stats.Add(stats[0] * 2 + stats[1]);

            return stats;
        }
    }
}
