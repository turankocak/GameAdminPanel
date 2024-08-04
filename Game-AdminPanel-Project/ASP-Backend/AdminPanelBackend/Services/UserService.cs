using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AdminPanelBackend.Data;
using AdminPanelBackend.Models;

namespace AdminPanelBackend.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(ApplicationDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Register(RegisterModel registerModel)
        {
            try
            {
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == registerModel.Username || u.EmailAddress == registerModel.EmailAddress);

                if (existingUser != null)
                {
                    _logger.LogWarning("Kullanıcı adı veya e-posta zaten kullanılıyor.");
                    throw new InvalidOperationException("Kullanıcı adı veya e-posta zaten kullanılıyor.");
                }

                var user = new User
                {
                    Username = registerModel.Username,
                    Password = registerModel.Password, 
                    EmailAddress = registerModel.EmailAddress
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Yeni kullanıcı kaydı başarıyla oluşturuldu.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı kaydı oluşturulurken bir hata oluştu.");
                throw;
            }
        }

        public async Task<bool> Login(LoginModel loginModel)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == loginModel.Username && u.Password == loginModel.Password);

                if (user != null)
                {
                    _logger.LogInformation("Giriş başarılı.");
                    return true;
                }

                _logger.LogWarning("Giriş başarısız: Kullanıcı adı veya şifre hatalı.");
                throw new InvalidOperationException("Kullanıcı adı veya şifre hatalı.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Giriş yapılırken bir hata oluştu.");
                throw;
            }
        }
    }
}
