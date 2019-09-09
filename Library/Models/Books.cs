using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Books
    {
        public int ID { get; set; }
        public int ISBN { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string  Register { get; set; }
        [DataType(DataType.Date)]
        public DateTime LastCheckedDate  { get; set; }
        public string LastCheckedPerson { get; set; }

    }
}
