namespace TicTacToe_Server
{
    public class UsersRepository : IUserRepository
    {
        private List<User> _users = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(), Username = "peter", Password = "peter123"
                },
                new User
                {
                    Id = Guid.NewGuid(), Username = "joydip", Password = "joydip123"
                },
                new User
                {
                    Id = Guid.NewGuid(), Username = "james", Password = "james123"
                }
            };

        public async Task<Guid> Authenticate(string username, string password)
        {
            var user = await Task.FromResult(_users.SingleOrDefault(x => x.Username == username && x.Password == password));
            if (user != null)
            {
                return user.Id;
            }
            return Guid.Empty;
        }

        public async Task<Guid> SignUp(string username, string password)
        {
            var user = await Task.FromResult(_users.SingleOrDefault(x => x.Username == username));

            if (user == null)
            {
                var newId = Guid.NewGuid();
                _users.Add(new User { Id = newId, Username = username, Password = password});
                return newId;
            }
            return Guid.Empty;
        }

        public async Task<List<string>> GetUserNames()
        {
            var users = new List<string>();
            foreach (var user in _users)
            {
                users.Add(user.Username);
            }
            return await Task.FromResult(users);
        }
    }
}
