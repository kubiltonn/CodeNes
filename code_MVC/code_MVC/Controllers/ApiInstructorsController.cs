using Microsoft.AspNetCore.Mvc;
using code_MVC.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace code_MVC.Controllers
{
    public class ApiInstructorsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string apiBase = "https://localhost:7133/api/Instructors";
        public ApiInstructorsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var instructors = await client.GetFromJsonAsync<List<InstructorDto>>(apiBase);
            return View(instructors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(InstructorDto instructor)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync(apiBase, instructor);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            ModelState.AddModelError("", "Kayıt eklenemedi.");
            return View(instructor);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var instructor = await client.GetFromJsonAsync<InstructorDto>($"{apiBase}/{id}");
            if (instructor == null) return NotFound();
            return View(instructor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, InstructorDto instructor)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PutAsJsonAsync($"{apiBase}/{id}", instructor);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            ModelState.AddModelError("", "Güncelleme başarısız.");
            return View(instructor);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var instructor = await client.GetFromJsonAsync<InstructorDto>($"{apiBase}/{id}");
            if (instructor == null) return NotFound();
            return View(instructor);
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
            var instructor = await client.GetFromJsonAsync<InstructorDto>($"{apiBase}/{id}");
            if (instructor == null) return NotFound();
            return View(instructor);
        }
    }
} 