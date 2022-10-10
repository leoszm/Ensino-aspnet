using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        

        public AccountController(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel(){
                ReturnUrl = returnUrl
            });
        }

        //Neste objeto LoginVM será colocado as informações de login e senha do viewmodel e vai retornar returnurl
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            var user = await _userManager.FindByNameAsync(loginVM.UserName);

           if(user != null)
            {
                //                                                                          não persistir o cookie se a sessão fechar
                var result = await _signInManager.PasswordSignInAsync(user, 
                    loginVM.Password, false, false);
                //                                                                                    não bloquear user se o login falhar

                //user existe
                if (result.Succeeded){
                    if (string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        //se o url for nulo ou vazio, direciona para método index controlador home
                        return RedirectToAction("Index", "Home");
                    }//caso não redireciona ele para o url desejado
                    return Redirect(loginVM.ReturnUrl);
                }
            }//se for nulo
            ModelState.AddModelError("", "Falha ao tentar realizar o login...");
            return View(loginVM);
        }

        //http get
        [AllowAnonymous]
        
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]//processar o envio do formulario
        [ValidateAntiForgeryToken]//antiforgerytoken(evitar ataques CSRF)
        public async Task<IActionResult> Register(LoginViewModel registroVM)
        //             retornar iaction result    receber dados do loginviewmodel que será chamado de registro
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = registroVM.UserName };
                var result = await _userManager.CreateAsync(user, registroVM.Password);

                    if (result.Succeeded)
                    {
                        //await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        this.ModelState.AddModelError("Registro", "Falha na criação de registro");
                    }
            }
            return View(registroVM);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Logout() {
            //reforçar o codigo de sign out abaixo
            HttpContext.Session.Clear();
            //zerando valores atribuídos a usuário
            HttpContext.User = null;

            await _signInManager.SignOutAsync();
            //direcionar ao method index do controlador home
            return RedirectToAction("Index","Home");
        }
    }
}
