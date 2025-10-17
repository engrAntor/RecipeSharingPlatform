using BLL.DTOs; 

namespace BLL.Interfaces
{
    
    public interface IAuthService
    {
        
        
        
        bool Register(UserRegisterDTO registerData);

        bool VerifyAccount(string token);

       
        string Login(UserLoginDTO loginData);

        bool RequestPasswordReset(string email);

        bool ResetPassword(PasswordResetDTO resetData);
    }
}