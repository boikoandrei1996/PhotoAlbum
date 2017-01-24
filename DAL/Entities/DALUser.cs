
namespace DAL.Entities
{
    public class DALUser : DALEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public DALProfile Profile { get; set; }
    }
}
