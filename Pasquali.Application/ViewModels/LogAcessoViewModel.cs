using System.ComponentModel.DataAnnotations;

namespace Pasquali.Application.ViewModels
{
    public class LogAcessoViewModel
    {
        [Key]
        public int LogAcessoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string DataHoraAcesso { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string EnderecoIp { get; set; }

        public string UsuarioNome { get; set; }
    }
}