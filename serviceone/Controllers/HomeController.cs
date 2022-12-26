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
        private IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _client = new HttpClient();
            _configuration = configuration;
            _client.BaseAddress = new Uri(_configuration.GetConnectionString("movieservice"));
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