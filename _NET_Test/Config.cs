using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

public static class Config
{
    public static IConfiguration configuration { get; set; } = null!;
    public static class JWT 
    {
        public static SymmetricSecurityKey GetSecretKey() => new (Encoding.UTF8.GetBytes(configuration.GetSection("AuthOptions")["Secret"]!));
        public static string Issuer = configuration.GetSection("AuthOptions")["Issuer"]!;
        public static string Audience = configuration.GetSection("AuthOptions")["Audience"]!;
        public static TokenValidationParameters validationParameters { get; set; } = new()
        {
            ValidIssuer = Issuer,
            ValidateAudience = true,
            ValidAudience = Audience,
            ValidateLifetime = true,
            IssuerSigningKey = GetSecretKey(),
            ValidateIssuerSigningKey = true,
        };
        public static string Decode(string jwt)
        {
            JwtSecurityTokenHandler handler = new ();
            JwtSecurityToken jwtSecurityToken = handler.ReadJwtToken(jwt);
            return jwtSecurityToken.ToString();
        }
    }
}
