
namespace GameStoreAPI.Models
{
    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

//     public class SignupRequest
//     {
//         public required string Name { get; set; }
//         public required string Email { get; set; }
//         public required string Password { get; set; }
//     }
// }
