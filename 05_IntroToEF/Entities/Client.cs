using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _05_IntroToEF.Entities
{
    //Entities (Entity)
    [Table("Passangers")]
    class Client
    {
        //Primary key : Id/id/ID/ Entityname+Id
        public int Id { get; set; }

        [Required, MaxLength(150)]
        [Column("FirstName")]
        public string Name { get; set; }

        [Required, MaxLength(150)]
        public string Lastname { get; set; }

        [Required, MaxLength(150)]
        //[EmailAddress]
        public string Email { get; set; }
        //null --> not null -------------------
        //not null --> null +++++++++++++++++
        public DateTime Birthdate { get; set; }//not null --> null 
        public string Phone { get; set; }




        /////Relational Type : Flight --- Client (*....*)
        public ICollection<Flight> Flights { get; set; }


    }
}
