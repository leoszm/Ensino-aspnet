using System.ComponentModel.DataAnnotations;

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
        
        //retornar o usuario para a pagina que ele estava querendo acessar

        public string ReturnUrl { get; set; }
    }
}
