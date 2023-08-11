namespace TekusWebAPI.Models.auth
{
    public class CreateTokenInputDto
    {
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
