using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Interfaces
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate(string email, string password);
        Task<bool> RegisterUser(string email, string password);
        Task Logout();

    }
}
