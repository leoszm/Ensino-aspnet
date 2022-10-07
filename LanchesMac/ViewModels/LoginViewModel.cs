using System.ComponentModel.DataAnnotations;
using static LanchesMac.Controllers.AccountController;

namespace LanchesMac.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage="Informe o nome")]
        [Display(Name ="Usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Informe a Senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        
        [Display(Name = "Ciente")]
        [CheckBoxRequired(ErrorMessage = "É obrigatório aceitar os termos de compromisso!")]
        public bool IsAccepted { get; set; }

        //retornar o usuario para a pagina que ele estava querendo acessar

        public string ReturnUrl { get; set; }
    }
}
