using UserApi.Entities;

namespace UserApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddUserAsync(UserData user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }
    }
}
