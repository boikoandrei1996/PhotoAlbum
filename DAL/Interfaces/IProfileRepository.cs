using System.Collections.Generic;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IProfileRepository : IRepository<DALProfile>
    {
        IEnumerable<DALProfile> GetAllByName(string firstOrLastName);
    }
}
