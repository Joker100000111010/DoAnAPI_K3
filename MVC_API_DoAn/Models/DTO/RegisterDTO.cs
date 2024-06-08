using System.ComponentModel.DataAnnotations;

namespace MVC_API_DoAn.Models.DTO
{
    public class RegisterDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp.")]
        public string ConfirmPassword { get; set; }
        public string[] Roles { get; set; }
    }
}
