using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Security.Entities;
using Security.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Security.Stores
{
    public class DeveloperStore :
        IUserStore<Developer>,
        IUserPasswordStore<Developer>,
        IUserEmailStore<Developer>
    {
        private readonly IDeveloperRepository _userRepository;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public DeveloperStore(IDeveloperRepository userRepository, IUserOperationClaimRepository userOperationClaimRepository)
        {
            _userRepository = userRepository;
            _userOperationClaimRepository = userOperationClaimRepository;
        }

        #region IUserStore Implementation
        public async Task<IdentityResult> CreateAsync(Developer user, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            Developer insertedDeveloper = await _userRepository.AddAsync(user);
            foreach(UserOperationClaim claim in user.UserOperationClaims)
            {
                claim.UserId = insertedDeveloper.Id;
                await _userOperationClaimRepository.AddAsync(claim);
            }
            
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Developer user, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            await _userRepository.DeleteAsync(user);

            return IdentityResult.Success;
        }

        public async Task<Developer> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (!int.TryParse(userId, out int parsedUserId))
                throw new ArgumentException(null, nameof(userId));

            Developer? user = await _userRepository.GetAsync(u => u.Id == parsedUserId);

#pragma warning disable CS8603 // Possible null reference return.
            return user;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<IdentityResult> UpdateAsync(Developer user, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentException(null, nameof(user));

            await _userRepository.UpdateAsync(user);

            return IdentityResult.Success;
        }

        public Task<string> GetUserIdAsync(Developer user, CancellationToken cancellationToken)
        {
            string userId = user.Id.ToString();
            return Task.FromResult(userId);
        }

        public async Task<Developer> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _userRepository.GetAsync(u => u.Email.ToUpper() == normalizedUserName);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public Task<string> GetNormalizedUserNameAsync(Developer user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email.ToUpper());
        }

        public Task<string> GetUserNameAsync(Developer user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task SetNormalizedUserNameAsync(Developer user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(Developer user, string userName, CancellationToken cancellationToken)
        {
            user.Email = userName;
            return Task.CompletedTask;
        }
        #endregion

        #region IUserPasswordStore Implementation
        public async Task SetPasswordHashAsync(Developer user, string passwordHash, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.PasswordHash = Convert.FromBase64String(passwordHash);
        }

        public Task<string> GetPasswordHashAsync(Developer user, CancellationToken cancellationToken)
        {
            string passwordHash = Convert.ToBase64String(user.PasswordHash);

            return Task.FromResult(passwordHash);
        }

        public Task<bool> HasPasswordAsync(Developer user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null && user.PasswordHash.Length > 0);
        } 
        #endregion

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #region IUserEmailStore Implementation
        public Task SetEmailAsync(Developer user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.CompletedTask;
        }

        public Task<string> GetEmailAsync(Developer user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(Developer user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Status);
        }

        public Task SetEmailConfirmedAsync(Developer user, bool confirmed, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<Developer> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _userRepository.GetAsync(u => u.Email.ToUpper() == normalizedEmail);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public Task<string> GetNormalizedEmailAsync(Developer user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email.ToUpper());
        }

        public Task SetNormalizedEmailAsync(Developer user, string normalizedEmail, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        #endregion
    }
}
