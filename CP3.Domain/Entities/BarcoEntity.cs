using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CP3.Domain.Entities
{
    [Table("tb_")]
    public class BarcoEntity
    {
        [Key]
        int Id { get; set; }
        [Required]
        string Nome { get; set; }
        [Required]
        String Modelo { get; set; }
        int Ano { get; set; }
        double Tamanho { get; set; }



    }
}
