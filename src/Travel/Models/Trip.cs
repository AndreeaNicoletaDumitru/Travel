using System;
using System.Collections.Generic;

namespace Travel.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserName { get; set; }

        public ICollection<Stop> Stops { get; set; }
    }
}
