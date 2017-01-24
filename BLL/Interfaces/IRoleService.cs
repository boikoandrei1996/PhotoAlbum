using System;
using System.Collections.Generic;
using BLL.Entities;

namespace BLL.Interfaces
{
    public interface IRoleService : IService<BLLRole>
    {
        BLLRole GetByName(string name);
        BLLRole GetUserRole(BLLUser user);
    }
}
