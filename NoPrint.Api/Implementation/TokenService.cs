using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using NoPrint.Application.Infra;
using NoPrint.Identity.Share;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using NoPrint.Users.Domain.Tools;
using System.Security.Cryptography;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace NoPrint.Api.Implementation;

public class TokenService : ITokenService
{
    public string Key { get; private set; }
    public string Issuer { get; private set; }
    public string Audience { get; private set; }
    public string EnKey { get; private set; }
    public int ExpireMin { get; private set; }


    public const string cg = "skjdSDFGHhfds&#$fhsvh6354";
    public const string te = "kdh#$d%asc3254ujs353sfsSH";
    public TokenService(IConfigurationGetter configuration)
    {
        Key = configuration.Getkey();
        Issuer = configuration.GetIssuer();
        Audience = configuration.GetAudience();
        EnKey = configuration.GetEnKey();
        ExpireMin = configuration.GetTokenExpireMin();
    }
    public TokenBehavior GenerateToken(UserId userId, Guid loginId, Rule rule)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var expireDate = DateTime.Now.AddMinutes(ExpireMin);


        var claimGroup = new ClaimGroup()
        {
            Key = loginId,
            Rule = rule,
            UserId = userId
        };

        var secToken = new JwtSecurityToken(
            signingCredentials: credentials,
            issuer: Issuer,
            audience: Audience,
            claims: new[]
            {
                new Claim("key",Encrypt(claimGroup.ToString(),cg) )
            },
            expires: expireDate);

        var handler = new JwtSecurityTokenHandler();

        string token = handler.WriteToken(secToken);
        token = Encrypt(token, te);
        return new TokenBehavior(token, expireDate);
    }
    public UserId ValidateToken(string token, out Rule rule, out Guid loginId)
    {
        token = Decrypt(token, te);

        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = GetValidationParameters();

        SecurityToken validatedToken;
        IPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
        var claims = (principal.Identity as ClaimsIdentity).Claims;

        string str_key = claims.Single(x => x.Type.Equals("key")).Value;

        str_key = Decrypt(str_key, cg);

        var claimGroup = ClaimGroup.Parse(str_key);

        rule = claimGroup.Rule;

        loginId = claimGroup.Key;

        return claimGroup.UserId;

    }
    private class ClaimGroup
    {
        public UserId UserId { get; set; }
        public Guid Key { get; set; }
        public Rule Rule { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public static ClaimGroup Parse(string value) => JsonSerializer.Deserialize<ClaimGroup>(value);
    }
    private TokenValidationParameters GetValidationParameters()
    {
        return new TokenValidationParameters()
        {
            ValidateLifetime = true, // Because there is no expiration in the generated token
            ValidateAudience = false, // Because there is no audiance in the generated token
            ValidateIssuer = false,   // Because there is no issuer in the generated token
            ValidIssuer = Issuer,
            ValidAudience = Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key)) // The same key as the one that generate the token
        };
    }
    private string Encrypt(string encryptString, string additionalKey)
    {
        string EncryptionKey = additionalKey + EnKey;
        byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
            });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                encryptString = Convert.ToBase64String(ms.ToArray());
            }
        }
        return encryptString;
    }
    private string Decrypt(string cipherText, string additionalKey)
    {
        string EncryptionKey = additionalKey + EnKey;
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
            });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
}

