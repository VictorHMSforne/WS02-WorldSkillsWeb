using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WS02._1.Models
{
    [Table("Paciente")]
    public class Paciente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o Nome do Paciente")]
        public string nome_paciente { get; set; }

        [Required(ErrorMessage = "Digite o Nome do Doutor")]
        public string nome_doutor { get; set; }

        [Required(ErrorMessage = "Digite o Tipo do Quarto")]
        public string tipo_quarto { get; set; }

        [Required(ErrorMessage = "Digite o Número do Quarto")]
        public int quarto { get; set; }
    }
}
