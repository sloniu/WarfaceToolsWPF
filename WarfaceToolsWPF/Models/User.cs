namespace WarfaceToolsWPF.Models
{
    /// <summary>
    /// Singleton design user class.
    /// There can be only one logged in user, therefore using singleton design.
    /// </summary>
    public sealed class User
    {
        private User() { }
        private static User _instance;

        public static User Instance => _instance ?? (_instance = new User());

        public string Username { get; set; } = "sloniu"; // domyślne wartości dodane w celu szybszego testowania aplikacji
        public string Password { get; set; } = "haslo";
        public bool IsLoggedIn { get; set; }
    }
}
