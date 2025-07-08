using Microsoft.AspNetCore.Mvc;
using code_MVC.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace code_MVC.Controllers
{
    public class ApiCoursesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string apiBase = "https://localhost:7133/api/Courses";
        public ApiCoursesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var courses = await client.GetFromJsonAsync<List<CourseDto>>(apiBase);
            return View(courses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseDto course)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync(apiBase, course);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            ModelState.AddModelError("", "Kayıt eklenemedi.");
            return View(course);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var course = await client.GetFromJsonAsync<CourseDto>($"{apiBase}/{id}");
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CourseDto course)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PutAsJsonAsync($"{apiBase}/{id}", course);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            ModelState.AddModelError("", "Güncelleme başarısız.");
            return View(course);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var course = await client.GetFromJsonAsync<CourseDto>($"{apiBase}/{id}");
            if (course == null) return NotFound();
            return View(course);
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
            var course = await client.GetFromJsonAsync<CourseDto>($"{apiBase}/{id}");
            if (course == null) return NotFound();
            return View(course);
        }
    }
} 