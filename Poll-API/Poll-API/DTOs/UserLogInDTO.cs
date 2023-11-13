using System.Text.Json.Serialization;

namespace Poll_API.DTOs
{
    public class UserLoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
