using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _05_IntroToEF.Entities
{
    public class Flight
    {
        public int Number { get; set; }
        public string ArrivalCity { get; set; }
        public string DepartureCity { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int Rating { get; set; }


        //Navigation properties

        /////Relational Type : Flight --- Airplane (1....*)
        public Airplane Airplane { get; set; }//null 0nm454m65h4

        //Foreign key : RelatedEntityName + RelatedEntityNamePrimaryKey
        public int AirplaneId { get; set; }
        /////Relational Type : Flight --- Client (*....*)
        public ICollection<Client> Clients { get; set; }
    }
}
