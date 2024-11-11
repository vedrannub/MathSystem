using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ExamTask
{
    [Key, Column(Order = 0)]
    public string StudentID { get; set; }

    [Key, Column(Order = 1)]
    public string ExamID { get; set; }

    [Key, Column(Order = 2)]
    public string ID { get; set; } 

    public Exam Exam { get; set; }

    public string TaskText { get; set; }
    public double? CorrectResult { get; set; }
    public double? StudentResult { get; set; }
    public bool IsCorrect { get; set; }
}
