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
        public IActionResult Xy()
        {
            SchuelerDb db = SchuelerDb.FromMockup();

            ICollection<Pupil> result = (ICollection<Pupil>)db.Pupil;

            return View(result);
        }
    }
}
