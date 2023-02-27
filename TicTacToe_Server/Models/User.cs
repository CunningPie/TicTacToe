namespace TicTacToe_Server
{
    public class User
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int Wins { get; set; } = 0;

        public int Loses { get; set; } = 0;
        public int Draws { get; set; } = 0;

    }
}
