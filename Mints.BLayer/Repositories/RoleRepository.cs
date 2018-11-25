using Mints.BLayer.Models.Identity;
using Mints.DLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Mints.BLayer.Helpers;
using System.Threading.Tasks;
using BRole = Mints.BLayer.Models.Identity.Role;
using DRole = Mints.DLayer.Models.Role;
using Mints.BLayer.Exceptions;

namespace Mints.BLayer.Repositories
{
    public class RoleRepository : GenericRepository<DRole>, IRoleRepository
    {
        public RoleRepository(Context dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task CreateAsync(BRole role)
        {
            var dRole = role.ToDbRole();
            await InsertAsync(dRole);
            role.Id = dRole.Id;
        }

        public async Task DeleteAsync(BRole role)
        {
            await DeleteAsync(role.Id);
        }

        public async Task<BRole> FindByIdAsync(int roleId)
        {
            var role = await GetAsync(roleId);
            if (role == null)
            {
                throw new RoleReferenceNotFoundException("The role id does not exist");
            }
            return role.ToRole();
        }

        public Task<BRole> FindByNameAsync(string roleName)
        {
            var role =  Query().FirstOrDefault(s => s.Name == roleName);
           if (role == null)
            {
                throw new RoleReferenceNotFoundException("The role name does not exist");
            }
            var bRole = role.ToRole();
            return Task.FromResult(bRole);
        }

        public async Task UpdateAsync(BRole role)
        {
            var dRole = await GetAsync(role.Id);

            if (dRole != null)
            {
                await UpdateAsync(dRole);
            }
            else
            {
                throw new RoleReferenceNotFoundException("The role reference does not exist");
            }           
        }

        public IQueryable<BRole> Roles => Query().Select(r => r.ToRole());
    }
}
