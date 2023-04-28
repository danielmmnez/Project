using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Project.WEB.Auth
{
    public class AuthenticationProviderTest : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(3000);
            var anonimous = new ClaimsIdentity();
            var dmmnzUser = new ClaimsIdentity(new List<Claim>
        {
            new Claim("FirstName", "D"),
            new Claim("LastName", "M"),
            new Claim(ClaimTypes.Name, "dm@gmail.com"),
            new Claim(ClaimTypes.Role, "Admin")

        },
                authenticationType: "test");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(dmmnzUser)));
        }
    }
}
