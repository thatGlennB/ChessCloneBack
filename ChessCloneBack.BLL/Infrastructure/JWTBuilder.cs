using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChessCloneBack.BLL.Infrastructure
{
    public class JWTBuilder
    {
        private List<Claim> _claims;
        public string Key { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public DateTime Expiration { get; set; }
        public string SecurityAlgorithm { get; set; }
        public JWTBuilder()
        {
            _claims = new List<Claim>();
            SecurityAlgorithm = SecurityAlgorithms.HmacSha256;
        }
        public JWTBuilder(string key, string issuer, string audience, DateTime expiration) : this()
        {
            Key = key;
            Issuer = issuer;
            Audience = audience;
            Expiration = expiration;
        }
        public JWTBuilder(string key, string issuer, string audience, DateTime expiration, List<Claim> claims) : this(key, issuer, audience, expiration)
        {
            _claims = claims;
        }
        public void AddClaim(string claimType, string claimValue, bool overrideExistingClaim = false)
        {
            Claim? existingClaim = _claims.SingleOrDefault(o => o.Type == claimType);
            if (existingClaim != null)
            {
                if (!overrideExistingClaim)
                {
                    return;
                }
                else
                {
                    _claims.Remove(existingClaim);
                }
            }
            _claims.Add(new Claim(claimType, claimValue));
        }
        public string Build()
        {
            if (Key == null || Issuer == null || Audience == null)
            {
                throw new NullReferenceException("Cannot generate JwtSecurity token: builder has not been provided with values for some combination of Key, Issuer and Audience");
            }
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithm);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: _claims.IsNullOrEmpty() ? null : _claims,
                expires: Expiration,
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
