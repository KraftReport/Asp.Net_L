using Microsoft.IdentityModel.Tokens; 
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq; 
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text; 
using System.Threading.Tasks;
using TimeZoneConverter;

namespace TokenDemo.Token
{
    public class TokenService
    {
        private readonly string SECRET = "thisisjwtsymmetrickeyforjwttokengeneration";
        private readonly TokenRepository tokenRepository;
        public TokenService(TokenRepository tokenRepository)
        {
            this.tokenRepository = tokenRepository; 
        }

        public string GenerateAccessToken()
        {
            var keyByte = Encoding.UTF8.GetBytes(SECRET);
            var key = new SymmetricSecurityKey(keyByte);
            var sigingingMethod = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var claims = new[] 
            {
                new Claim("table-id","TBL6657372"),
                new Claim("role","admin")
            };
            var myanmarDateTime = changeMyanmarTime(DateTime.UtcNow);

            Console.WriteLine(myanmarDateTime);

            var token = new JwtSecurityToken(
            issuer:"myosetpaing",
            audience:"myosetpaing",
            claims: claims,
            expires: myanmarDateTime.AddMinutes(3),
            signingCredentials: sigingingMethod
            );  
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateJwtRefreshToken()
        {
            var keyByte = Encoding.UTF8.GetBytes(SECRET);
            var key = new SymmetricSecurityKey(keyByte);
            var sigingingMethod = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var myanmarTime = changeMyanmarTime(DateTime.UtcNow);
            var token = new JwtSecurityToken(
                expires : myanmarTime.AddDays(1),
                signingCredentials : sigingingMethod
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string ValidateRefreshToken(string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            if(tokenHandler.ReadToken(refreshToken) == null)
            {
                return "this is not jwt token ";
            }

            var jwt = tokenHandler.ReadToken(refreshToken);

            if (!ValidateTokenExpirationTime(refreshToken))
            {
                return "refresh token is expired";
            }
            
            if(!ValidateSymmetricKey(refreshToken))
            {
                return "not a valid refresh token generated from server";
            } 

            return $"access token -> {GenerateAccessToken()}  refresh token -> {GenerateJwtRefreshToken()}";
        }

        private bool ValidateSymmetricKey(string token)
        {
            var tokenPart = token.Split('.');
            var headerAndPayload = $"{tokenPart[0]}.{tokenPart[1]}";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET));
            using(var hmac = new HMACSHA256(securityKey.Key))
            {
                var computedValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(headerAndPayload));
                if(Base64UrlEncoder.Encode(computedValue) != tokenPart[2])
                {
                    return false;
                } 
                return true;
            }
        }
         
        public bool ValidateTokenExpirationTime(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var expiration = changeMyanmarTime(jwtToken.ValidTo);
            var currentTime = changeMyanmarTime(DateTime.UtcNow);
            if(currentTime > expiration)
            {
                //return "token is expired";
                return false;
            }
            var remainingTime = currentTime - expiration;
            // return $"token is still valid {remainingTime.TotalSeconds} is left before expired";
            return true;
        }

        public bool ValidateRefreshTokenExpirationTime(RefreshToken refreshToken)
        {
            var currentTime = changeMyanmarTime(DateTime.UtcNow);
            var expiartion = changeMyanmarTime(refreshToken.expirationTime);
            if(currentTime > expiartion)
            {
                return false;
            }
            return true;
        }

        public Dictionary<string,string> GetClaimsFromAccessToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            return jwtToken.Claims.ToDictionary(j=>j.Type,j=>j.Value);
        }

        public async Task<string> GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using(var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(randomNumber);
            }
            
            var token = Convert.ToBase64String(randomNumber);
            var refreshToken = new RefreshToken
            {
                code = token,
                expirationTime = changeMyanmarTime(DateTime.UtcNow).AddMinutes(5)
            };

            await tokenRepository.SaveRefreshToken(refreshToken);

            return token;

        }

        public  string GenerateRefreshTokenCode()
        {
            var randomNumber = new byte[64];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }

        public async Task<RefreshTokenProcessResponseModel> RefreshTokenProcess(string token)
        {
            var foundToken = await tokenRepository.FindRefreshToken(token);


            if (foundToken != null)
            {
                var validation = ValidateRefreshTokenExpirationTime(foundToken);
                if (validation)
                {
                    var newCode = GenerateRefreshTokenCode();
                    var newRefreshToken = new RefreshToken
                    {
                        id = foundToken.id,
                        code = newCode,
                        expirationTime = changeMyanmarTime(DateTime.UtcNow).AddMinutes(5)
                    };

                    await tokenRepository.UpdateRefreshToken(newRefreshToken);
                    var newAccessToken = GenerateAccessToken();
                    return new RefreshTokenProcessResponseModel
                    {
                        NewAccessToken = newAccessToken,
                        NewRefreshToken = newCode
                    }; 
                }

                throw new Exception("refresh token is expired");
            }

            throw new Exception("invalid refresh token");
        }

        

        private DateTime changeMyanmarTime(DateTime time)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(time, TZConvert.GetTimeZoneInfo("Asia/Yangon"));
        }
    }
}
