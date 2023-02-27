using TicTacToe;
using TicTacToe_Server.Models;

namespace TicTacToe_Server
{
    public class Match
    {
        public Guid Id { get; init; }
        public Guid Player1Id { get; init; }
        public Guid Player2Id { get; set; }
        public MatchStatus Status { get; set; }
        public Guid TurnOwner { get; set; }
        public Game Game { get; init; }

        public Guid Winner {get; set;}

        public List<string> History { get; set; }

        public Guid GetOtherPlayerId(Guid playerId)
        {
            if (playerId == Player1Id)
            {
                return Player2Id;
            }
            else if (playerId == Player2Id)
            {
                return Player1Id;
            }

            return Guid.Empty;
        }
    }
}
