using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoapConsole
{
    public class CsvDbContext : DbContext
    {
        public DbSet<CsvModel> CsvModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            MyConfiguration config = new MyConfiguration();
            var connection = config.GetConnectionString();
            optionsBuilder.UseSqlServer(connection);
        }
    }
}
