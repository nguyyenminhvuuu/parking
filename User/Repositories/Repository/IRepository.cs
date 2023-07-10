using Firebase.Database;

namespace User.Repository
{
    public interface IRepository<T>
    {
       
        Task<IReadOnlyCollection<FirebaseObject<T>>> GetAllAsync(string child);
        Task<T> GetByPrimaryKeyAsync(string id, string child);
        Task AddUpdateAsync(T t, string child, string primaryKey);
    }
}
