using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SoapExportConsole
{
    public class SoapToApi
    {
        private readonly string apiUrl;
        private Logger _logger;

        public SoapToApi(Logger logger, string apiUrl)
        {
            this._logger = logger;
            this.apiUrl = apiUrl;
        }

        public void GetDataFromApiAndSaveToCsv(string csvFilePath)
        {
            try
            {
                Console.WriteLine("Data reading starts.");
                _logger?.LogMessage("Data reading starts.");

                using (var httpClient = new HttpClient())
                {
                    var response = httpClient.GetStringAsync(apiUrl).Result;

                    // Assuming the SOAP response is in XML format
                    var xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(response);

                    // Select the nodes containing data (adjust the XPath accordingly)
                    XmlNodeList dataNodes = xmlDoc.SelectNodes("//CsvModel");

                    if (dataNodes != null)
                    {
                        var dataFromApi = new List<CsvModel>();

                        foreach (XmlNode dataNode in dataNodes)
                        {
                           int id = int.Parse(dataNode.SelectSingleNode("Id").InnerText);
                            string name = dataNode.SelectSingleNode("Name").InnerText;
                            int age = int.Parse(dataNode.SelectSingleNode("Age").InnerText);
                            string country = dataNode.SelectSingleNode("Country").InnerText;

                            dataFromApi.Add(new CsvModel
                            {
                                Id = id,
                                Name = name,
                                Age = age,
                                Country = country
                            });
                        }

                        // Save data to a CSV file
                        using (var writer = new StreamWriter(csvFilePath))
                        {
                            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
                            {
                                csv.WriteRecords(dataFromApi);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found in the SOAP response.");
                        _logger?.LogMessage("No data found in the SOAP response.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                _logger?.LogMessage("Error: " + ex.Message);
                // Handle and log the error as needed
            }

            Console.WriteLine("API Data Exported to CSV.");
            _logger?.LogMessage("API Data Exported to CSV.");

            // Calling Email By Method
            EmailSender emailSender = new EmailSender();
            emailSender.SendEmail("From SqlToApi", "Export Successful");
            Console.WriteLine("Data reading Ends.");
        }
    }
}