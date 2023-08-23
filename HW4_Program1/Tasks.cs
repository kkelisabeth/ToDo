using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4_Program1
{
    internal class Tasks
    {
        [JsonProperty("taskDescription")]
        public string taskDescription { get; set; } 
    }
}
