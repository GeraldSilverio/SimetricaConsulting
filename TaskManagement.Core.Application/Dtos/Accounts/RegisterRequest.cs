using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace TaskManagement.Core.Application.Dtos.Accounts
{
    public class RegisterRequest
    {
        [SwaggerParameter(Description = "El nombre de la persona a quien se le creara la cuenta")]
        public string FirstName { get; set; } = null!;

        [SwaggerParameter(Description = "El apellido de la persona a quien se le creara la cuenta")]
        public string LastName { get; set; } = null!;

        [SwaggerParameter(Description = "El nombre de usuario de la persona a quien se le creara la cuenta")]
        public string UserName { get; set; } = null!;

        [SwaggerParameter(Description = "El correo electronico de la persona a quien se le creara la cuenta")]
        public string Email { get; set; } = null!;

        [SwaggerParameter(Description = "La contraseña de la persona a quien se le creara la cuenta")]
        public string Password { get; set; } = null!;

        [SwaggerParameter(Description = "La contraseña de la persona a quien se le creara la cuenta")]
        public string ConfirmPassword { get; set; } = null!;

        [JsonIgnore] public bool IsActive { get; set; }
        [JsonIgnore] public int SelectRole { get; set; }
    }
}