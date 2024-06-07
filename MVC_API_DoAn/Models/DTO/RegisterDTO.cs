using System.ComponentModel.DataAnnotations;

namespace MVC_API_DoAn.Models.DTO
{
    public class RegisterDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string[] Roles { get; set; }
    }
}
