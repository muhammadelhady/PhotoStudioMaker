using Microsoft.EntityFrameworkCore;
using PhotoMakerStudio.Model;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMakerStudio.Data
{
    
    public class AuthReository : IAuthrRpository
    {
        private readonly DataContext _context;

        public AuthReository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> Login(string userName, string Password)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Name == userName);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(Password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
              
               var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
              for (int i =0; i< computedHash.Length;i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false; 
                }
            }

            return true;
        }

        public async Task<User> Resgister(User user, string Password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            CreatPasswordHash(Password, out passwordHash, out passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private void CreatPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserIsExists(string userName)
        {
            if (await _context.User.AnyAsync(x => x.Name == userName))
                return true;
            return false;
        }
    }
}
