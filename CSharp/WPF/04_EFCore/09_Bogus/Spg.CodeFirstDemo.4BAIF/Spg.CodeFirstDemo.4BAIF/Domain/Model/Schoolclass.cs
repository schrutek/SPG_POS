using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Spg.CodeFirstDemo._4BAIF.Domain.Model
{
    /// <summary>
    /// hat: 1:n Pupils ; 1:n Lessons ; 1:n Tests ; TeacherNavigation
    /// </summary>
    public class Schoolclass : IEquatable<Schoolclass?>
    {
        [MaxLength(255)]
        public string Id { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;



        public List<Pupil> Pupils { get; set; } = new();

        public List<Lesson> Lessons { get; set; } = new();

        public List<Test> Tests { get; set; } = new();

        [Required]
        [MaxLength(8)]
        public string TeacherId { get; set; } = default!;

        public Teacher TeacherNavigation { get; set; } = default!;

        public override bool Equals(object? obj)
        {
            return Equals(obj as Schoolclass);
        }

        public bool Equals(Schoolclass? other)
        {
            return other != null &&
                   Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
