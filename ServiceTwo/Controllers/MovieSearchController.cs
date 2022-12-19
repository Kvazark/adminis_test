using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ServiceTwo.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MovieSearchController : ControllerBase
    {
        private static readonly string[] Genres = new[]
        {
            "Horrors", "Comedy", "Drama", "Fantasy", "Science fiction", "Documentary", "Action movie", "Thriller", "Melodrama", "Detective"
        };
        private static readonly string[] Types = new[]
        {
            "movie", "cartoon", "serial", 
        };

        private readonly ILogger<MovieSearchController> _logger;

        public MovieSearchController(ILogger<MovieSearchController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetMovieSearch")]
        public IEnumerable<MovieSearch> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new MovieSearch
                {
                    Date = DateTime.Now.AddDays(index),
                    Year = Random.Shared.Next( 1990, 2022),
                    Type = Types[Random.Shared.Next(Types.Length)],
                    Genre = Genres[Random.Shared.Next(Genres.Length)]
                })
                .ToArray();
        }
    }
}