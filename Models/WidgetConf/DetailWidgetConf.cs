using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DEV_dashboard_2019.Models.WidgetConf
{
    public class DetailWidgetConf
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required]
        [Display(Name = "Movie Name")]
        public string Name { get; set; }
    }
}
