using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _05_IntroToEF.Entities
{
    class Airplane
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int MaxPassangers { get; set; }


        //Navigation properties
        /////Relational Type : Flight --- Airplane (1....*)
        public ICollection<Flight> Flights { get; set; }

    }
}
