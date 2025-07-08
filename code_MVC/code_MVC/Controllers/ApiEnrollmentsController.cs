using Microsoft.AspNetCore.Mvc;
using code_MVC.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace code_MVC.Controllers
{
    public class ApiEnrollmentsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string apiBase = "https://localhost:7133/api/Enrollments";
        public ApiEnrollmentsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var enrollments = await client.GetFromJsonAsync<List<EnrollmentDto>>(apiBase);
            return View(enrollments);
        }

        public async Task<IActionResult> Create()
        {
            var client = _httpClientFactory.CreateClient();
            var students = await client.GetFromJsonAsync<List<StudentDto>>("https://localhost:7133/api/Students");
            var courses = await client.GetFromJsonAsync<List<CourseDto>>("https://localhost:7133/api/Courses");
            ViewBag.Students = students;
            ViewBag.Courses = courses;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EnrollmentDto enrollment)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync(apiBase, enrollment);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            ModelState.AddModelError("", "Kayıt eklenemedi.");
            return View(enrollment);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var enrollment = await client.GetFromJsonAsync<EnrollmentDto>($"{apiBase}/{id}");
            if (enrollment == null) return NotFound();
            var students = await client.GetFromJsonAsync<List<StudentDto>>("https://localhost:7133/api/Students");
            var courses = await client.GetFromJsonAsync<List<CourseDto>>("https://localhost:7133/api/Courses");
            ViewBag.Students = students;
            ViewBag.Courses = courses;
            return View(enrollment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EnrollmentDto enrollment)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PutAsJsonAsync($"{apiBase}/{id}", enrollment);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            ModelState.AddModelError("", "Güncelleme başarısız.");
            return View(enrollment);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var enrollment = await client.GetFromJsonAsync<EnrollmentDto>($"{apiBase}/{id}");
            if (enrollment == null) return NotFound();
            return View(enrollment);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{apiBase}/{id}");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var enrollment = await client.GetFromJsonAsync<EnrollmentDto>($"{apiBase}/{id}");
            if (enrollment == null) return NotFound();
            return View(enrollment);
        }
    }
} 