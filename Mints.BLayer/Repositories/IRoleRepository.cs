using Mints.DLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BRole = Mints.BLayer.Models.Identity.Role;
using DRole = Mints.DLayer.Models.Role;

namespace Mints.BLayer.Repositories
{
    public interface IRoleRepository: IGenericRepository<DRole>
    { 
        Task CreateAsync(BRole role);

        Task DeleteAsync(BRole role);

        Task<BRole> FindByIdAsync(int roleId);

        Task<BRole> FindByNameAsync(string roleName);

        Task UpdateAsync(BRole role);

    }
}
