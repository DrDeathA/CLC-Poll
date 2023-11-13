using Microsoft.EntityFrameworkCore;
using Poll_API.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace Poll_API.Data.Entities
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int ConsecutiveLoginAttempts { get; set; }
        public bool ForgotPassword { get; set; }
        public string? PasswordResetToken { get; set; }
        [InverseProperty("User")]
        public ICollection<Poll> Polls { get; set; }

        public User()
        {
        }

        public User(UserDTO user)
        {
            UserId = Guid.NewGuid();
            Name = user.Name;
            Surname = user.Surname;
            Email = user.Email;
            PasswordResetToken = null;
            HashPassword(user.Password);
        }

        public void HashPassword(string password)
        {
            using var hmac = new HMACSHA512();
            this.PasswordSalt = hmac.Key;
            this.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(this.PasswordSalt + password + this.PasswordSalt));
        }

        public bool ValidPassword(string password)
        {
            using var hmac = new HMACSHA512(this.PasswordSalt);

            var newHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(this.PasswordSalt + password + this.PasswordSalt));

            if (this.PasswordHash.Length != newHash.Length)
            {
                return false;
            }

            for (int i = 0; i < newHash.Length; i++)
            {
                if (newHash[i] != this.PasswordHash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
