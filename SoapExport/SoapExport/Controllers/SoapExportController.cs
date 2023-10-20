using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoapExport.Data;
using SoapExport.Models;


[Route("api/[controller]")]
[ApiController]
public class SoapExportController : ControllerBase
{
    private readonly SoapExportContext _context;

    public SoapExportController(SoapExportContext context)
    {
        _context = context;
    }

    // GET: api/SoapExport
    [HttpGet]
    public async Task<IActionResult> GetCsvModels()
    {
        var data = await _context.CsvModels.ToListAsync();

        if (data == null || data.Count == 0)
        {
            return NotFound();
        }

        // Convert data to XML
        var xmlData = ConvertDataToXml(data);

        return Ok(xmlData);
    }

    private string ConvertDataToXml(List<CsvModels> data)
    {
        // Create an XML document and root element
        var xmlDoc = new XDocument(new XElement("CsvModels"));

        // Add each CsvModel as an element
        foreach (var item in data)
        {
            xmlDoc.Root.Add(
                new XElement("CsvModel",
                    new XElement("Id", item.Id),
                    new XElement("Name", item.Name),
                    new XElement("Age", item.Age),
                    new XElement("Country", item.Country)
                )
            );
        }

        return xmlDoc.ToString();
    }
}
