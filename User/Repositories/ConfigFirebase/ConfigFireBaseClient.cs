using Firebase.Database;
using User.Repository.Interface;
using static System.Net.WebRequestMethods;

namespace User.Repositories.ConfigFirebase
{
    public class ConfigFireBaseClient
    {
        public FirebaseClient client;
        public ConfigFireBaseClient()
        {
            client = new FirebaseClient("https://parking-c17f2-default-rtdb.firebaseio.com");
        }
    }
}
