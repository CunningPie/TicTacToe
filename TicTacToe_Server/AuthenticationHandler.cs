using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace TicTacToe_Server
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserRepository _userRepository;
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock, IUserRepository userRepository) :
           base(options, logger, encoder, clock)
        {
            _userRepository = userRepository;
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                return await Task.FromResult(AuthenticateResult.NoResult());
            }

            var authorizationHeader = Request.Headers["Authorization"].ToString();
            if (authorizationHeader != null && authorizationHeader.StartsWith("Basic", StringComparison.OrdinalIgnoreCase))
            {
                var token = authorizationHeader.Substring("Basic ".Length).Trim();
                var credentialsAsEncodedString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var credentials = credentialsAsEncodedString.Split(':');
                if (await _userRepository.Authenticate(credentials[0], credentials[1]) != Guid.Empty)
                {
                    var claims = new[] { new Claim("name", credentials[0]), new Claim(ClaimTypes.Role, "Admin") };
                    var identity = new ClaimsIdentity(claims, "Basic");
                    var claimsPrincipal = new ClaimsPrincipal(identity);
                    return await Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
                }
            }
            Response.StatusCode = 401;
            Response.Headers.Add("WWW-Authenticate", "Basic realm=\"TicTacToe\"");
            return await Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
        }
    }
}
