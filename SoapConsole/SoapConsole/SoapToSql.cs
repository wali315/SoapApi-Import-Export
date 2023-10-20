using System;
using System.Net.Http;
using System.Xml;
using System.Linq;
using System.Configuration;
using SoapConsole; // Import your CSV model and CsvDbContext
using System.Threading.Tasks;

namespace SoapConsole
{
    public class SoapToSql
    {
        private string connectionString;
        private Logger logger;

        public SoapToSql(string connectionString, Logger logger)
        {
            this.connectionString = connectionString;
            this.logger = logger;
        }

        public async Task SendDataFromSoapApiToDatabaseAsync()
        {
            try
            {
                string apiUrl = "https://localhost:7132/api/Soap";

                Console.WriteLine("reading starts");
                using (var httpClient = new HttpClient())
                {
                    // Send a GET request to the SOAP API URL
                    var response = await httpClient.GetStringAsync(apiUrl);

                    // Load the SOAP response into an XML document
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(response);

                    // Select the nodes containing employee data (adjust the XPath accordingly)
                    XmlNodeList employeeNodes = xmlDoc.SelectNodes("/CsvModels/CsvModel");

                    using (var dbContext = new CsvDbContext()) // Use your CSV database context
                    {
                        foreach (XmlNode employeeNode in employeeNodes)
                        {
                            // Parse employee data from XML
                            int id = int.Parse(employeeNode.SelectSingleNode("Id").InnerText);
                            string name = employeeNode.SelectSingleNode("Name").InnerText;
                            int age = int.Parse(employeeNode.SelectSingleNode("Age").InnerText);
                            string country = employeeNode.SelectSingleNode("Country").InnerText;
                            try
                            {
                                Console.WriteLine("Checking");
                                // Create a CsvModel instance
                                var csvModel = new CsvModel
                                {
                                    Id = id,
                                    Name = name,
                                    Age = age,
                                    Country = country
                                };

                                // Check if a record with the same ID exists in the database
                                var existingRecord = dbContext.CsvModels.FirstOrDefault(x => x.Id == csvModel.Id);
                                Console.WriteLine("check");
                                if (existingRecord != null)
                                {
                                    // Update the existing record
                                    existingRecord.Name = csvModel.Name;
                                    existingRecord.Age = csvModel.Age;
                                    existingRecord.Country = csvModel.Country;
                                }
                                else
                                {
                                    // Add a new record to the database
                                    dbContext.CsvModels.Add(csvModel);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: " + ex.Message);
                                Console.WriteLine("Error: " + ex.ToString()); // Log the entire exception details for debugging
                                throw;
                            }
                            Console.WriteLine("reading ends");

                        }

                        // Save changes to the database
                        dbContext.SaveChanges();
                    }

                    Console.WriteLine("Data successfully inserted into the database");
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine("Error: " + e.ToString()); // Log the entire exception details for debugging
                throw;
            }

        }
    }
}
