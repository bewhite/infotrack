using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InfoTrackScraperLib
{
    public class Scraper
    {
        public async Task<List<int>> Execute(string url, string terms)
        {
            var rtn = new List<int>();

            for (int start = 0; start < 100; start += 10)
            {
                var hits = await ExecutePage(terms, start);

                int j = 0;
                foreach (var hit in hits)
                {
                    if (hit.Contains(url))
                    {
                        rtn.Add(start + j + 1);
                    }
                    j++;
                }
            }

            return rtn;
        }

        private static async Task<IEnumerable<string>> ExecutePage(string terms, int start)
        {
            var client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/93.0");
            var google = $"http://www.google.co.uk/search?q={terms.Replace(' ', '+')}&start={start}";

            var data = client.OpenRead(google);
            var reader = new StreamReader(data);
            string page = await reader.ReadToEndAsync();

            var cits = GetCitations(page, "<cite.*?>(.*?)</cite>");

            // The citations are repeated in the markup for some reason.
            return cits.Select((c, i) => new { c, i }).Where(it => it.i % 2 == 0).Select(it => it.c);
        }

        private static List<string> GetCitations(string page, string pattern)
        {
            var rtn = new List<string>();
            var regex = new Regex(pattern);
            var match = regex.Match(page);
            while (match.Success)
            {
                var link = match.Groups[1].ToString();
                rtn.Add(link);
                match = match.NextMatch();
            }
            return rtn;
        }
    }
}
