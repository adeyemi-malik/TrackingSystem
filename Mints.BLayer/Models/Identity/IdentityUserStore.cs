using Mints.BLayer.Repositories;
using Mints.DLayer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Mints.BLayer.Helpers;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Mints.BLayer.Models.Identity
{
    public class IdentityUserStore : IUserStore<User>, IUserRoleStore<User>, IQueryableUserStore<User>, IUserPhoneNumberStore<User>, IUserPasswordStore<User>, IUserEmailStore<User>
    {
        private readonly Context _dbContext;


        public IdentityUserStore(Context context)
        {
            _dbContext = context;
        }

        public IQueryable<User> Users => _dbContext.Users
            .Select(u => u.ToUser());

        public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (role != null)
            {
                _dbContext.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = role.Id });
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = user.ToDbUser();
            entity.DateCreated = DateTime.Now;
            entity.DateUpdated = DateTime.Now;
            _dbContext.Users.Add(entity);
            await _dbContext.SaveChangesAsync();
            user.Id = entity.Id;
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = user.ToDbUser();
            _dbContext.Entry(entity).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }



        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await _dbContext.Users
                 .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Email.Equals(normalizedEmail, StringComparison.OrdinalIgnoreCase));

            return user?.ToUser();
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await _dbContext.Users
                .Include(u => u.UserRoles)
               .FirstOrDefaultAsync(u => u.Id == (int)Convert.ChangeType(userId, typeof(int)));
            return user?.ToUser();
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await _dbContext.Users
                 .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.UserName.Equals(normalizedUserName, StringComparison.OrdinalIgnoreCase));

            return user?.ToUser();
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(true);
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.Email.ToLower());
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.UserName.ToLower());
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetPhoneNumberAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(true);
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var roles = await _dbContext.UserRoles
                 .Include(u => u.Role)
                 .Include(u => u.User)
                 .Where(u => u.UserId == user.Id)
                 .Select(r => r.Role.Name).ToListAsync();
            return roles;
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var Id = user.Id.ToString();
            return Task.FromResult(Id);
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.UserName);
        }

        public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var users = await _dbContext.UserRoles
                 .Include(u => u.User)
                .Include(u => u.Role)
                .Where(u => u.Role.Name == roleName)
                .Select(r => r.User.ToUser()).ToListAsync();
            return users;
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _dbContext.UserRoles
                 .Include(u => u.User)
                 .Include(u => u.Role)
                 .Where(u => u.Role.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase) && u.UserId == user.Id)
                 .AnyAsync();
        }

        public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var userRoles = _dbContext.UserRoles
                            .Include(u => u.User)
                            .Include(u => u.Role)
                            .Where(u => u.Role.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase) && u.UserId == user.Id);
            _dbContext.UserRoles.RemoveRange(userRoles);
            await _dbContext.SaveChangesAsync();
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.Email = email;
            return Task.FromResult(0);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(0);
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(0);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(0);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberAsync(User user, string phoneNumber, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            user.UserName = userName;
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await _dbContext.Users.FindAsync(user.Id);
            if (entity == null)
            {
                return null;
            }
            entity.Email = user.Email;
            entity.Phone = user.PhoneNumber;
            entity.DateUpdated = DateTime.Now;
            entity.UserName = user.UserName;
            entity.PasswordHash = user.PasswordHash;
            entity.HashSalt = user.HashSalt;
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return IdentityResult.Success;
        }
    }
}
