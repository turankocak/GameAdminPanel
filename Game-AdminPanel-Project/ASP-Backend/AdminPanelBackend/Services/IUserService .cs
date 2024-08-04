using System.Threading.Tasks;
using AdminPanelBackend.Models;
using AdminPanelBackend.Data;

namespace AdminPanelBackend.Services
{
    public interface IUserService
    {
        Task<bool> Register(RegisterModel registerModel);
        Task<bool> Login(LoginModel loginModel);
    }
}
