using System.Security.Claims;

namespace ContentLoop.API.Services
{
    public static class UserContextHelper
    {
        public static string GetUserId(ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                   ?? throw new UnauthorizedAccessException("Utilisateur non authentifié.");
        }

        public static string GetUsername(ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value
                   ?? "Inconnu";
        }

        public static string GetUserRole(ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Role)?.Value
                   ?? "user";
        }
    }
}
