using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mints.BLayer.Helpers;
using Mints.DLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Mints.BLayer.Models.Identity
{
    public class IdentityRoleStore : IRoleStore<Role>, IQueryableRoleStore<Role>
    {
        private readonly Context _dbContext;

        public IdentityRoleStore(Context context)
        {
            _dbContext = context;
        }

        public IQueryable<Role> Roles => _dbContext.Roles
            .Include(r => r.UserRoles)
            .Select(r => r.ToRole());

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = role.ToDbRole();
            entity.DateCreated = DateTime.Now;
            entity.DateUpdated = DateTime.Now;
            _dbContext.Entry(entity).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
            role.Id = entity.Id;
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = role.ToDbRole();
            _dbContext.Entry(entity).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var role = await _dbContext.Roles
                .Include(u => u.UserRoles)
               .FirstOrDefaultAsync(r => r.Id == (int)Convert.ChangeType(roleId, typeof(int)));
            
            return role?.ToRole();
        }

        public async Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var role = await _dbContext.Roles
                .Include(u => u.UserRoles)
               .FirstOrDefaultAsync(r => r.Name.Equals(normalizedRoleName, StringComparison.OrdinalIgnoreCase));

            return role?.ToRole();
        }

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(role.Name.ToLower());
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var Id = role.Id.ToString();
            return Task.FromResult(Id);
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(0);
        }

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            role.Name = roleName;
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await _dbContext.Roles.FindAsync(role.Id);
            if (entity == null)
            {
                return null;
            }
            entity.Name = role.Name;
            entity.DateUpdated = DateTime.Now;
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return IdentityResult.Success;
        }
    }
}
