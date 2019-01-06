using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StudentApp.Entity
{
    class Clazz
    {
        [JsonProperty("studentId")]
        public int StudentId { get; set; }
        [JsonProperty("classRoomId")]
        public int ClassRoomId { get; set; }
        [JsonProperty("joinedAt")]
        public string joinedAt { get; set; }
        [JsonProperty("leftAt")]
        public string leftAt { get; set; }
    }
}
