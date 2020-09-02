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

        [HttpGet("")]
        public IActionResult GetAllSchuelerSorted([FromQuery] string query)
        {
            // ../api/schueler?orderby:name:asc;filter:musterman;skip:1;take:5

            int rowCount = 0;
            query = query.ToLower();

            string[] functions = query.Split(';', StringSplitOptions.RemoveEmptyEntries);

            int? skip = null;
            int? take = null;
            string filter = String.Empty;
            string sortColumnName = String.Empty;
            string sortDirection = "asc";

            foreach (string item in functions)
            {
                string[] parameters = item.Split(':');
                if (item.StartsWith("orderby"))
                {
                    sortColumnName = parameters[1];
                    sortDirection = parameters[2];
                }
                if (item.StartsWith("filter"))
                {
                    filter = parameters[1];
                }
                if (item.StartsWith("skip"))
                {
                    skip = Int32.Parse(parameters[1]);
                }
                if (item.StartsWith("take"))
                {
                    take = Int32.Parse(parameters[1]);
                }
            }

            IQueryable<schueler> dbSet = (IQueryable<schueler>)_dbContext.schueler;
            rowCount = dbSet.Count();

            if (!String.IsNullOrEmpty(sortColumnName))
            {
                switch (sortColumnName)
                {
                    default:
                        dbSet = dbSet.OrderBy(c => c.S_SCHNR);
                        if (sortDirection == "desc")
                        {
                            dbSet = dbSet.OrderByDescending(c => c.S_SCHNR);
                        }
                        break;
                    case "name":
                        dbSet = dbSet.OrderBy(c => c.S_Name);
                        if (sortDirection == "desc")
                        {
                            dbSet = dbSet.OrderByDescending(c => c.S_Name);
                        }
                        break;
                    case "vorname":
                        dbSet = dbSet.OrderBy(c => c.S_Vorname);
                        if (sortDirection == "desc")
                        {
                            dbSet = dbSet.OrderByDescending(c => c.S_Vorname);
                        }
                        break;
                    case "adresse":
                        dbSet = dbSet.OrderBy(c => c.S_Adresse);
                        if (sortDirection == "desc")
                        {
                            dbSet = dbSet.OrderByDescending(c => c.S_Adresse);
                        }
                        break;
                    case "klasse":
                        dbSet = dbSet.OrderBy(c => c.S_K_Klasse);
                        if (sortDirection == "desc")
                        {
                            dbSet = dbSet.OrderByDescending(c => c.S_K_Klasse);
                        }
                        break;
                }
            }
            if (!String.IsNullOrEmpty(filter))
            {
                dbSet = dbSet.Where(c => c.S_Name
                .Contains(filter) 
                    || c.S_Vorname.Contains(filter) 
                    || c.S_K_Klasse.Contains(filter) 
                    || c.S_Adresse.Contains(filter)
                );
                rowCount = dbSet.Count();
            }
            if (skip.HasValue)
            {
                dbSet = dbSet.Skip(skip.Value);
            }
            if (take.HasValue)
            {
                dbSet = dbSet.Take(take.Value);
            }

            return Ok(new SchuelerDataDto()
            {
                RowCount = rowCount,
                Schuelers = dbSet.Select(c => new SchuelerDto()
                {
                    Adresse = c.S_Adresse,
                    Klasse = c.S_K_Klasse,
                    Name = c.S_Name,
                    Id = c.S_SCHNR,
                    Vorname = c.S_Vorname
                }).ToList()
            });
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

    public class SchuelerDataDto
    {
        public int RowCount { get; set; }

        public List<SchuelerDto> Schuelers { get; set; }
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