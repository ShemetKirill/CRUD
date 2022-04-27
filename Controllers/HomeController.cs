using CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CRUD.Database;
using CRUD.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Shyjus.BrowserDetection;

namespace CRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly SqlDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IBrowserDetector browserDetector;

        public HomeController(ILogger<HomeController> logger, SqlDbContext context, IBrowserDetector browserDetector)
        {
            _logger = logger;
            _context = context;
            this.browserDetector = browserDetector;

        }

        public IActionResult Index()
        {
            var hostName = System.Net.Dns.GetHostName();
            var ips = System.Net.Dns.GetHostAddresses(hostName);

            ViewData["Name"] = browserDetector.Browser.Name;
            ViewData["DeviceType"] = browserDetector.Browser.DeviceType;
            ViewData["OS"] = browserDetector.Browser.OS;
            ViewData["Version"] = browserDetector.Browser.Version;
            ViewData["ipv4"] = ips[1];
            ViewData["ipv6"] = ips[0];
            return View();
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

        public IActionResult List()
        {
            List < Note > a = _context.Notes.Include(n => n.CreationDate).ToList();
            foreach (var item in a)
            {
                Console.WriteLine($"{item.Name}//{item.CreationDate.Date}");
            }
            return View(_context.Notes.Include(c => c.CreationDate).ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Note model) 
        {
            var dateItem = new CreationDate();
            dateItem.Date = DateTime.Now;
            _context.Dates.Add(dateItem);
            _context.SaveChanges();

            model.CreationDateId = dateItem.Id;

            _context.Notes.Add(model);

            _context.SaveChanges();
            return View("List");

        }

        public IActionResult Delete(int Id)
        {
            var item= _context.Notes.FirstOrDefault(n => n.Id == Id);
            _context.Notes.Remove(item);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var item= _context.Notes
                .Include(c=>c.CreationDate)
                .SingleOrDefault(n => n.Id == Id);
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(Note model)
        {
            _context.Notes.Update(model);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            var item = _context.Notes
                .Include(c => c.CreationDate)
                .SingleOrDefault(n => n.Id == Id);
            return View(item);
        }


    }
}