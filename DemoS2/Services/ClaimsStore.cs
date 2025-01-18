using System.Security.Claims;

namespace DemoS2.Services
{
    public static class ClaimsStore
    {
        public static List<Claim> GetAllClaims()
        {

            return new List<Claim>
            {
                new Claim("Create", "Create"),
                new Claim("Edit", "Edit"),
                new Claim("Delete", "Delete"),
            };
        }
    }
}
