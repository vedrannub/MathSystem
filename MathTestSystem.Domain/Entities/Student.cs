// MathTestSystem.Domain/Entities/Student.cs

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Student
{
    [Key]
    public string ID { get; set; }

    public string TeacherID { get; set; }
    public Teacher Teacher { get; set; }

    public ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
