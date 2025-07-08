using System.Collections.Generic;

namespace code_MVC.Models
{
    public class StudentPanelViewModel
    {
        public string StudentName { get; set; }
        public List<CourseDto> Courses { get; set; }
        public List<AssignmentDto> Assignments { get; set; }
    }

    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int DurationWeeks { get; set; }
        public int ProgressPercent { get; set; }
    }

    public class AssignmentDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CourseTitle { get; set; }
        public string DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
} 