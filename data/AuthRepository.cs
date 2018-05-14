using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.data
{
    public class AuthRepository : IAuthRepository
    {
       
        public AuthRepository(dataContext context)
        {
           
           _Context = context;
        }

        public dataContext _Context { get; }

        public async Task<Users> Login(string username, string password)
        {
            var user = await _Context.Users.FirstOrDefaultAsync(x => x.Username== username);

            if (user == null)
            return null;
            if (!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
            return null;

            // auth succesful 
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
           using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)){
              var computedHash= hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
              for (int i = 0; i < computedHash.Length; i++)
              {
                  if (computedHash[i] != passwordHash[i] ) 
                  return false;
              }
             
          }
          return true;
        }

        public async Task<Users> Register(Users user, string password)
        {

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _Context.Users.AddAsync(user);
            await _Context.SaveChangesAsync();

            return user;

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
          using (var hmac = new System.Security.Cryptography.HMACSHA512()){
              passwordSalt = hmac.Key;
              passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
          }
        }

        public async Task<bool> UserExists(string username)
        {
           if (await _Context.Users.AnyAsync(x => x.Username == username))
           return true;

           return false;
        }
    }
}