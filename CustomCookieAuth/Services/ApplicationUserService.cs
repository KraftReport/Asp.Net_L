using CustomCookieAuth.Entities;
using CustomCookieAuth.Models;
using CustomCookieAuth.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CustomCookieAuth.Services
{
    public class ApplicationUserService
    {
        private readonly ApplicationUserRepository _applicationUserRepository;
        public ApplicationUserService(ApplicationUserRepository applicationUserRepository)
        {
            _applicationUserRepository = applicationUserRepository;
        }

        public async Task<bool> RegisterApplicationUser(RegisterDTO dto)
        {
            var (hash,salt) = PasswordHashingService.HashPassword(dto.Password);
            var applicationUser = new ApplicationUser
            {
                Name = dto.Email,
                Email = dto.Email,
                Password = hash,
                Salt = salt,
                Role = ROLE.MEMBER
            };
            await _applicationUserRepository.createUser(applicationUser);
            return true;
        }

        public async Task<(UserClaims,CookieOptions)> LoginUser([FromBody] LoginDTO loginDTO)
        {
            var user = await _applicationUserRepository.findByEmail(loginDTO.Email);
            var claims = new UserClaims
            {
                Email = user.Email,
                Role = user.Role
            };
            var cookie = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddSeconds(100)
            };
            return (claims, cookie);
        }

        public async Task<bool> ValidateUser(LoginDTO loginDTO)
        {
            var user = await _applicationUserRepository.findByEmail(loginDTO.Email);

            if (user == null && !PasswordHashingService.VerifyPassword(loginDTO.Password, user.Password, user.Salt))
                return false;
            return true;
        }

        public async Task<bool> UpdateUser(ApplicationUser user)
        {
              await _applicationUserRepository.updateUser(user);
            return true;
        }

        public async Task<bool> DeleteUser(int Id)
        {
            await _applicationUserRepository.deleteUser(Id);
            return true;
        } 

        public async Task<ApplicationUser> FindById(int Id)
        {
            return await _applicationUserRepository.findById(Id);
        }

        public async Task<List<ApplicationUser>> FindAll()
        {
            return await _applicationUserRepository.findAll();
        }
    }
}
