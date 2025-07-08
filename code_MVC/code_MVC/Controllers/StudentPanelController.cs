using Microsoft.AspNetCore.Mvc;
using code_MVC.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace code_MVC.Controllers
{
    public class StudentPanelController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StudentPanelController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int studentId = 1)
        {
            var client = _httpClientFactory.CreateClient();
            // API URL'lerini kendi ortamına göre güncelleyebilirsin
            var student = await client.GetFromJsonAsync<StudentDto>($"https://localhost:7133/api/Students/{studentId}");
            var enrollments = await client.GetFromJsonAsync<List<EnrollmentDto>>($"https://localhost:7133/api/Enrollments?studentId={studentId}");
            var courses = new List<CourseDto>();
            foreach (var enrollment in enrollments)
            {
                var course = await client.GetFromJsonAsync<CourseDto>($"https://localhost:7133/api/Courses/{enrollment.CourseId}");
                // Progress hesaplama örnek: random veya API'den alınabilir
                course.ProgressPercent = new System.Random().Next(10, 100);
                courses.Add(course);
            }
            var assignments = await client.GetFromJsonAsync<List<AssignmentDto>>($"https://localhost:7133/api/Assignments?studentId={studentId}");
            var model = new StudentPanelViewModel
            {
                StudentName = student?.Name ?? "Öğrenci",
                Courses = courses,
                Assignments = assignments
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Select()
        {
            var client = _httpClientFactory.CreateClient();
            var students = await client.GetFromJsonAsync<List<StudentDto>>("https://localhost:7133/api/Students");
            return View(students);
        }

        [HttpPost]
        public IActionResult Select(int studentId)
        {
            return RedirectToAction("Index", new { studentId });
        }
    }
}