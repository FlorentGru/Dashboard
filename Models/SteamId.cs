using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DEV_dashboard_2019.Models
{
    [DataContract]
    public class SteamId
    {
        public SteamId() { Key = 0; }

        [Key]
        public int Key { get; set; }

        [DataMember(Name = "steamid")]
        public string Id { get; set; }

        [DataMember(Name = "success")]
        public int Success { get; set; }
    }
}
