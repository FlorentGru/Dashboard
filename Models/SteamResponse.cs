using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DEV_dashboard_2019.Models
{

    [DataContract]
    public class SteamResponse
    {
        [DataMember(Name = "response")]
        public SteamId Response { get; set; }
    }
}
