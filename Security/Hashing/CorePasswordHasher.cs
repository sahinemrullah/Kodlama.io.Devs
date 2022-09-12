using Core.Security.Hashing;
using Microsoft.AspNetCore.Identity;
using Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Hashing
{
    public class CorePasswordHasher : IPasswordHasher<Developer>
    {
        public string HashPassword(Developer user, string password)
        {
            HashingHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            
            user.PasswordSalt = passwordSalt;

            return Convert.ToBase64String(passwordHash);
        }

        public PasswordVerificationResult VerifyHashedPassword(Developer user, string hashedPassword, string providedPassword)
        {
            if(HashingHelper.VerifyPasswordHash(providedPassword, user.PasswordHash, user.PasswordSalt))
                return PasswordVerificationResult.Success;
            return PasswordVerificationResult.Failed;
        }
    }
}
