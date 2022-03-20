using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    public class Porcija
    {
        [KeyAttribute]
        public int ID { get; set; }
        [Required]
        public int Velicina { get; set; }
        [Range(0,5000)]
        public int Kalorije { get; set; }
        [Range(0,5000)]
        public int UgljeniHidrati { get; set; }
        [Range(0,5000)]
        public int Proteini { get; set; }
        [Range(0,5000)]
        public int Masti { get; set; }
        [Required]
        [JsonIgnore]
        public Hrana Hrana { get; set; }
        [JsonIgnore]
        public Obrok Obrok { get; set; }
    }
}