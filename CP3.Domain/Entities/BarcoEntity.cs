using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CP3.Domain.Entities
{
    [Table("tb_")]
    public class BarcoEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public String Modelo { get; set; }
        public int Ano { get; set; }
        public double Tamanho { get; set; }



    }
}
