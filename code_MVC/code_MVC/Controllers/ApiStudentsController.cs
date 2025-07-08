using Microsoft.AspNetCore.Mvc;
using code_MVC.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace code_MVC.Controllers
{
    public class ApiStudentsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string apiBase = "https://localhost:7133/api/Students";
        public ApiStudentsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var students = await client.GetFromJsonAsync<List<StudentDto>>(apiBase);
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentDto student)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync(apiBase, student);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            ModelState.AddModelError("", "Kayıt eklenemedi.");
            return View(student);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var student = await client.GetFromJsonAsync<StudentDto>($"{apiBase}/{id}");
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, StudentDto student)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PutAsJsonAsync($"{apiBase}/{id}", student);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            ModelState.AddModelError("", "Güncelleme başarısız.");
            return View(student);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var student = await client.GetFromJsonAsync<StudentDto>($"{apiBase}/{id}");
            if (student == null) return NotFound();
            return View(student);
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
            var student = await client.GetFromJsonAsync<StudentDto>($"{apiBase}/{id}");
            if (student == null) return NotFound();
            return View(student);
        }
    }
} 