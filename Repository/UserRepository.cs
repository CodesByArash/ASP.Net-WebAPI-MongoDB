using ASP.MongoDb.API.Settings;
using AuthService.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AuthService.Repository
{
    public class UserRepository: Repository<User>,IUserRepository
    {
        public UserRepository(IOptions<MongoDbSettings> settings) :base(settings)
        {
            CreateIndexes();
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _collection
                .Find(Builders<User>.Filter.Eq(u => u.Email, email))
                .FirstOrDefaultAsync();
        }
        public async Task<User?> GetByUserNameAsync(string userName)
        {
            return await _collection
                .Find(Builders<User>.Filter.Eq(u => u.UserName, userName))
                .FirstOrDefaultAsync();
        }
        private void CreateIndexes()
        {
            var indexKeys = Builders<User>.IndexKeys.Ascending(u => u.UserName);
            var indexOptions = new CreateIndexOptions { Unique = true };
            var model = new CreateIndexModel<User>(indexKeys, indexOptions);
            _collection.Indexes.CreateOne(model);

            var emailIndexKeys = Builders<User>.IndexKeys.Ascending(u => u.Email);
            var emailIndexOptions = new CreateIndexOptions { Unique = true };
            var emailModel = new CreateIndexModel<User>(emailIndexKeys, emailIndexOptions);
            _collection.Indexes.CreateOne(emailModel);
        }
    }
}
