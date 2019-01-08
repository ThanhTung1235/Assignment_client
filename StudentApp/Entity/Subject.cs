using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Entity
{
    class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string Description { get; set; }
    }
}
