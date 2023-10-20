using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoapConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            MyConfiguration config = new MyConfiguration();
            string connectionString = config.GetConnectionString();
            string apiUrl = config.GetApiUrl();
            string LogPath = config.GetLogPath();
            Logger logger = new Logger(LogPath);

            SoapToSql apiToSql = new SoapToSql(connectionString, logger);
            await apiToSql.SendDataFromSoapApiToDatabaseAsync();
            }
    }
}
