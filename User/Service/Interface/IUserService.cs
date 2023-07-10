using Microsoft.AspNetCore.Mvc;
using User.Model.User;
using User.Repository.Interface;

namespace User.Service.Interface
{
    public interface IUserService
    {

        Task<List<DTO.User>> GetAll();
        Task<ResponseClient> Add(UserRequest user);
        Task<ResponseClient> Update(UserUpdate user);
        Task<ResponseClient> LoginCheck(string username, string password);
    }
}
