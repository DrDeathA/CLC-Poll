using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Poll_API.Data;
using Poll_API.Data.Entities;
using Poll_API.DTOs;
using Poll_API.Interfaces;
using System.Text;

namespace Poll_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;
        public UserController(ITokenService tokenService, IConfiguration config)
        {
            _tokenService = tokenService;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(UserDTO user)
        {
            using var db = new DataContext();

            try
            {
                await db.User.AddAsync(new User(user));

                if (await db.SaveChangesAsync() > 0)
                {
                    return Ok("User Added Successfully");
                }
                else
                {
                    return BadRequest("Failed To Save User");
                }
            }
            catch (Exception)
            {
                return BadRequest("Email Already In Use");
            }
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDTO creds)
        {
            using var db = new DataContext();
            var dbUser = await db.User.SingleOrDefaultAsync(x => x.Email == creds.Email);

            if (dbUser == null) return BadRequest("Invalid Credentials");
            if (dbUser.ConsecutiveLoginAttempts >= 3) return BadRequest("Account Blocked, Please Reset Your Password");

            var validPassword = dbUser.ValidPassword(creds.Password);


            if (validPassword)
            {
                dbUser.PasswordResetToken = null;
                dbUser.ForgotPassword = false;
                await SetLoginAttempts(db, dbUser, 0);
                return Ok(_tokenService.CreateToken(dbUser));
            }
            else
            {
                await SetLoginAttempts(db, dbUser);
                return BadRequest("Invalid Credentials");
            }
        }

        [HttpGet("Reset/{email}")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            using var db = new DataContext();
            var dbUser = await db.User.SingleOrDefaultAsync(x => x.Email == email);

            if (dbUser != null)
            {
                var validToken = GenerateRandomToken();

                dbUser.PasswordResetToken = validToken;
                dbUser.ForgotPassword = true;

                await db.SaveChangesAsync();
            }

            //Return even if no such user exsists, aind to prevent brute force
            return Ok("Reset Token Sent To Email Address");
        }


        [HttpPost("Reset/{token}")]
        public async Task<IActionResult> PasswordReset(string token, UserLoginDTO user)
        {
            using var db = new DataContext();
            var dbUser = await db.User.SingleOrDefaultAsync(x => x.PasswordResetToken == token && x.Email == user.Email);

            if (dbUser == null) return BadRequest("Token Not Found");

            dbUser.HashPassword(user.Password);

            dbUser.PasswordResetToken = null;
            dbUser.ForgotPassword = false;
            await SetLoginAttempts(db, dbUser, 0);
            return Ok("Email Sent With Reset Link");
        }

        private string GenerateRandomToken()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new StringBuilder(8);
            for (int i = 0; i < 8; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }
            return result.ToString();
        }

        private async Task SetLoginAttempts(DataContext db, User user, int value = 1)
        {
            if (value > 0)
            {
                user.ConsecutiveLoginAttempts++;
            }
            else
            {
                user.ConsecutiveLoginAttempts = 0;
            }

            await db.SaveChangesAsync();
        }
    }
}
