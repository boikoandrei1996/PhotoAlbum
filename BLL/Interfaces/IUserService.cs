using System;
using System.Collections.Generic;
using BLL.Entities;

namespace BLL.Interfaces
{
    public interface IUserService : IService<BLLUser>
    {
        bool CheckPassword(string login, string password);
        BLLUser GetByLogin(string login);
        BLLUser GetPhotoUser(BLLPhoto photo);
        ICollection<BLLUser> GetUsersWithRole(BLLRole role);
    }
}
