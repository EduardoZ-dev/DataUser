using UserApi.Entities;

namespace UserApi.Repositories
{
    public interface IUserRepository
    {
        Task<int> AddUserAsync(UserData user);

    }
}
