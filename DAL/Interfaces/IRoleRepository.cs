using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IRoleRepository : IRepository<DALRole>
    {
        DALRole GetByName(string name);
    }
}
