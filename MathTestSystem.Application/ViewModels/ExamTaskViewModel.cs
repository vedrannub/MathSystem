// MathTestSystem.API/Models/ExamTaskViewModel.cs
namespace MathTestSystem.API.Models
{
    public class ExamTaskViewModel
    {
        public string ID { get; set; }
        public string TaskText { get; set; }
        public double? CorrectResult { get; set; }
        public double? StudentResult { get; set; }
        public bool IsCorrect { get; set; }
    }
}
