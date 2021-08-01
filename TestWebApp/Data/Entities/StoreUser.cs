using Microsoft.AspNetCore.Identity;

namespace MyWebApp.Data.Entities
{
    public class StoreUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}