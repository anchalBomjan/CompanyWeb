using System.Security.Claims;

namespace WebApp.API.Extensions
{
    public  static class ClaimsPrincipleExtensions
    {

        public static string GetUsername(this ClaimsPrincipal user)
        {
            var usernameClaim = user.FindFirst(ClaimTypes.Name);
            if (usernameClaim == null)
            {
                // Log that the claim wasn't found
                Console.WriteLine("ClaimTypes.Name not found in token.");
                return null;
            }
            return usernameClaim.Value;
        }
    }
}
