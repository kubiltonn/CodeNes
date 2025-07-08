using Microsoft.AspNetCore.Mvc;
using code_MVC.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace code_MVC.Controllers
{
    public class ApiAssignmentsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string apiBase = "https://localhost:7133/api/Assignments";
        public ApiAssignmentsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var assignments = await client.GetFromJsonAsync<List<AssignmentDto>>(apiBase);
            return View(assignments);
        }

        public async Task<IActionResult> Create()
        {
            var client = _httpClientFactory.CreateClient();
            var courses = await client.GetFromJsonAsync<List<CourseDto>>("https://localhost:7133/api/Courses");
            ViewBag.Courses = courses;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AssignmentDto assignment)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync(apiBase, assignment);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            ModelState.AddModelError("", "Kayıt eklenemedi.");
            return View(assignment);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var assignment = await client.GetFromJsonAsync<AssignmentDto>($"{apiBase}/{id}");
            if (assignment == null) return NotFound();
            var courses = await client.GetFromJsonAsync<List<CourseDto>>("https://localhost:7133/api/Courses");
            ViewBag.Courses = courses;
            return View(assignment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AssignmentDto assignment)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PutAsJsonAsync($"{apiBase}/{id}", assignment);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            ModelState.AddModelError("", "Güncelleme başarısız.");
            return View(assignment);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var assignment = await client.GetFromJsonAsync<AssignmentDto>($"{apiBase}/{id}");
            if (assignment == null) return NotFound();
            return View(assignment);
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
            var assignment = await client.GetFromJsonAsync<AssignmentDto>($"{apiBase}/{id}");
            if (assignment == null) return NotFound();
            return View(assignment);
        }
    }
} 