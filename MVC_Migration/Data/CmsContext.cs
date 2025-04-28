using Microsoft.EntityFrameworkCore;
using MVC_Migration.Models;
namespace MVC_Migration.Data
{
    public class CmsContext: DbContext
    {
        public CmsContext(DbContextOptions<CmsContext> options) : base(options)
        {

        }
        public DbSet<Members> Tablemytables1121645 { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //資料表建立新的資料
        }
    }
}
