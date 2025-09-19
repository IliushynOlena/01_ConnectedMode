using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _05_IntroToEF.Entities
{
    class Flight
    {
        [Key]//set primary key
        public int Number { get; set; }

        [Required]
        [MaxLength(100)]
        public string ArrivalCity { get; set; }

        [Required, MaxLength(100)]
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
