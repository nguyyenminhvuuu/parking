using Firebase.Database;
using Firebase.Database.Query;
using User.Repository.Interface;
using User.Repositories.ConfigFirebase;
namespace User.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private static FirebaseClient _client;
        public Repository(ConfigFireBaseClient firebase) {
        _client = firebase.client;
        }
        public async Task AddUpdateAsync(T t, string child, string primaryKey)
        {
            await _client.Child($"{child}").Child($"{primaryKey}").PutAsync(t);
        }

        public async Task<IReadOnlyCollection<FirebaseObject<T>>> GetAllAsync(string child)
        {
            return await _client.Child($"{child}").OnceAsync<T>();
        }

        public async Task<T> GetByPrimaryKeyAsync(string primaryKey, string child)
        {
            return await _client.Child($"{child}").Child($"{primaryKey}").OnceSingleAsync<T>();
        }

    }
}
