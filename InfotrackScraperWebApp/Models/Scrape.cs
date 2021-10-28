using System.Collections.Generic;
using System.ComponentModel;

namespace InfoTrackScraper.Models
{
    public class Scrape
    {
        [DisplayName("URL")]
        public string Url { get; set; }

        [DisplayName("Search Terms")]
        public string Terms { get; set; }

        [DisplayName("Ranking Results")]
        public List<int> Results { get; set; }
    }
}
