using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Exam
{
    [Key, Column(Order = 0)]
    public string StudentID { get; set; }

    [Key, Column(Order = 1)]
    public string ID { get; set; } 

    public Student Student { get; set; }

    public ICollection<ExamTask> Tasks { get; set; } = new List<ExamTask>();
}
