using System;
using System.Net.Http;
using System.Net.Http.Headers; 
using Microsoft.AspNetCore.Mvc;

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
            _client.BaseAddress = new Uri("http://servicetwo:80/");
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