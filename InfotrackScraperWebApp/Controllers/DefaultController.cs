using InfoTrackScraper.Models;
using InfoTrackScraperLib;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InfoTrackScraper.Controllers
{
    public class DefaultController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(new Scrape());
        }

        [HttpPost]
        public async Task<ActionResult> Index(Scrape scrape)
        {
            var scraper = new Scraper();
            scrape.Results = await scraper.Execute(scrape.Url, scrape.Terms);
            return View(scrape);
        }
    }
}
