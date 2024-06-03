
namespace TaskManagement.Core.Application.Dtos.Accounts
{
    public class RegisterResponse
    {
        public string? IdUser { get; set; }
        public bool HasError { get; set; }
        public List<string>? Error { get; set; }
    }
}
