using System.Collections.Generic;

namespace MathTestSystem.Application.DTOs
{
    public class ExamDto
    {
        public string Id { get; set; }
        public List<TaskDto> Tasks { get; set; }
    }
}
