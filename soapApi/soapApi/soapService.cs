using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using soapApi.Models;

namespace soapApi
{

    public class SoapService
    {
        public List<CsvModel> ReadCsvData(string filePath)
        {
            List<CsvModel> csvData = new List<CsvModel>();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csvData = csv.GetRecords<CsvModel>().ToList();
            }

            return csvData;
        }


        public string ConvertToXml(List<CsvModel> csvData)
        {
            XDocument xmlDocument = new XDocument(new XElement("CsvModel"));

            foreach (var data in csvData)
            {
                // Create a new XML element for each data item
                var dataElement = new XElement("CsvModel",
                    new XElement("Id", data.Id),
                    new XElement("Name", data.Name),
                    new XElement("Age", data.Age),
                    new XElement("Country", data.Country)
                );

                // Add the data element to the root element
                xmlDocument.Root.Add(dataElement);
            }

            return xmlDocument.ToString();
        }
    }
}