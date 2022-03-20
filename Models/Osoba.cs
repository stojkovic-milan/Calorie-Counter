using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Osoba
    {
        [KeyAttribute]
        public int ID { get; set; }
        [Required,MaxLength(20)]
        public string Ime { get; set; }
        [Required,MaxLength(20)]
        public string Prezime { get; set; }
        [Required,RegularExpression("M|Z")]
        public char Pol { get; set; }
        [Required]
        public int CiljKg { get; set; }
        public int TrenutnoKg { get; set; }
        public int CiljKcal { get; set; }
        [Required,Range(100,250)]
        public int Visina { get; set; }
        [Required ]
        public int PocetakKg { get; set; }
        [Required,Range(18,200)]
        public int Godine { get; set; }
        [Required,Range(1,3)]
        public int FizAktivnost { get; set; }//1-ne aktivna,2-srednje aktivna,3-aktivna
        [JsonIgnore]
        public List<Dan> Dani { get; set; }
    }
}