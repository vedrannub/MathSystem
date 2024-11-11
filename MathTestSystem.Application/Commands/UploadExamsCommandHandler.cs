using MathTestSystem.Application.Commands;
using MathTestSystem.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

public class UploadExamsCommandHandler : IRequestHandler<UploadExamsCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IMathProcessor _mathProcessor;

    public UploadExamsCommandHandler(IApplicationDbContext context, IMathProcessor mathProcessor)
    {
        _context = context;
        _mathProcessor = mathProcessor;
    }

    public async Task<bool> Handle(UploadExamsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            XDocument xmlDoc = XDocument.Load(request.XmlStream);

            var teacherElement = xmlDoc.Root;
            var teacherID = teacherElement.Attribute("ID")?.Value;

            if (string.IsNullOrWhiteSpace(teacherID))
            {
                Console.WriteLine("Teacher ID is missing or empty.");
                return false;
            }

            // Retrieve or create the teacher
            var teacher = await _context.Teachers
                .Include(t => t.Students)
                    .ThenInclude(s => s.Exams)
                        .ThenInclude(e => e.Tasks)
                .FirstOrDefaultAsync(t => t.ID == teacherID, cancellationToken);

            if (teacher == null)
            {
                teacher = new Teacher { ID = teacherID };
                _context.Teachers.Add(teacher);
            }

            var studentsElement = teacherElement.Element("Students");
            foreach (var studentElement in studentsElement.Elements("Student"))
            {
                var studentID = studentElement.Attribute("ID")?.Value;

                if (string.IsNullOrWhiteSpace(studentID))
                {
                    Console.WriteLine("Student ID is missing or empty.");
                    continue;
                }

                // Retrieve or create the student
                var student = teacher.Students.FirstOrDefault(s => s.ID == studentID);
                if (student == null)
                {
                    student = new Student { ID = studentID, TeacherID = teacherID };
                    teacher.Students.Add(student);
                }

                foreach (var examElement in studentElement.Elements("Exam"))
                {
                    var examID = examElement.Attribute("ID")?.Value;

                    if (string.IsNullOrWhiteSpace(examID))
                    {
                        Console.WriteLine("Exam ID is missing or empty.");
                        continue;
                    }

                    // Retrieve or create the exam
                    var exam = student.Exams.FirstOrDefault(e => e.ID == examID);
                    if (exam == null)
                    {
                        exam = new Exam
                        {
                            ID = examID,
                            StudentID = studentID
                        };
                        student.Exams.Add(exam);
                    }

                    foreach (var taskElement in examElement.Elements("Task"))
                    {
                        var taskID = taskElement.Attribute("ID")?.Value;

                        if (string.IsNullOrWhiteSpace(taskID))
                        {
                            Console.WriteLine("Task ID is missing or empty.");
                            continue;
                        }

                        var taskText = taskElement.Value.Trim();

                        // Retrieve or create the task
                        var task = exam.Tasks.FirstOrDefault(t => t.ID == taskID);
                        if (task == null)
                        {
                            task = new ExamTask
                            {
                                ID = taskID,
                                StudentID = studentID,
                                ExamID = examID,
                                TaskText = taskText
                            };
                            exam.Tasks.Add(task);
                        }

                        ProcessTask(task);
                    }
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }

    private void ProcessTask(ExamTask task)
    {
        var parts = task.TaskText.Split('=');
        if (parts.Length != 2)
        {
            task.IsCorrect = false;
            return;
        }

        var expression = parts[0].Trim();
        var studentResultStr = parts[1].Trim();

        if (!double.TryParse(studentResultStr, out double studentResult))
        {
            task.IsCorrect = false;
            return;
        }

        task.StudentResult = studentResult;

        var correctResult = _mathProcessor.EvaluateExpression(expression);
        task.CorrectResult = correctResult;

        task.IsCorrect = Math.Abs(correctResult - studentResult) < 0.0001;
    }
}
