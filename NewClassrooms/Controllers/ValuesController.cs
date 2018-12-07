using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewClassrooms.Models;
using NewClassrooms.Services;
using Newtonsoft.Json;

namespace NewClassrooms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcome! Please send a POST request with RandomUser data as the body, either as a JSON string or a file containing JSON.");
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] ResultWrapper data)
        {
            // generate analytics, return
            Analytics[] results =  new AnalyticsService().GenerateDefaultAnalytics(data.Results);

            return Ok(results);
        }

        // POST api/values
        [HttpPost]
        [Route("/api/values/postWithFile")]
        public IActionResult Post(IFormFile file)
        {
            ResultWrapper data;
            // read file, parse json
            using (StreamReader reader = new StreamReader(file.OpenReadStream()))
            {
                data = JsonConvert.DeserializeObject<ResultWrapper>(reader.ReadToEnd());
            }

            // generate analytics, return
            Analytics[] results = new AnalyticsService().GenerateDefaultAnalytics(data.Results);

            return Ok(results);
        }
    }
}
