using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Lextatico.Domain.Security
{
    public class SigningConfiguration
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfiguration(string secretKey)
        {
            var secretKeyBytes = Encoding.ASCII.GetBytes(secretKey);
            Key = new SymmetricSecurityKey(secretKeyBytes);


            SigningCredentials = new SigningCredentials(
                Key, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
