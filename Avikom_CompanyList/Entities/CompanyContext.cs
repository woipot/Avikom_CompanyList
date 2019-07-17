using System.Data.Entity;
using Avikom_CompanyList.mvvm.Models;

namespace Avikom_CompanyList.Entities
{
    public class CompanyContext : DbContext
    {
        public CompanyContext()
            : base("DBConnection")
        {
        }
        public DbSet<UserModel> Users { get; set; }
        

        public DbSet<CompanyModel> Companies { get; set; }

    }
}
