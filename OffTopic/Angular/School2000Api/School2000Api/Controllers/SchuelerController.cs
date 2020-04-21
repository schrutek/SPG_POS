using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School2000Api.Model;

namespace School2000Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchuelerController : ControllerBase
    {
        School2000Context _dbContext = null;

        public SchuelerController(School2000Context dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("all")]
        public IActionResult GetAllSchueler()
        {
            return Ok(_dbContext.schueler
                .Select(c => new SchuelerDto()
                {
                    Adresse = c.S_Adresse,
                    Klasse = c.S_K_Klasse,
                    Name = c.S_Name,
                    Id = c.S_SCHNR,
                    Vorname = c.S_Vorname
                }).ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetSchuelerDetails(short id)
        {
            return Ok(_dbContext.schueler.Where(c => c.S_SCHNR == id).Select(c => new SchuelerDto()
            {
                Adresse = c.S_Adresse,
                Klasse = c.S_K_Klasse,
                Name = c.S_Name,
                Id = c.S_SCHNR,
                Vorname = c.S_Vorname
            }).FirstOrDefault());
        }
    }

    public class SchuelerDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public string Adresse { get; set; }
        public string Klasse { get; set; }
    }
}