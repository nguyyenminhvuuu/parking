using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using System.Text.Json.Nodes;
using User.Model.User;
using User.Repository;
using User.Repository.Interface;
using User.Service.Interface;

namespace User.Service.Implement
{
    public class UserService : IUserService
    {
        private static string Child = "User";

        private static List<DTO.User> listUser = new List<DTO.User>();
        private IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IUserRepository UserRepository => throw new NotImplementedException();
        public async Task<ResponseClient> Add(UserRequest user)
        {
            if (listUser != null)
            {
                listUser.Clear();
            }
            var dataSnapShot = await _unitOfWork.UserRepository.GetAllAsync(Child);

            #region Valid username
            foreach (var item in dataSnapShot)
            {
                listUser.Add(item.Object);
                if (item.Object.Username.Equals(user.Username))
                {
                    return new ResponseClient
                    {
                        Status = "Fail",
                        Message = "Username duplicate",
                        Data = null
                    };
                }
            }
            #endregion

            #region  Valid email
            if (user.Email.ToLower().EndsWith("@gmail.com"))
            {
                int index = user.Email.LastIndexOf("@gmail.com");
                string newEmail = user.Email.Substring(0, index + 1);
                string email2 = "";
                if (!newEmail.ToLower().Contains("@gmail.com"))
                {
                    foreach (var item in listUser)
                    {
                        email2 = item.Email.Substring(0, item.Email.ToLower().LastIndexOf("@gmail.com") + 1);
                        if (email2.Equals(newEmail))
                        {
                            return new ResponseClient
                            {
                                Status = "Fail",
                                Message = "Email duplicate",
                                Data = null
                            };
                        }
                    }
                }
                else
                {
                    return new ResponseClient
                    {
                        Status = "Fail",
                        Message = "Email Invalid",
                        Data = null
                    };
                }
            }
            else
            {
                return new ResponseClient
                {
                    Status = "Fail",
                    Message = "Email Invalid",
                    Data = null
                };
            }
            #endregion

            #region Valid phone
            BigInteger i = 0;
            if (user.Phone.Trim().Length < 8 || !BigInteger.TryParse(user.Phone.Trim(), out i))
            {
                return new ResponseClient
                {
                    Status = "Fail",
                    Message = "Phone [8-11]",
                    Data = ""
                };
            }
            #endregion

            DTO.User user1 = new DTO.User
            {
                Id = Guid.NewGuid(),
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                Phone = user.Phone,
                Name = user.Name,
                DateCreate = DateTime.Now,
                LastUpdate = DateTime.Now,
                Status = "Active",
            };
            await _unitOfWork.UserRepository.AddUpdateAsync(user1, Child, user1.Id.ToString());
            return new ResponseClient
            {
                Status = "Success",
                Message = "Created",
                Data = user1
            };
        }
        public async Task<ResponseClient> Update(UserUpdate user)
        {
            DTO.User user1 = await _unitOfWork.UserRepository.GetByPrimaryKeyAsync(user.Id.ToString(), Child);
            if (user1 != null)
            {
                user1.Name = user.Name;
                user1.Password = user.Password;
                user1.Email = user.Email;
                user1.Phone = user.Phone;
                await _unitOfWork.UserRepository.AddUpdateAsync(user1, Child, user.Id.ToString());
                return new ResponseClient
                {
                    Status = "success",
                    Message = "created",
                    Data = user
                };
            }
            else
            {
                return new ResponseClient
                {
                    Status = "fail",
                    Message = "not found user with id: " + user.Id,
                    Data = null
                };
            }
        }

        public async Task<List<DTO.User>> GetAll()
        {
            var dataSnapShot = await _unitOfWork.UserRepository.GetAllAsync(Child);
            if (listUser != null)
            {
                listUser.Clear();
            }
            foreach (var item in dataSnapShot)
            {
                listUser.Add(item.Object);
            }
            return listUser;
        }

        public async Task<ResponseClient> LoginCheck(string username, string password)
        {

            var dataSnapShot = await _unitOfWork.UserRepository.GetAllAsync(Child);
            foreach (var item in dataSnapShot)
            {
                if (item.Object.Username.Equals(username) && item.Object.Password.Equals(password))
                {
                    return new ResponseClient
                    {
                        Status = "success",
                        Message = "Ok",
                        Data = item.Object
                    };
                }
            }
            return new ResponseClient
            {
                Status = "fail",
                Message = "cuc",
                Data = "[]"
            };
        }
    }
}
