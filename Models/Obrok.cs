using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public enum TipObroka{
        Dorucak,Rucak,Uzina,Vecera
    }
    public class Obrok
    {
        [KeyAttribute]
        public int ID { get; set; }
        [Required]
        public int Kalorije { get; set; }
        [Required]
        public int Masti { get; set; }
        [Required]
        public int UgljeniHidrati { get; set; }
        [Required]
        public int Proteini { get; set; }
        [Required,MaxLength(20)]
        public string Tip { get; set; }
        public List<Porcija> Porcija { get; set; }
        //[Required]
        [JsonIgnore]
        public Dan Dan { get; set; }
    }
}