using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ClothesWeb.Models
{
    public class ClothesDBContext:DbContext
    {
        public ClothesDBContext(DbContextOptions<ClothesDBContext> options) : base(options)
        {

        }
        public DbConnection GetDbConnection()
        {
            string sqlconnectStr = "Server=localhost\\sqlexpress;Database=ClothesDB;Integrated Security=true;TrustServerCertificate=True";
            DbConnection connection = new SqlConnection(sqlconnectStr);
            return connection;
        }
        public DbSet<AccountDB> Account { get; set; }
        public DbSet<ClothesDB> Clothes { get; set; }
    }
}
