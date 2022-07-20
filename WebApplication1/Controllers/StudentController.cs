using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Azure.Data.Tables;
using System.Threading.Tasks;
using System;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public readonly ICosmosDbService _cosmosDBService;
        public StudentController(ICosmosDbService carCosmosService)
        {
            _cosmosDBService = carCosmosService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sqlCosmosQuery = "Select * from c";
            var result = await _cosmosDBService.GetItemsAsync(sqlCosmosQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Student student)
        {
            student.Id = Guid.NewGuid().ToString();
            var result = await _cosmosDBService.AddItemAsync(student);
            return Ok(result);
        }
    }
}
