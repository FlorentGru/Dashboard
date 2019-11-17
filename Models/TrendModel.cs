using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Objects.Search;

namespace DEV_dashboard_2019.Models
{
    public class TrendModel
    {

        public TrendModel()
        {
            Movie = new List<SearchMovie>();
            Tv = new List<SearchTv>();
        }

        public List<SearchMovie> Movie { get; set; }

        public List<SearchTv> Tv { get; set; }
    }
}
