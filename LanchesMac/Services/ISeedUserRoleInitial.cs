namespace LanchesMac.Services
{
    public interface ISeedUserRoleInitial
    {

        void SeedRoles();//deverá ser implementada a criação dos perfis
        void SeedUsers();//criar usuários e atribuir os usuários aos perfis
        //para atribuir um usuário aos perfis, deve ser criado primeiro os perfis
    }
}
