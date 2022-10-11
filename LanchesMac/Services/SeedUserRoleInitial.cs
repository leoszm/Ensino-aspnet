using Microsoft.AspNetCore.Identity;

namespace LanchesMac.Services
{
    //Implementando a classe
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }



        //assinatura dos methods para implementar
        public void SeedRoles()
        {
            //verificar se o perfil que deseja ser criado ja existe, caso não ele cria
            if (!_roleManager.RoleExistsAsync("Member").Result)
            {
                IdentityRole role = new IdentityRole();//usa instancia de identityrole
                role.Name           = "Member";//define nome do perfil
                role.NormalizedName = "MEMBER";//nome normalizado
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;//usa method createAsync que cria o perfil
            }
            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();//usa instancia de identityrole
                role.Name           = "Admin";//define nome do perfil
                role.NormalizedName = "ADMIN";//nome normalizado
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;//usa method createAsync que cria o perfil
            }
        }
        //Atribuir os usuários aos perfis que foram criados
        public void SeedUsers()
        {
            //tentar localizar usuário, caso não consiga retorna null
            if (_userManager.FindByEmailAsync("usuario@localhost").Result == null)
            {
                //propriedades, os dados do usuário que quer criar
                IdentityUser user       = new IdentityUser();
                user.UserName           = "usuario@localhost";
                user.Email              = "usuario@localhost";
                user.NormalizedUserName = "USUARIO@LOCALHOST";
                user.NormalizedEmail    = "USUARIO@LOCALHOST";
                user.EmailConfirmed     = true;//se vai confirmar o email
                user.LockoutEnabled     = false;//se vai bloquear a conta do usuario
                user.SecurityStamp      = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "Numsey#2022").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Member").Wait();
                }
            }
            if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
            {
                //propriedades, os dados do usuário que quer criar
                IdentityUser user       = new IdentityUser();
                user.UserName           = "admin@localhost";
                user.Email              = "admin@localhost";
                user.NormalizedUserName = "ADMIN@LOCALHOST";
                user.NormalizedEmail    = "ADMIN@LOCALHOST";
                user.EmailConfirmed     = true;//se vai confirmar o email
                user.LockoutEnabled     = false;//se vai bloquear a conta do usuario
                user.SecurityStamp      = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "Numsey#2022").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}