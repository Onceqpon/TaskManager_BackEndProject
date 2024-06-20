namespace Web.Models.ViewModels
{
    public class RegisterViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "user"; // Domyślna rola to "user"
    }
}
