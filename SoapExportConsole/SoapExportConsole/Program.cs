using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoapExportConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new MyConfiguration();
            // Get the API URL, CSV file path, and LogPath from your configuration
            string apiUrl = config.GetApiUrl();
            string csvFilePath = config.GetCsvFilePath();
            string logPath = config.GetLogPath();

            Logger logger = new Logger(logPath);

            SoapToApi api = new SoapToApi(logger,apiUrl);
            api.GetDataFromApiAndSaveToCsv(csvFilePath);

        }
    }
}
