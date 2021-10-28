using System;
using System.Linq;

namespace InfoTrackScraperConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                return;
            }
            var scraper = new InfoTrackScraperLib.Scraper();
            var terms = string.Join(" ", args.Select((a, i) => new { a, i }).Where(it => it.i > 0).Select(it => it.a));
            var task = scraper.Execute(args[0], terms);
            task.Wait();
            var positions = task.Result;

            foreach (var id in positions)
            {
                Console.WriteLine(id);
            }

        }
    }
}
