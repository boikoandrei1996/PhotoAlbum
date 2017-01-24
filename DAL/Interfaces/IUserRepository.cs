using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUserRepository : IRepository<DALUser>
    {
        DALUser GetByLogin(string login);
    }
}
