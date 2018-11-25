using Mints.BLayer.Models.Identity;
using Mints.DLayer.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Mints.BLayer.Helpers;
using System.Threading.Tasks;
using BUser = Mints.BLayer.Models.Identity.User;
using DUser = Mints.DLayer.Models.User;
using Mints.BLayer.Exceptions;
using System.Text;

namespace Mints.BLayer.Repositories
{
    public class UserRepository : GenericRepository<DUser>, IUserRepository
    {
        public UserRepository(Context dbcontext)
        {
            _dbContext = dbcontext;
        }


        public async Task CreateAsync(BUser user)
        {
            var dUser = user.ToDbUser();
            await InsertAsync(dUser);
            user.Id = dUser.Id;
        }

        public async Task DeleteAsync(BUser user)
        {
            await DeleteAsync(user.Id);
        }

        public async Task<BUser> FindByIdAsync(int userId)
        {
            var user = await GetAsync(userId);
            if (user == null)
            {
                throw new UserReferenceNotFoundException("The user reference does not exist");
            }
            return user.ToUser();

        }

        public async Task<BUser> FindByNameAsync(string userName)
        {
            var user = await Query()
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null)
            {
                throw new UserReferenceNotFoundException("The user id does not exist");
            }
            var bUser = user.ToUser();
            return bUser;
        }

        public async Task<BUser> FindByEmailAsync(string email)
        {
            var user = await Query()
                 .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                throw new UserReferenceNotFoundException("The user with email does not exist");
            }
            var bUser = user.ToUser();
            return bUser;
        }

        public async Task UpdateAsync(BUser user)
        {
            var dUser = await GetAsync(user.Id);

            if (dUser != null)
            {
                dUser.Email = user.Email;
                dUser.Phone = user.PhoneNumber;
                dUser.DateUpdated = DateTime.Now;
                dUser.UserName = user.UserName;
                dUser.PasswordHash = user.PasswordHash;
                dUser.HashSalt = user.HashSalt;
                await UpdateAsync(dUser);
            }
            else
            {
                throw new UserReferenceNotFoundException("The user id does not exist");
            }
        }

       
        public async Task AddToRoleAsync(BUser user, string rolename)
        {
            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == rolename);
            if (role != null)
            {
                _dbContext.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = role.Id });
                await _dbContext.SaveChangesAsync();
            }
            throw new NullReferenceException("The role reference does not exist");
        }

        public async Task RemoveFromRoleAsync(BUser user, string rolename)
        {
            var userRoles = _dbContext.UserRoles
                .Include(u => u.User)
                .Include(u => u.Role)
                .Where(u => u.Role.Name == rolename && u.UserId == user.Id);
            _dbContext.UserRoles.RemoveRange(userRoles);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<string>> GetRolesAsync(BUser user)
        {
            var roles = await _dbContext.UserRoles
                .Include(u => u.Role)
                .Include(u => u.User)
                .Where(u => u.UserId == user.Id)
                .Select(r => r.Role.Name).ToListAsync();
            return roles;

        }

        public  Task<bool> IsInRoleAsync(BUser user, string rolename)
        {           
            return _dbContext.UserRoles
                .Include(u => u.User)
                .Include(u => u.Role)
                .Where(u => u.Role.Name == rolename && u.UserId == user.Id)
                .AnyAsync();

        }

        public async Task<IList<BUser>> GetUsersInRoleAsync(string roleName)
        {
            var users = await _dbContext.UserRoles
                 .Include(u => u.User)
                .Include(u => u.Role)
                .Where(u => u.Role.Name == roleName)
                .Select(r => r.User.ToUser()).ToListAsync();
            return users;
        }
              
    }
}
