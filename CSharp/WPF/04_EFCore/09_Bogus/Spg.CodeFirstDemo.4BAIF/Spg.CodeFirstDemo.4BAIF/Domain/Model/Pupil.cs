using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.CodeFirstDemo._4BAIF.Domain.Model
{
    /// <summary>
    /// hat: SchoolclassNavigation
    /// </summary>
    public class Pupil
    {
        public int Id { get; init; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? Account { get; set; }



        public string SchoolclassId { get; set; } = default!;

        public Schoolclass SchoolclassNavigation { get; set; } = default!;
    }
}
