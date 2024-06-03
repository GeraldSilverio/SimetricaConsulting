using System.Text.Json.Serialization;

namespace TaskManagement.Core.Application.Dtos.Accounts
{
    public class AuthenticationResponse
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public List<string>? Roles { get; set; }
        public bool IsActive { get; set; }
        public bool HasError { get; set; }
        public List<string>? Error { get; set; }
        public string? JwToken { get; set; }
        [JsonIgnore]
        public string? RefreshToken { get; set; }
    }
}
