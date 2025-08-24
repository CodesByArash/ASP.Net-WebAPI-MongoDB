using AuthService.Entities;

namespace AuthService.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User?> GetByEmailAsync(string email);
        public Task<User?> GetByUserNameAsync(string userName);
    }
}