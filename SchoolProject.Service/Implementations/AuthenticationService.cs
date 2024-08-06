using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrustucture.Abstracts;
using SchoolProject.Service.Abstracts;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolProject.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        private readonly ConcurrentDictionary<String, RefreshToken> _userRefreshToken;
        private readonly IRefreshTokenRepository _refreshTokenRepository;


        #endregion
        #region Constructor
        public AuthenticationService(JwtSettings jwtSettings,
                                     IRefreshTokenRepository refreshTokenRepository)
        {
            _jwtSettings = jwtSettings;
            _userRefreshToken = new ConcurrentDictionary<String, RefreshToken>();
            _refreshTokenRepository = refreshTokenRepository;

        }


        #endregion
        #region Handel Functions
        public async Task<JwtAuthResult> GetJWTToken(User user)
        {
            var claims = GetClaims(user);
            var jwtToken = new JwtSecurityToken(
               _jwtSettings.Issuer,
               _jwtSettings.Audience,
               claims,
               expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
               signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var refreshToken = GetRefreshToken(user.UserName);
            var userRefreshToken = new UserRefreshToken
            {

                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                IsUsed = true,
                IsRevoked = false,
                JwtId = jwtToken.Id,
                RefreshToken = refreshToken.TokenString,
                Token = accessToken,
                UserId = user.Id
            };
            await _refreshTokenRepository.AddAsync(userRefreshToken);
            var response = new JwtAuthResult();
            response.refreshToken = refreshToken;
            response.AccessToken = accessToken;
            return response;

        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private RefreshToken GetRefreshToken(string username)
        {
            var refreshToken = new RefreshToken
            {
                ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                UserName = username,
                TokenString = GenerateRefreshToken()
            };
            return refreshToken;
        }
        public List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimModel.UserName),user.UserName),
                new Claim(nameof(UserClaimModel.Email),user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber),user.PhoneNumber),

            };
            return claims;
        }
        #endregion
    }
}

