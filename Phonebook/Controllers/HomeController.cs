using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
using Phonebook.Models;
using Newtonsoft.Json;

// ReSharper disable once IdentifierTypo
namespace Phonebook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ContactAPI.ContactAPI _contactApi = new ContactAPI.ContactAPI();

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public async Task<IActionResult> Index()
        {
            var contacts = new List<ContactData>();
            var client = _contactApi.Initial();
            var response = await client.GetAsync("api/contacts");

            if (!response.IsSuccessStatusCode) return View(contacts);
            var results = response.Content.ReadAsStringAsync().Result;
            contacts = JsonConvert.DeserializeObject<List<ContactData>>(results);

            return View(contacts);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string search)
        {
            var contacts = new List<ContactData>();
            var client = _contactApi.Initial();
            var response = await client.GetAsync("api/contacts");

            if (!response.IsSuccessStatusCode) return View(contacts);
            var results = response.Content.ReadAsStringAsync().Result;
            contacts = JsonConvert.DeserializeObject<List<ContactData>>(results);

            var query = from x in contacts select x;

            // sorting
            ViewBag.IdSortParam = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.NameSortParam = sortOrder == "name_desc" ? "name_asc" : "name_desc";
            ViewBag.AddressSortParam = sortOrder == "address_desc" ? "address_asc" : "address_desc";
            ViewBag.NumberSortParam = sortOrder == "number_desc" ? "number_asc" : "number_desc";

            switch (sortOrder)
            {
                case "id_desc":
                    query = query.OrderByDescending(x => x.Id);
                    break;
                case "id_asc":
                    query = query.OrderBy(x => x.Id);
                    break;
                case "name_desc":
                    query = query.OrderByDescending(x => x.Name);
                    break;
                case "name_asc":
                    query = query.OrderBy(x => x.Name);
                    break;
                case "address_desc":
                    query = query.OrderByDescending(x => x.Address);
                    break;
                case "address_asc":
                    query = query.OrderBy(x => x.Address);
                    break;
                case "number_desc":
                    query = query.OrderByDescending(x => x.Number);
                    break;
                default:
                    query = query.OrderBy(x => x.Id);
                    break;
            }

            // search
            ViewData["GetSearchDetails"] = search;
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Name.Contains(search) || x.Address.Contains(search) || x.Number.Contains(search));
            }

            return View(query.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ContactData contact)
        {
            var client = _contactApi.Initial();

            // HTTP POST
            var response = client.PostAsync("api/contacts", new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8, "application/json"));
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> Edit(long id)
        {
            var contact = new ContactData();
            var client = _contactApi.Initial();
            var response = await client.GetAsync($"api/contacts/{id}");

            if (!response.IsSuccessStatusCode) return View(contact);
            var results = response.Content.ReadAsStringAsync().Result;
            contact = JsonConvert.DeserializeObject<ContactData>(results);

            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ContactData contact)
        {
            var client = _contactApi.Initial();
            var response = await client.PutAsync($"api/contacts/{contact.Id}", new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> Details(long id)
        {
            var contact = new ContactData();
            var client = _contactApi.Initial();
            var response = await client.GetAsync($"api/contacts/{id}");

            if (!response.IsSuccessStatusCode) return View(contact);
            var results = response.Content.ReadAsStringAsync().Result;
            contact = JsonConvert.DeserializeObject<ContactData>(results);

            return View(contact);
        }

        public async Task<IActionResult> Delete(long id)
        {
            var client = _contactApi.Initial();
            
            await client.DeleteAsync($"api/contacts/{id}");
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
