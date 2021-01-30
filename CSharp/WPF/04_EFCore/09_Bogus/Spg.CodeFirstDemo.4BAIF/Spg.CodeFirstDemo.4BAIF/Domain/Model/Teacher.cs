using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Spg.CodeFirstDemo._4BAIF.Domain.Model
{
    /// <summary>
    /// hat: 1:n Lessons ; 1:n Tests ; 1:n Schoolclasses
    /// </summary>
    public class Teacher : IEquatable<Teacher?>
    {
        [MaxLength(8)]
        public string Id { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Lastname { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Firstname { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string? Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(64)]
        public string AccountName { get; set; } = string.Empty;



        public List<Lesson> Lessons { get; set; } = new();

        public List<Test> Tests { get; set; } = new();

        public List<Schoolclass> Schoolclasses { get; set; } = new();

        public override bool Equals(object? obj)
        {
            return Equals(obj as Teacher);
        }

        public bool Equals(Teacher? other)
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
