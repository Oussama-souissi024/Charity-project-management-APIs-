using CityProjects.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                {
                    return BadRequest(new { message = "Les mots de passe ne correspondent pas." });
                }

                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok(new { message = "Enregistrement réussi." });
                }

                return BadRequest(result.Errors);
            }

            return BadRequest(ModelState);
        }




        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    var roles = await _userManager.GetRolesAsync(user);

                    // L'authentification JWT se fera automatiquement via les configurations
                    return Ok(new
                    {
                        token = GenerateJwtToken(user, roles),
                        roles = roles
                    });
                }
                if (result.IsLockedOut)
                {
                    return Unauthorized("Le compte est verrouillé.");
                }
                else
                {
                    return Unauthorized("Échec de la connexion.");
                }
            }

            return BadRequest(ModelState);
        }

        private object GenerateJwtToken(IdentityUser user, IList<string> roles)
        {
            // Création des claims pour l'utilisateur
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim("typ", "Bearer")
    }.Union(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

            // Récupération de la clé de configuration
            var key = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key), "JWT key cannot be null or empty");
            }

            // Création de la clé symétrique et des informations de signature
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            // Création du token JWT
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            // Génération du refresh token
            var refreshToken = Guid.NewGuid().ToString();

            // Retour du token et des informations supplémentaires
            return new
            {
                TokenType = "Bearer",
                AccessToken = jwtToken,
                ExpiresIn = 3600, // durée en secondes
                RefreshToken = refreshToken,
                Roles = roles
            };
        }


    }
}

