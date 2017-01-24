using System.Collections.Generic;
using BLL.Entities;

namespace BLL.Interfaces
{
    public interface IProfileService : IService<BLLProfile>
    {
        ICollection<BLLProfile> GetAllByName(string firstOrLastName);
    }
}
