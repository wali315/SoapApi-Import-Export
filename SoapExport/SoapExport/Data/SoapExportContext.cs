using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoapExport.Models;

namespace SoapExport.Data
{
    public class SoapExportContext : DbContext
    {
        public SoapExportContext (DbContextOptions<SoapExportContext> options)
            : base(options)
        {
        }

        public DbSet<SoapExport.Models.CsvModels> CsvModels { get; set; } = default!;
    }
}
