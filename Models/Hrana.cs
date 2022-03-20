using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    public class Hrana
    {
        [KeyAttribute]
        public int ID { get; set; }
        [MaxLength(50),Required]
        public string Naziv { get; set; }
        [Required,Range(0,2000)]
        public int Kalorije { get; set; }
        [Range(0,100),Required]
        public int UgljeniHidrati { get; set; }
        [Range(0,100),Required]
        public int Proteini { get; set; }
        [Range(0,100),Required]
        public int Masti { get; set; }
        [JsonIgnore]
        public List<Porcija> Porcije { get; set; }
    }
}