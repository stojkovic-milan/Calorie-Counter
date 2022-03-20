using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Dan
    {
        [KeyAttribute]
        public int ID { get; set; }
        [Required]
        public DateTime Datum { get; set; }
        [Required]
        public int Kalorije { get; set; }
        [Required]
        public int UgljeniHidrati { get; set; }
        [Required]
        public int Proteini { get; set; }
        [Required]
        public int Masti { get; set; }
        public int Kilaza { get; set; }
        [Required]
        public Osoba Osoba { get; set; }
        public List<Obrok> Obroci { get; set; }
        //public List<Vezbao> Vezbe { get; set; }
    }
}