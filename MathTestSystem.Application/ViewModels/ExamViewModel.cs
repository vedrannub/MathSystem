// MathTestSystem.API/Models/ExamViewModel.cs
namespace MathTestSystem.API.Models
{
    public class ExamViewModel
    {
        public string ID { get; set; }
        public List<ExamTaskViewModel> Tasks { get; set; } = new List<ExamTaskViewModel>();
    }
}
