using Firebase.Database;
using User.Repositories.ConfigFirebase;
using User.Repository.Interface;
namespace User.Repository.Implements
{
    public class UserRepository : Repository<DTO.User>, IUserRepository
    {
        private static FirebaseClient _client;
        public UserRepository(ConfigFireBaseClient firebase) : base(firebase)
        {
            _client = firebase.client;
        }
    }
}
