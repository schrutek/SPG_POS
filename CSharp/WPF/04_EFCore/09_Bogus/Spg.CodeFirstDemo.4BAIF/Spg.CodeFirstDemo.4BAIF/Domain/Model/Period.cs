using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Spg.CodeFirstDemo._4BAIF.Domain.Model
{
    /// <summary>
    /// hat: 1:n Lessons ; 1:n Tests
    /// </summary>
    public class Period
    {
        [Key]
        public int Nr { get; init; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }



        public List<Lesson> Lessons { get; set; } = new();

        public List<Test> Tests { get; set; } = new();
    }
}
