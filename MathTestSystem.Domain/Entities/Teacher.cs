// MathTestSystem.Domain/Entities/Teacher.cs

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Teacher
{
    [Key]
    public string ID { get; set; }

    public ICollection<Student> Students { get; set; } = new List<Student>();
}
