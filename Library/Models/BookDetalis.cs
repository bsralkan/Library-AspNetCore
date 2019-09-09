using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class BookDetails
    {
        public int ID { get; set; }
        public int ISBN { get; set; }
        public string BookName { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReservedDate { get; set; }
        public string ReservedPerson { get; set; }
    }
}
