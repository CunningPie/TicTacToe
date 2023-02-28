using System.Collections.Generic;

namespace TicTacToe_Server
{
    public interface IUserRepository
    {
        Task<Guid> Authenticate(string username, string password);
        Task<Guid> SignUp(string username, string password);
        Task<List<string>> GetUserNames();
    }
}
