using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spg.MvcTest.WebApp.Models;

namespace Spg.MvcTest.WebApp.Controllers
{
    public class PupilController : Controller
    {
        public IActionResult Index()
        {
            SchuelerDb db = SchuelerDb.FromMockup();

            ICollection<Pupil> result = (ICollection<Pupil>)db.Pupil;

            for (int i = 0; i <= result.Count; i++)
            {
            }

            foreach (Pupil item in result)
            {
                Conslole.Writeline($"{item.Firstname}");
            }


            return View(result);
        }
    }
}
