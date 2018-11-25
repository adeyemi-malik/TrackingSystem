using Mints.DLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BUser = Mints.BLayer.Models.Identity.User;
using DUser = Mints.DLayer.Models.User;

namespace Mints.BLayer.Repositories
{
    public interface IUserRepository: IGenericRepository<DUser>
    {
        Task CreateAsync(BUser user);

        Task DeleteAsync(BUser user);

        Task<BUser> FindByIdAsync(int userId);

        Task<BUser> FindByNameAsync(string userName);

        Task<BUser> FindByEmailAsync(string email);

        Task UpdateAsync(BUser user);

        Task AddToRoleAsync(BUser user, string rolename);

        Task RemoveFromRoleAsync(BUser user, string rolename);

        Task<IList<string>> GetRolesAsync(BUser user);

        Task<bool> IsInRoleAsync(BUser user, string rolename);

        Task<IList<BUser>> GetUsersInRoleAsync(string roleName);
    }
}
