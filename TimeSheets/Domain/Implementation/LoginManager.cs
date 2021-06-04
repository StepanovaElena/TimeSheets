using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Timesheets.Models.Dto;
using Timesheets.Models.Dto.Authentication;
using TimeSheets.Data.Interfaces;
using TimeSheets.Domain.Interfaces;
using TimeSheets.Infrastructure.Extensions;
using TimeSheets.Models;
using TimeSheets.Models.Dto.Requests;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TimeSheets.Domain.Implementation
{
    public class LoginManager : ILoginManager
    {
        private readonly JwtAccessOptions _jwtAccessOptions;
        private readonly IUserRepo _userRepo;

        public LoginManager(
            IOptions<JwtAccessOptions> jwtAccessOptions,
            IUserRepo userRepo)
        {
            _jwtAccessOptions = jwtAccessOptions.Value;
            _userRepo = userRepo;
        }

        public async Task<LoginResponse> Authenticate(User user)
        {
            (var token, var expiresIn) = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken(user);

            var loginResponse = new LoginResponse()
            {
                AccessToken = token,
                ExpiresIn = expiresIn,
                RefreshToken = refreshToken.Token
            };

            // сохраняем значение токена
            user.RefreshTokens.Add(refreshToken);
            await _userRepo.Update(user);

            return loginResponse;
        }

        public async Task<LoginResponse> RefreshToken(RefreshTokenRequest request)
        {
            var user = await _userRepo.GetUserByToken(request.Token);
            if (user == null) return null;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == request.Token);
            if (!refreshToken.IsActive) return null;

            var newRefreshToken = GenerateRefreshToken(user);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.ReplacedByToken = newRefreshToken.Token;

            user.RefreshTokens.Add(newRefreshToken);
            await _userRepo.Update(user);

            (var token, var expiresIn) = GenerateJwtToken(user);

            var loginResponse = new LoginResponse()
            {
                AccessToken = token,
                ExpiresIn = expiresIn,
                RefreshToken = newRefreshToken.Token
            };
            
            return loginResponse;
        }

        public async Task<bool> RevokeToken(RevokeTokenRequest request)
        {
            var user = await _userRepo.GetUserByToken(request.Token);
            if (user == null) return false;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == request.Token);
            if (!refreshToken.IsActive) return false;
                        
            refreshToken.Revoked = DateTime.UtcNow;
            await _userRepo.Update(user);

            return true;
        }

        private (string Token, long ExpiresIn) GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };

            var accessTokenRaw = _jwtAccessOptions.GenerateToken(claims);
            var securityHandler = new JwtSecurityTokenHandler();
            var accessToken = securityHandler.WriteToken(accessTokenRaw);
            var expiresIn = accessTokenRaw.ValidTo.ToEpochTime();

            return (accessToken, expiresIn);
        }

        private RefreshToken GenerateRefreshToken(User user)
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedBy = user.Id
            };
        }
    }
}