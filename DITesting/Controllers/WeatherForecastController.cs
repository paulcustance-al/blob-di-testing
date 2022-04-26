using System;
using System.Collections.Generic;
using System.Linq;
using EHR.BlobStorage.Sdk.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DITesting.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IBlobStorageClient _blobStorageClient;

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IBlobStorageClient blobStorageClient)
    {
        _logger = logger;
        _blobStorageClient = blobStorageClient;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _blobStorageClient.GetBucket("Foo").ListBlobs();   
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}