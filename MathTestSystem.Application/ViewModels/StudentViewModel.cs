
namespace MathTestSystem.API.Models
{
    public class StudentViewModel
    {
        public string ID { get; set; }
        public List<ExamViewModel> Exams { get; set; } = new List<ExamViewModel>();
    }
}
