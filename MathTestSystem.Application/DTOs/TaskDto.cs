namespace MathTestSystem.Application.DTOs
{
    public class TaskDto
    {
        public string Id { get; set; }
        public string TaskText { get; set; }
        public bool IsCorrect { get; set; }
        public double CorrectResult { get; set; }
        public double StudentResult { get; set; }
    }
}
