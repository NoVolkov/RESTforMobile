using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Snils { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
    }
}
