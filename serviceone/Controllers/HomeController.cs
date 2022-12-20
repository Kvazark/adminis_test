using System;
using System.Net.Http;
using System.Net.Http.Headers; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace serviceone.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private HttpClient _client;

        public HomeController()
        {
            _client = new HttpClient();
            var url =
                new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["url"];
            _client.BaseAddress = new Uri("url");
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpGet("check")]
        public IActionResult Check()
        {
            return Ok("It works!!!");
        }


        [HttpGet("network")]
        public IActionResult NetworkRequest()
        {
            var response = _client.GetAsync("MovieSearch").Result;
            return Ok(response.Content.ReadAsStringAsync().Result);
        }
    }
}