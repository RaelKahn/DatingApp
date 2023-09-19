using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class AccountController: BaseAPIDataContextController // BaseAPIController
    {
        public ITokenService _tokenService { get; }
        public AccountController(DataContext context, ITokenService tokenService):  base(context)
        {
            this._tokenService = tokenService;
            
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.UserName))
                return BadRequest(String.Format("User name {0} is taken",registerDto.UserName));
                

            using var hmac = new HMACSHA512(); // Used for the password salt

            var user = new AppUser
            {
                UserName = registerDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            Context.Users.Add(user);
            await Context.SaveChangesAsync();

            var myToken = _tokenService.CreateToken(user);
            return new UserDto()
            {
                UserName = user.UserName,
                Token = myToken //_tokenService.CreateToken(user)
            };
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await Context.Users.SingleOrDefaultAsync<AppUser>(x => 
                x.UserName.ToLower() == loginDto.UserName.ToLower());

                
            if(user==null)
                return Unauthorized("Invalid user name");

            using var hmac = new HMACSHA512(user.PasswordSalt); // Used for the password salt

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            // Compare the hash
            if(computedHash.IsEqual(user.PasswordHash))
                return Unauthorized("Invalid password");

            return new UserDto()
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }


        private async Task<bool> UserExists(string username)
        {
            return await Context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}