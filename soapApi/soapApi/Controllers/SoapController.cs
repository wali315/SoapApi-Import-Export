using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; // Add this using directive

namespace soapApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoapController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetCsvData()
        {
            SoapService soap = new SoapService();
           
            var csvRecords = soap.ReadCsvData(@"C:\Users\pc\Desktop\data3.csv");
            var xmlData = soap.ConvertToXml(csvRecords);


            return Content(xmlData);

           // return Ok(csvRecords);
        }
    }
}
