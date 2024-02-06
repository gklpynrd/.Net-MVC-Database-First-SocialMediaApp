using System;

namespace SocialMediaApp.App_Classes
{
    public class User
    {
        public Nullable<Guid> UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string SecretQuestion { get; set; }
        public string SecretAnswer { get; set; }
    }
}