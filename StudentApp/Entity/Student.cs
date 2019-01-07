using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Entity
{
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string DoB { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        //public List<StudentClass> StudentClasses { get; set; }
        public StudentStatus Status { get; set; }

    }

    public enum StudentStatus
    {
        Active = 1,
        Deactive = 0
    }
}
