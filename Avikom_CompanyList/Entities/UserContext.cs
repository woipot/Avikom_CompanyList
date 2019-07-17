using System.Data.Entity;

namespace Avikom_CompanyList.Entities
{
    public class UserContext : DbContext
    {
        public UserContext()
            : base("DbConnection")
        { }

        public DbSet<UserContext> Users { get; set; }
    }
}
