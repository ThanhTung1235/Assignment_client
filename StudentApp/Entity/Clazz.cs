using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Entity
{
    class Clazz
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string,string> studentClassRooms { get; set; }
    }
}
