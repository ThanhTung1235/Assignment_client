using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Entity
{
    class Mark
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public string createdAt { get; set; }
        public string updateAt { get; set; }
        public int Type { get; set; }
    }
}
